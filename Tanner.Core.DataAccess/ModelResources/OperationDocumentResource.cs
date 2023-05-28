using System.Collections.Generic;
using Tanner.Core.DataAccess.Models;

namespace Tanner.Core.DataAccess.ModelResources
{
    public class OperationDocumentResource
    {
        public int Total { get; set; }

        public IEnumerable<OperationDocument> Documents { get; set; }

        public static (string, object) Query_OperationDocumentByRUT(string rut, int? page, int? pageSize, string order = null, string orderBy = null, string operationNumber = null, string fromDate = null, string toDate = null)
        {
            var query = @"DECLARE  
				 @inicio			INT = NULL
				,@fin				INT = NULL
				,@li_codigo_cliente INT
				,@EO_ANALISIS		INT = 0
				,@EO_APROBADA		INT = 1
				,@EO_VIGENTE		INT = 2
				,@EO_CANCELADA	    INT = 3
				,@ED_ELIMINADA		INT = 9
				,@ED_FANTASMA		INT = 99
				,@MONEDA_PESOS		INT = 1

				IF (@orderByColumnName IS NULL) SET @orderByColumnName = 'numero_operacion'  
				IF (@orderByAscDesc IS NULL)    SET @orderByAscDesc = 'asc'  
				Set @orderByColumnName = Lower(@orderByColumnName)  
				Set @orderByAscDesc    = Lower(@orderByAscDesc) 

				--declaramos tabla de salida
				DECLARE @tabla_salida table (numero_operacion	numeric(8, 0),
										fecha_otorgamiento	char (8),
										tipo_documento		char (100),
										monto_documento		numeric(20, 4), 
										monto_anticipado	numeric(20, 4), 
										giro_real			numeric(20, 4), 
										estado_operacion	char (50),
										estado_operacion_codigo int)
	    
				SET @inicio = ISNULL(@pagina, 0) * ISNULL(@registros, 10)
				SET @fin = ISNULL(@registros, 10)
				--
				SELECT @li_codigo_cliente = cli.codigo_cliente
				  FROM dba.tb_fin01 cli (NoLock)
				 INNER JOIN (Select codigo_persona From dba.tb_fin41 (NoLock) WHERE	rut_persona = right('0000000000' + replace(@rut_cli,'-',''),10) )per ON per.codigo_persona = cli.codigo_persona
				--
				INSERT INTO @tabla_salida
				SELECT ope.numero_operacion
						, CONVERT(CHAR(8), ope.fecha_operacion,112) AS fecha_otorgamiento
						, tdoc.descripcion_documento AS tipo_documento
						, SUM(CONVERT(NUMERIC(11), CASE WHEN ope.codigo_moneda = @MONEDA_PESOS THEN doc.valor_nominal_documento ELSE doc.valor_nominal_mx END)) AS monto_documento
						, SUM( CONVERT(NUMERIC(11),CASE WHEN ope.codigo_moneda = @MONEDA_PESOS THEN doc.VALOR_FUTURO_DOCUMENTO  ELSE doc.VALOR_FUTURO_MX  END) ) AS MONTO_ANTICIPADO
						, SUM(valor_futuro_documento) - (SUM(interes_documento) + MAX(isnull(ope.monto_comi_cob, 0)) + MAX(isnull(ope.cargos_afectos, 0)) + MAX(isnull(ope.iva_operacion, 0)) ) - 
						( MAX(isnull(ope.cargos_exentos,0)) + MAX(isnull(ope.otros_descuentos,0)) + MAX(isnull(ope.impuesto,0)) + MAX(isnull(ope.otros_anticipos,0)) ) AS giro_real
					 , CASE WHEN ope.estado_operacion = 0 THEN 'En analisis'
							WHEN ope.estado_operacion = 1 THEN 'Aprobada' 
							WHEN ope.estado_operacion = 2 THEN 'Financiada y Vigente' 
							WHEN ope.estado_operacion = 3 THEN 'Financiada y Recaudada'
							END AS ESTADO_OPERACION
						, ope.estado_operacion AS estado_operacion_codigo
					FROM DBA.TB_FIN17 OPE (NOLOCK)
						INNER JOIN (Select tipo_documento, cod_tipo_doc, descripcion_documento From DBA.TB_FIN61 (NOLOCK)) TDOC ON ope.tipo_documento  = tdoc.tipo_documento   
						INNER JOIN (Select numero_documento, valor_nominal_documento, valor_nominal_mx, VALOR_FUTURO_DOCUMENTO, VALOR_FUTURO_MX, interes_documento, numero_operacion From DBA.TB_FIN24 d (NOLOCK)
										Where estado_documento not in(@ED_ELIMINADA, @ED_FANTASMA) 
									) DOC ON ope.numero_operacion = doc.numero_operacion 
					WHERE ope.codigo_cliente = @li_codigo_cliente  
					AND ope.estado_operacion IN (@EO_ANALISIS, @EO_APROBADA, @EO_VIGENTE, @EO_CANCELADA)
					AND (@numero_operacion is null OR ope.numero_operacion = @numero_operacion)
					AND (@fecha_desde is null OR DateDiff(day, @fecha_desde, ope.fecha_operacion) >= 0)   
					AND (@fecha_hasta is null OR DateDiff(day, ope.fecha_operacion, @fecha_hasta) >= 0) 
					GROUP BY ope.tipo_operacion, ope.FECHA_OPERACION, ope.numero_operacion, tdoc.descripcion_documento, ope.cargos_afectos, ope.iva_operacion, ope.estado_operacion 
					--ORDER BY numero_operacion ASC	
		
				--SALIDA1
				Select count(1) Total From @tabla_salida
				--SALIDA2
				Select numero_operacion AS OperationNumber
					  , CONVERT(DATETIME, fecha_otorgamiento, 126) AS GrantDate
					  , CAST(RTRIM(LTRIM(tipo_documento)) AS VARCHAR(150)) AS DocumentType
					  , monto_documento AS DocumentAmount
					  , monto_anticipado AS AdvancedAmount
					  , giro_real AS BankDraft
					  , CAST(RTRIM(LTRIM(estado_operacion)) AS VARCHAR(100)) AS OperationStatus
					  , estado_operacion_codigo AS OperationStatusCode
				   from @tabla_salida
				  --order by numero_operacion ASC
					order by case when @orderByColumnName = 'numero_operacion'     and @orderByAscDesc = 'asc'  then numero_operacion end ASC  
						  , case when @orderByColumnName = 'numero_operacion'     and @orderByAscDesc = 'desc' then numero_operacion end DESC  
					   , case when @orderByColumnName = 'fecha_otorgamiento'     and @orderByAscDesc = 'asc'  then fecha_otorgamiento end ASC  
						  , case when @orderByColumnName = 'fecha_otorgamiento'     and @orderByAscDesc = 'desc' then fecha_otorgamiento end DESC  
					   , case when @orderByColumnName = 'tipo_documento'      and @orderByAscDesc = 'asc'  then tipo_documento end ASC  
						  , case when @orderByColumnName = 'tipo_documento'      and @orderByAscDesc = 'desc' then tipo_documento end DESC  
					   , case when @orderByColumnName = 'monto_documento'      and @orderByAscDesc = 'asc'  then monto_documento end ASC  
						  , case when @orderByColumnName = 'monto_documento'      and @orderByAscDesc = 'desc' then monto_documento end DESC  
					   --, case when @orderByColumnName = 'monto_documento_moneda'  and @orderByAscDesc = 'asc'  then monto_documento_moneda end ASC  
					   --   , case when @orderByColumnName = 'monto_documento_moneda'  and @orderByAscDesc = 'desc' then monto_documento_moneda end DESC  
					   , case when @orderByColumnName = 'monto_anticipado'     and @orderByAscDesc = 'asc'  then monto_anticipado end ASC  
						  , case when @orderByColumnName = 'monto_anticipado'     and @orderByAscDesc = 'desc' then monto_anticipado end DESC  
					   --, case when @orderByColumnName = 'monto_anticipado_moneda' and @orderByAscDesc = 'asc'  then monto_anticipado_moneda end ASC  
					   --   , case when @orderByColumnName = 'monto_anticipado_moneda' and @orderByAscDesc = 'desc' then monto_anticipado_moneda end DESC  
					   , case when @orderByColumnName = 'giro_real'    and @orderByAscDesc = 'asc'  then giro_real end ASC  
						  , case when @orderByColumnName = 'giro_real'    and @orderByAscDesc = 'desc' then giro_real end DESC  
					   --, case when @orderByColumnName = 'giro_real_moneda'  and @orderByAscDesc = 'asc'  then giro_real_moneda end ASC  
					   --   , case when @orderByColumnName = 'giro_real_moneda'  and @orderByAscDesc = 'desc' then giro_real_moneda end DESC  
					   , case when @orderByColumnName = 'estado_operacion'  and @orderByAscDesc = 'asc'  then estado_operacion end ASC  
						  , case when @orderByColumnName = 'estado_operacion'  and @orderByAscDesc = 'desc' then estado_operacion end DESC    
					OFFSET @inicio ROWS FETCH NEXT @fin ROWS ONLY";
            
            object param = new { rut_cli = rut, numero_operacion = operationNumber, fecha_desde = fromDate, fecha_hasta = toDate, pagina = page, registros = pageSize, orderByAscDesc = order, orderByColumnName = orderBy };
            (string, object) result = (query, param);

            return result;
        }
    }
}
