using System;

namespace Tanner.Core.DataAccess
{
    public class OperationDetailResource
    {
        /// <summary>
        /// Number of operation
        /// </summary>
        /// <summary xml:lang="es">
        /// Número de operación
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Rut of client
        /// </summary>
        /// <summary xml:lang="es">
        /// Rut del cliente
        /// </summary>
        public string ClientRUT { get; set; }

        /// <summary>
        /// Social reason enterprise
        /// </summary>
        /// <summary xml:lang="es">
        /// Razón social de empresa
        /// </summary>
        public string SocialReason { get; set; }

        /// <summary>
        /// Type of operation
        /// </summary>
        /// <summary xml:lang="es">
        /// Tipo de operación 
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Status of operation
        /// </summary>
        /// <summary xml:lang="es">
        /// Estado de operación 
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Date of Grant
        /// </summary>
        /// <summary xml:lang="es">
        /// Fecha de cesión
        /// </summary>
        public DateTime GrantDate { get; set; }

        /// <summary>
        /// Type of document
        /// </summary>
        /// <summary xml:lang="es">
        /// Tipo de documento
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        /// Rate of operation
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa de la operación
        /// </summary>
        public float Rate { get; set; }

        /// <summary>
        /// Number of documents
        /// </summary>
        /// <summary xml:lang="es">
        /// Numero de documentos 
        /// </summary>
        public int CountDocuments { get; set; }

        /// <summary>
        /// Custody of Commisiion
        /// </summary>
        /// <summary xml:lang="es">
        /// Comisión por custodia 
        /// </summary>
        public float CustodyCommission { get; set; }

        /// <summary>
        /// Percentage of Commission
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje por comisión 
        /// </summary>
        public float PercentageCommission { get; set; }

        /// <summary>
        /// Expenses
        /// </summary>
        /// <summary xml:lang="es">
        /// Gastos  
        /// </summary>
        public float Expenses { get; set; }

        /// <summary>
        /// Difference of price
        /// </summary>
        /// <summary xml:lang="es">
        /// diferencia de precios
        /// </summary>
        public float PriceDifference { get; set; }

        /// <summary>
        /// Advance Percentage
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje de avance
        /// </summary>
        public float AdvancePercentage { get; set; }

        /// <summary>
        /// IVA
        /// </summary>
        /// <summary xml:lang="es">
        /// IVA
        /// </summary>
        public float IVA { get; set; }

        /// <summary>
        /// Discount
        /// </summary>
        /// <summary xml:lang="es">
        /// Descuentos 
        /// </summary>
        public float Discount { get; set; }

        /// <summary>
        /// Other Discount
        /// </summary>
        /// <summary xml:lang="es">
        /// Otros descuentos 
        /// </summary>
        public float OtherDiscount { get; set; }

        /// <summary>
        /// Other Advance
        /// </summary>
        /// <summary xml:lang="es">
        /// Otros avances
        /// </summary>
        public float OtherAdvance { get; set; }

        /// <summary>
        /// Cash spin
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto a girar
        /// </summary>
        public float CashSpin { get; set; }

        /// <summary>
        /// Document Amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto de documento
        /// </summary>
        public float DocumentAmount { get; set; }

        /// <summary>
        /// Bussiness amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto de negocio
        /// </summary>
        public float BussinessAmount { get; set; }

        /// <summary>
        /// Name of executive
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre del ejecutivo
        /// </summary>
        public string ExecutiveName { get; set; }

        /// <summary>
        /// Email Executive
        /// </summary>
        /// <summary xml:lang="es">
        /// Correo ejecutivo
        /// </summary>
        public string ExecutiveEmail { get; set; }

        /// <summary>
        /// Name BranchOffice
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre sucursal
        /// </summary>
        public string BranchOffice { get; set; }

        /// <summary>
        /// All In Rate
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa All in (CAE)
        /// </summary>
        public decimal  AllInRate { get; set; }

        /// <summary>
        /// External code of coin
        /// </summary>
        /// <summary xml:lang="es">
        /// Código externo de la moneda
        /// </summary>
        private string _coinExternalCode;
        public string CoinExternalCode {
            get {
                return _coinExternalCode?.Trim();
            }
            set {
                _coinExternalCode = value;
            }
        }

		/// <summary>
		/// Bank code
		/// </summary>
		/// <summary xml:lang="es">
		/// Código del banco
		/// </summary>
		public string BankCode { get; set; }

		/// <summary>
		/// Bank name
		/// </summary>
		/// <summary xml:lang="es">
		/// Nombre del banco
		/// </summary>
		public string BankName { get; set; }

		/// <summary>
		/// Current account
		/// </summary>
		/// <summary xml:lang="es">
		/// Cuenta corriente
		/// </summary>
		public string BankAccount { get; set; }


