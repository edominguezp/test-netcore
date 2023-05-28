namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent resource for proposed payment
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa el recurso para el pago propuesto
    /// </summary>
    public class OperationDocumentsAmountResource
    {
        /// <summary>
        /// Number of operation
        /// </summary>
        /// <summary xml:lang="es">
        /// Número de operación
        /// </summary>
        public long Number { get; set; }

        /// <summary>
        /// Advance Amount Document
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto anticipado del documento
        /// </summary>
        public string AdvanceAmountDocument { get; set; }

        /// <summary>
        /// Advance Amount Document
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto anticipado del documento
        /// </summary>
        public long AmountValue { get {
                if (string.IsNullOrEmpty(AdvanceAmountDocument))
                    return 0;
                string[] data = AdvanceAmountDocument.Split(".");
                long result = long.Parse(data[0]);
                return result;
            } }

        /// <summary>
        /// Document Days
        /// </summary>
        /// <summary xml:lang="es">
        /// Días del documento          
        /// </summary>
        public int DocumentDays { get; set; }

		/// <summary>
		/// Query to get actual and proposed payment
		/// </summary>
		/// <summary xml:lang="es">
		/// Consulta para obtener el pago actual y propuesto
		/// </summary>
		public static (string, object) Query_GetProposedAndActualPayment(int operationNumber, CommercialTermsResource request)
		{
			var query = $@"				
				DECLARE  
					@coinCode			INT,
					@proposedAmount		NUMERIC(16,4),
					@proposedAllIn		NUMERIC(7,4),
					@actualAmount		NUMERIC(16,4),
					@actualAllIn		NUMERIC(7,4),
					@operationIVA		NUMERIC(16,4),
					@rateOperation		NUMERIC(7,4),
					@rateIVA			NUMERIC(7,4),
					@productCode		INT,
					@existOperation		BIT,
					@otherTreatment		CHAR(1),
					@futureValue		NUMERIC(16,4),
					@expiredInterest	NUMERIC(16,4),
					@tax				NUMERIC(16,4)

				SET @existOperation = 0

				SELECT 
					@rateIVA = tasa_iva 
				FROM 
					dba.tb_fin00 WITH(NOLOCK)
	
				SELECT 
					@existOperation = 1,
					@coinCode = o.codigo_moneda,
					@operationIVA = ISNULL(ROUND((ISNULL(@commission, 0) + ISNULL(@expenses, 0)) * (@rateIVA / 100.0), 0), 0), 
					@rateOperation = tasa_operacion,
					@otherTreatment = td.otro_tratamiento
				FROM  
					dba.tb_fin17 o  WITH(NOLOCK)
				INNER JOIN 
					dba.tb_fin61 td WITH(NOLOCK) ON (td.tipo_documento = o.tipo_documento)
				WHERE 
					o.numero_operacion = @operationNumber

				IF @otherTreatment = 'V'
					BEGIN
						EXEC dbo.spe_oper_obt_datos_credito  
							@operationNumber,
							@rate, --TASA
							@commission, --COMISION 
							@expenses, --GASTOS
							@futureValue output, --VALOR FUTURO
							@expiredInterest output, --DIFERENCIA PRECIO
							@tax output --IMPUESTO 
					END

				--ACTUAL-----------------------------------------------------------
				SELECT 	
					@actualAmount = salida.monto_giro,
					@actualAllIn = 
						CASE 
							WHEN salida.monto_giro > 0 THEN salida.tasaAllIn 
							ELSE 0 
						END
				FROM (
					SELECT 
						ISNULL((
							ope.valor_futuro
							- CASE 
								WHEN ope.es_vencido = 1 THEN 0 
								ELSE ope.valor_intereses 
							END
							- ope.cargos_afectos  
							- ope.cargos_exentos  
							- ope.iva_operacion 
							- ope.monto_comi_cob 
							- ope.otros_descuentos 
							- ope.notario  
							- ope.impuesto  
							- ope.otros_anticipos   
							- ope.gac  
							- ope.comision_fogain 
							+ (CASE 
								WHEN @coinCode = 1 THEN 0 
								ELSE ope.diferencia_prepago 
							END ) 
						) 
						- ope.descuento_x_fuera,0) AS monto_giro,
						ISNULL(
							CASE 
								WHEN ope.es_credito = 1 THEN @rateOperation 
								ELSE ((
									3000 * (
										ope.valor_intereses 
										+ ope.cargos_afectos  
										+ ope.cargos_exentos  
										+ ope.iva_operacion 
										+ ope.monto_comi_cob 
										+ ope.otros_descuentos 
										+ ope.notario  
										+ ope.impuesto  
										+ ope.otros_anticipos   
										+ ope.gac  
										+ ope.comision_fogain 
										- (CASE 
											WHEN @coinCode = 1 THEN 0 
											ELSE ope.diferencia_prepago 
										END )  
										+ ope.descuento_x_fuera  
									)) / (
										ope.dias_ponderados 
										* ope.valor_futuro)
								) 
							END,
						0) AS tasaAllIn
					FROM (
						SELECT  
							SUM(documento.valor_futuro_documento) AS valor_futuro, --monto anticipado
							SUM(documento.valor_futuro_documento 
								* CASE 
									WHEN documento.dias_documento = 0 THEN NULL 
									ELSE documento.dias_documento 
								END
							) / SUM(documento.valor_futuro_documento) AS dias_ponderados, --días ponderados de la operación
							SUM(
								ISNULL(documento.interes_documento, 0) 
								+ ISNULL(documento.interes_vencido, 0) 
							) AS valor_intereses, --dif precio
							ISNULL(operacion.cargos_afectos, 0) AS cargos_afectos, --gastos
							ISNULL(operacion.cargos_exentos, 0) AS cargos_exentos, --descuentos
							ISNULL(operacion.iva_operacion, 0) AS  iva_operacion, --iva
							ISNULL(operacion.otros_descuentos, 0) AS otros_descuentos, --otros descuentos
							ISNULL(operacion.monto_comi_cob, 0) AS monto_comi_cob, --comisión (comision custodia)
							ISNULL(operacion.impuesto, 0) AS impuesto, --impuestos
							ISNULL(operacion.otros_anticipos, 0) AS otros_anticipos, --anticipo
							ISNULL((oc.GAC - oc.notario), 0) AS gac, --gac
							CASE 
								WHEN td.otro_tratamiento = 'V' THEN 1 
								ELSE 0 
							END AS es_vencido, --vencido
							CASE 
								WHEN td.otro_tratamiento IN ('C','V') THEN 1 
								ELSE (
									CASE 
										WHEN td.tipo_documento IN (83, 84, 85) THEN 1 
										ELSE 0 
									END) --KU CAPITAL PREFERENCIAL UF, KP CAPITAL PREFERENCIAL PESOS y KS CAPITAL PREFERENCIAL U$
							END AS es_credito, --1 credito; 0 no credito
							ISNULL(oc.comision_fogain, 0) AS comision_fogain, --comision fogain
							ISNULL(operacion.descuento_x_fuera, 0) AS descuento_x_fuera, --descuentos por fuera
							ISNULL(oc.notario, 0) AS notario, --notario
							ISNULL(operacion.diferencia_prepago, 0) AS diferencia_prepago
						FROM  
							dba.tb_fin17 operacion WITH(NOLOCK)
						INNER JOIN 
							dba.tb_fin24 documento WITH(NOLOCK) ON operacion.numero_operacion = documento.numero_operacion
						INNER JOIN 
							dba.tb_fin61 td WITH(NOLOCK) ON td.tipo_documento = operacion.tipo_documento
						LEFT OUTER JOIN 
							dbo.operacion_credito oc WITH(NOLOCK) ON operacion.numero_operacion = oc.numero_operacion
						WHERE 
							operacion.numero_operacion = @operationNumber AND 
							documento.estado_documento NOT IN(7, 8, 9, 99)
						GROUP BY 
							operacion.cargos_afectos,
							operacion.cargos_exentos,
							operacion.iva_operacion,
							operacion.otros_descuentos,
							operacion.monto_comi_cob,
							operacion.impuesto,
							operacion.otros_anticipos,
							(oc.GAC - oc.notario),
							td.otro_tratamiento,
							td.tipo_documento,
							oc.comision_fogain,
							operacion.descuento_x_fuera,
							oc.notario,
							operacion.diferencia_prepago
						HAVING 
							SUM(documento.valor_futuro_documento) > 0 
					) ope 
				) salida

				--PROPOSED-----------------------------------------------------------
				SELECT 	
					@proposedAmount = salida.monto_giro,
					@proposedAllIn = 
						CASE 
							WHEN salida.monto_giro > 0 THEN salida.tasaAllIn 
							ELSE 0 
						END
				FROM(
					SELECT 
						ISNULL(
							(
								CASE 
									WHEN ope.es_vencido = 1 THEN @futureValue 
									ELSE ope.valor_futuro 
								END - 
								CASE 
									WHEN ope.es_vencido = 1 THEN 0  
									ELSE ope.valor_intereses 
								END -  
								CASE 
									WHEN ope.es_vencido = 1 THEN 0 
									ELSE @expenses 
								END - 
								ope.cargos_exentos - 
								@operationIVA -
								@commission - 
								ope.otros_descuentos - 
								ope.notario - 
								CASE 
									WHEN ope.es_vencido = 1 THEN @tax 
									ELSE ope.impuesto 
								END - 
								ope.otros_anticipos - 
								ope.gac - 
								ope.comision_fogain + 
								CASE 
									WHEN @coinCode = 1 THEN 0 
									ELSE ope.diferencia_prepago 
								END
							) - 
							ope.descuento_x_fuera, 
						0) AS monto_giro,
						ISNULL(
							CASE 
								WHEN ope.es_credito = 1 THEN @rate 
								ELSE (
									(3000 *(
										ope.valor_intereses + 
										@expenses + 
										ope.cargos_exentos  +  
										@operationIVA + 
										@commission + 
										ope.otros_descuentos + 
										ope.notario + 
										ope.impuesto + 
										ope.otros_anticipos + 
										ope.gac + 
										ope.comision_fogain - 
										CASE 
											WHEN @coinCode = 1 THEN 0 
											ELSE ope.diferencia_prepago 
										END + 
										ope.descuento_x_fuera 
									)) / 
									(ope.dias_ponderados * ope.valor_futuro)
								) 
							END, 
						0) AS tasaAllIn
					FROM(
						SELECT 
							SUM(documento.valor_futuro_documento) AS valor_futuro, --monto anticipado
							SUM(
								documento.valor_futuro_documento * 
								CASE 
									WHEN documento.dias_documento = 0 THEN NULL 
									ELSE documento.dias_documento 
								END
							) / SUM(documento.valor_futuro_documento) AS dias_ponderados, -- días ponderados de la operación
							SUM(
								documento.valor_futuro_documento * 
								CASE 
									WHEN documento.dias_documento = 0 THEN NULL 
									ELSE documento.dias_documento 
								END * 
								CASE 
									WHEN @coinCode = 2 THEN (@rate/12) 
									ELSE @rate 
								END / 
								3000
							) AS valor_intereses,
							ISNULL(operacion.cargos_exentos, 0) AS cargos_exentos, --descuentos
							ISNULL(operacion.otros_descuentos, 0) AS otros_descuentos, --otros descuentos
							ISNULL(operacion.impuesto, 0) AS impuesto, --impuestos
							ISNULL(operacion.otros_anticipos, 0) AS otros_anticipos, --anticipo
							ISNULL((oc.GAC - oc.notario), 0) AS gac, --gac
							CASE 
								WHEN td.otro_tratamiento = 'V' THEN 1 
								ELSE 0 
							END AS es_vencido, --vencido
							CASE 
								WHEN td.otro_tratamiento IN('C', 'V') THEN 1 
								ELSE 
									CASE 
										WHEN td.tipo_documento IN(83,84,85) THEN 1 
										ELSE 0 
									END --KU CAPITAL PREFERENCIAL UF, KP CAPITAL PREFERENCIAL PESOS y KS CAPITAL PREFERENCIAL U$
							END AS es_credito, --1 credito; 0 no credito
							ISNULL(oc.comision_fogain, 0) AS comision_fogain,  --comision fogain
							ISNULL(operacion.descuento_x_fuera, 0) AS descuento_x_fuera, --descuentos por fuera
							ISNULL(oc.notario, 0) AS notario,--notario
							ISNULL(operacion.diferencia_prepago, 0) AS diferencia_prepago
						FROM  
							dba.tb_fin17 operacion WITH(NOLOCK)
						INNER JOIN 
							dba.tb_fin24 documento WITH(NOLOCK) ON (operacion.numero_operacion = documento.numero_operacion)
						INNER JOIN 
							dba.tb_fin61 td WITH(NOLOCK) ON (td.tipo_documento = operacion.tipo_documento)
						LEFT OUTER JOIN 
							dbo.operacion_credito oc WITH(NOLOCK) ON (operacion.numero_operacion = oc.numero_operacion)
						WHERE 
							operacion.numero_operacion = @operationNumber AND
							documento.estado_documento NOT IN(7, 8, 9, 99)
						GROUP BY 
							operacion.cargos_exentos,
							operacion.otros_descuentos,
							operacion.impuesto,
							operacion.otros_anticipos,
							(oc.GAC - oc.notario),
							td.otro_tratamiento,
							td.tipo_documento,
							oc.comision_fogain,
							operacion.descuento_x_fuera,
							oc.notario,
							operacion.diferencia_prepago
					) ope
				) salida

				--OUT-----------------------------------------------------------
				IF OBJECT_ID('tempdb..#MyTable') IS NOT NULL DROP Table #MyTable

				SELECT 
					ROUND(@proposedAmount, 0) AS proposedAmount,
					ISNULL(@proposedAllIn, 0) AS proposedAllIn,
					ROUND(@actualAmount, 0) AS actualAmount,
					ISNULL(@actualAllIn, 0) AS actualAllIn,
					@existOperation AS existOperation
					INTO #MyTable

				SELECT 
					proposedAmount, 
					proposedAllIn, 
					actualAmount, 
					actualAllIn 
				FROM 
					#MyTable 
				WHERE
					existOperation = 1
            ";

			object param = new
			{
				operationNumber,
				expenses = request.Expenses,
				commission = request.Commission,
				rate = request.Rate
			};
			(string, object) result = (query, param);
			return result;
		}

	}
}