        public static (string, object) Query_GetOperationDataByNumber(int number)
        {
            var query = $@"
                DECLARE 
					@Dias_Ponderado_Operacion NUMERIC(18, 4)

				--Variable de tipo tabla 
				DECLARE @documentos TABLE
				(
					id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,	
					numero_docto INT,
					dif_precio_x_docto NUMERIC(18, 4)
				)

				DECLARE @documentos_dias_ponderados TABLE
				(
					id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,	
					numero_docto INT,
					Dias_Ponderado_Operacion NUMERIC(18, 0)
				)

				--insertamos los valores 
				INSERT 
					INTO @documentos
				SELECT 
					doc.id_documento, 
					((valor_futuro_documento * dias_documento * (
						SELECT 
							ISNULL(ope.tasa_operacion, 0)
						FROM 
							DBA.TB_FIN17 ope WITH(NOLOCK)
						WHERE 
							ope.numero_operacion = doc.numero_operacion)
						) / 3000 
					) AS dif_precio_x_docto
				FROM 
					DBA.TB_FIN24 doc WITH(NOLOCK)
				WHERE 
					doc.numero_operacion = @number

				INSERT
					INTO @documentos_dias_ponderados
				SELECT 
					doc.id_documento, 
					(valor_futuro_documento * dias_documento) 
				FROM 
					DBA.TB_FIN24 doc WITH(NOLOCK)
				WHERE 
					doc.numero_operacion = @number

				SELECT 
					SALIDA.Number AS Number,  
					SALIDA.ClientRUT AS ClientRUT,  
					SALIDA.SocialReason AS SocialReason, 				 
					SALIDA.Type AS Type,  
					SALIDA.Status AS Status,
					SALIDA.GrantDate AS GrantDate,
					SALIDA.DocumentType AS DocumentType,
					SALIDA.Rate AS Rate,
					SALIDA.CountDocuments AS CountDocuments,
					SALIDA.CustodyCommission AS CustodyCommission, 
					SALIDA.PercentageCommission AS PercentageCommission,								
					SALIDA.Expenses AS Expenses, 
					SALIDA.PriceDifference AS PriceDifference,				
					SALIDA.AdvancePercentage AS AdvancePercentage,  
					SALIDA.IVA AS IVA,	 
					SALIDA.Discount AS Discount,     
					SALIDA.OtherDiscount AS OtherDiscount,
					SALIDA.OtherAdvance AS OtherAdvance,


					SALIDA.banco_giro AS BankCode,
					SALIDA.ctacte_giro AS BankAccount,
					SALIDA.descripcion_banco AS BankName,

					CONVERT(
						NUMERIC(20), 
						ISNULL(
							(
								SALIDA.valor_futuro - 
								CASE 
									WHEN SALIDA.es_vencido = 1 THEN 0 
									ELSE SALIDA.valor_intereses 
								END - 
								SALIDA.Expenses - 
								SALIDA.Discount - 
								SALIDA.IVA - 
								SALIDA.CustodyCommission - 
								SALIDA.OtherDiscount - 
								SALIDA.notario - 
								SALIDA.impuesto - 
								SALIDA.OtherAdvance - 
								SALIDA.gac - 
								SALIDA.comision_fogain + 
								CASE
									WHEN SALIDA.codigo_moneda = 1 THEN 0 
									ELSE SALIDA.diferencia_prepago 
								END 
							) - 
							SALIDA.descuento_x_fuera,
						0), 
					0) AS CashSpin,
					SALIDA.DocumentAmount AS DocumentAmount,
					SALIDA.BussinessAmount AS BussinessAmount,
					SALIDA.ExecutiveName AS ExecutiveName,
					SALIDA.ExecutiveEmail AS ExecutiveEmail,
					SALIDA.BranchOffice AS BranchOffice,
					ISNULL(
						CASE 
							WHEN SALIDA.es_credito = 1 THEN SALIDA.Rate 
							ELSE(
								(
									3000 * 
									(
										SALIDA.valor_intereses + 
										SALIDA.Expenses + 
										SALIDA.Discount + 
										SALIDA.IVA + 
										SALIDA.CustodyCommission + 
										SALIDA.OtherDiscount + 
										SALIDA.notario + 
										SALIDA.impuesto + 
										SALIDA.OtherAdvance + 
										SALIDA.gac + 
										SALIDA.comision_fogain - 
										CASE 
											WHEN SALIDA.codigo_moneda = 1 THEN 0 
											ELSE SALIDA.diferencia_prepago 
										END + 
										SALIDA.descuento_x_fuera
									)
								) / 
								(
									SALIDA.dias_ponderados * 
									SALIDA.valor_futuro
								)
							) 
						END,
						0
					) AS AllInRate,
					SALIDA.CoinExternalCode AS CoinExternalCode
				FROM (
				SELECT    
					OPE.numero_operacion AS Number,  
					dbo.f_formatea_rut_azure(PER.RUT_PERSONA) AS ClientRUT,  
					LTRIM(RTRIM(PER.razon_social)) AS SocialReason, 				 
					LTRIM(RTRIM(ope.tipo_operacion)) AS Type,  
					EstOP.descripcion AS Status,
					ISNULL(log_evaluacion.fecha_otorgamiento, ope.fecha_operacion) AS GrantDate,
					LTRIM(RTRIM(TDOC.descripcion_documento)) AS DocumentType,
					ISNULL(OPE.tasa_operacion, 0) AS Rate,
					COUNT(DOC.NUMERO_DOCUMENTO) AS CountDocuments,
					ISNULL(MAX(ope.monto_comi_cob), 0)  AS CustodyCommission, 
					OPE.factor_comi_cob AS PercentageCommission,								
					ISNULL(OPE.cargos_afectos, 0) AS Expenses, 
					OPE.interes_operacion AS PriceDifference,				
					ISNULL(OPE.porcentaje_descuento, 0) AS AdvancePercentage,  
					CONVERT(NUMERIC(11), ISNULL(iva_operacion, 0)) AS IVA,	 
					MAX(ISNULL(ope.cargos_exentos, 0)) AS Discount,     
					MAX(ISNULL(ope.otros_descuentos, 0)) AS OtherDiscount,
					MAX(ISNULL(ope.otros_anticipos, 0)) AS OtherAdvance,
					sum(DOC.valor_futuro_documento) as valor_futuro,
					sum(( isnull(DOC.interes_documento,0) +  isnull(DOC.interes_vencido,0)  )) as valor_intereses, 
					SUM( 
						CASE 
							WHEN ope.codigo_moneda = 1 THEN doc.VALOR_NOMINAL_DOCUMENTO 
							ELSE doc.VALOR_NOMINAL_MX 
						END
					) AS DocumentAmount,
					OPE.valor_futuro_operacion AS BussinessAmount,
					RTRIM(EMP.nombre_empleado) AS ExecutiveName,
					USU.CORREO AS ExecutiveEmail,
					LTRIM(RTRIM(suc.descripcion_sucursal)) AS BranchOffice,
					moneda.cod_ext_mon AS CoinExternalCode,
					(case when td.otro_tratamiento = 'V' then 1 else 0 end ) as es_vencido, --vencido
					isnull(oc.notario,0) as notario,--notario
					isnull(OPE.impuesto,0) as impuesto,    --impuestos
					isnull(( oc.GAC - oc.notario ),0) as gac, --gac
					isnull(oc.comision_fogain,0) as comision_fogain,  --comision fogain
					ope.codigo_moneda AS codigo_moneda,
					isnull(ope.diferencia_prepago,0) as diferencia_prepago,
					isnull(ope.descuento_x_fuera,0) as descuento_x_fuera, --descuentos por fuera
					sum(DOC.valor_futuro_documento *  case when DOC.dias_documento = 0 then null else DOC.dias_documento end ) / sum(DOC.valor_futuro_documento)  as dias_ponderados,   -- // días ponderados de la operación
					(case when td.otro_tratamiento in ('C','V') then 1 
								 else (case when td.tipo_documento in (83,84,85) then 1 else 0 end) --KU CAPITAL PREFERENCIAL UF, KP CAPITAL PREFERENCIAL PESOS y KS CAPITAL PREFERENCIAL U$
								 end ) as es_credito, --1 credito; 0 no credito
					OPE.banco_giro,
					OPE.ctacte_giro,
					BANK.descripcion_banco
				FROM		
					DBA.TB_FIN17 OPE WITH(NOLOCK) /*MCG*/    
				INNER JOIN 
					dba.tb_fin61 td WITH(NOLOCK) ON td.tipo_documento = OPE.tipo_documento


				LEFT JOIN
					dba.TB_FIN26 BANK WITH(NOLOCK) ON OPE.banco_giro = BANK.codigo_banco

				LEFT JOIN 
					DBA.TB_FIN24 DOC WITH(NOLOCK) ON (OPE.numero_operacion = DOC.NUMERO_OPERACION) 
				LEFT JOIN 
					dbo.operacion_credito oc WITH(NOLOCK) ON OPE.numero_operacion = oc.numero_operacion 
				LEFT JOIN 
					DBA.TB_FIN01 CLI  WITH(NOLOCK) ON (OPE.CODIGO_CLIENTE  = CLI.CODIGO_CLIENTE)   
				LEFT JOIN 
					DBA.TB_FIN41 PER WITH(NOLOCK) ON (CLI.CODIGO_PERSONA  = PER.CODIGO_PERSONA) 
				LEFT JOIN 
					DBA.TB_FIN08 DEU  WITH(NOLOCK) ON (DOC.CODIGO_TERCERO  = DEU.CODIGO_TERCERO)
				LEFT JOIN 
					DBA.TB_FIN41 PDEU WITH(NOLOCK) ON (DEU.CODIGO_PERSONA = PDEU.CODIGO_PERSONA)
				LEFT JOIN 
					DBA.TB_FIN61 TDOC WITH(NOLOCK) ON (OPE.TIPO_DOCUMENTO = TDOC.tipo_documento)
				LEFT JOIN 
					dba.tb_fin50 EstOP WITH(NOLOCK) ON (OPE.estado_operacion = EstOp.codigo AND EstOp.tipo =3) 
				LEFT JOIN 
					dba.tb_fin06 EMP WITH(NOLOCK) ON (CLI.codigo_empleado = EMP.codigo_empleado) 
				LEFT JOIN (
					SELECT 
						num_oper, 
						fyh_log AS fecha_otorgamiento 
					FROM 
						log_evaluacion WITH(NOLOCK) 
					WHERE 
						num_oper = @number AND 
						cod_accion = 20 AND 
						des_accion LIKE '%otorgo%'
				) log_evaluacion ON (ope.numero_operacion = log_evaluacion.num_oper)
				LEFT JOIN 
					Segpriv.dba.usuario USU WITH(NOLOCK) ON (EMP.login_name = USU.login_name)
				INNER JOIN 
					dba.tb_fin44 suc WITH(NOLOCK) ON (ope.codigo_sucursal = suc.codigo_sucursal)
				INNER JOIN 
					@documentos docu ON (docu.numero_docto = doc.id_documento)
				INNER JOIN 
					@documentos_dias_ponderados docu_d ON (docu_d.numero_docto = doc.id_documento)
				INNER JOIN 
					dba.tb_zona zo WITH(NOLOCK) ON (suc.codigo_zona = zo.codigo_zona)
				INNER JOIN 
					dba.tb_fin45 moneda WITH(NOLOCK) ON (moneda.codigo_moneda = OPE.codigo_moneda)
				WHERE		 
					OPE.numero_operacion = @number AND
					DOC.estado_documento NOT IN(7, 8, 9, 99)
				GROUP BY 	
					PER.rut_persona, 
					PER.razon_social, 
					ope.tipo_operacion, 
					ope.fecha_operacion, 
					OPE.numero_operacion,  
					TDOC.cod_tipo_doc, 
					TDOC.descripcion_documento, 
					OPE.cargos_afectos, 
					iva_operacion,
					EstOP.descripcion,
					CLI.nombre_cliente,
					OPE.tasa_operacion,
					OPE.valor_futuro_operacion,
					OPE.factor_comi_cob,
					OPE.interes_operacion,
					EMP.nombre_empleado, 
					USU.CORREO,
					OPE.porcentaje_descuento,
					log_evaluacion.fecha_otorgamiento, 
					suc.descripcion_sucursal,  
					OPE.valor_futuro_operacion,
					moneda.cod_ext_mon,
					td.otro_tratamiento,
					oc.notario,
					BANK.descripcion_banco,
					OPE.banco_giro,
					OPE.ctacte_giro,
					OPE.impuesto,
					 oc.GAC - oc.notario,
					 oc.comision_fogain,
					 ope.codigo_moneda,
					 ope.diferencia_prepago,
					 ope.descuento_x_fuera,
					 td.tipo_documento
				) SALIDA
            ";
            var param = new
            {
                number
            };

            (string, object) result = (query, param);

            return result;
        }
    
		/// <summary>
		/// Query to get operation status
		/// </summary>
		/// <param name="number">Operation number</param>
		/// <returns>Query and parameters</returns>
		public static (string, object) Query_GetOperationStatus(long number)
		{
			var query = @"
				SELECT 
					OP.numero_operacion AS Number,
					OP.estado_operacion AS Status,
					CRED.tarea_actual AS CurrentTask,
					CASE
						WHEN CRED.vb_ejecutivo = 'S' THEN 1
						ELSE 0
					END AS IsExecutiveApproval
				FROM 
					dba.tb_fin17 as OP WITH(NOLOCK)
				LEFT JOIN (
					SELECT 
						tarea_actual,
						num_op,
						vb_ejecutivo 
					FROM  
						dbo.credito WITH(NOLOCK)
				) CRED ON (OP.numero_operacion = CRED.num_op)
				WHERE
					OP.numero_operacion = @number
			";

			var parameters = new
			{
				number
			};

			(string, object) result = (query, parameters);
			return result;
		}
	}
}