using System.Collections.Generic;
using Tanner.Core.DataAccess.Models;

namespace Tanner.Core.DataAccess.ModelResources
{
    public class OperationDocumentByIDResource
    {
		public int Total { get; set; }

		public IEnumerable<OperationDocumentByID> Documents { get; set; }

		public static (string, object) Query_OperationDocumentByID(long operationNumber, int? page, int? pageSize)
		{
			var query = @"DECLARE @inicio INT = NULL, @fin INT = NULL
​
				SET @inicio = ISNULL(@pagina, 0) * ISNULL(@registros, 10)
				SET @fin = ISNULL(@registros, 10)

				--declaramos tabla de salida​
				DECLARE @tabla_salida table 
				(
				 RUT_DEUDOR char(10)
					 , NOMBRE_DEUDOR  varchar(150)
					 , FECHA_VENCIMIENTO char(8)
					 , FECHA_VENCIMIENTO_DATETIME datetime
					 , NUMERO_DOCUMENTO char(20)
					 , MONTO_DOCUMENTO  numeric(16, 4)
					 , MONTO_ANTICIPADO   numeric(16, 4)
					 , DIFERENCIA_PRECIO   numeric(16, 4)
					 , PRECIO_COMPRA    numeric(16, 4)
					 , SALDO   numeric(16, 4) )


				INSERT INTO @tabla_salida​
				SELECT   PDEU.rut_persona												AS RUT_DEUDOR
							, PDEU.razon_social												AS NOMBRE_DEUDOR 
							, CONVERT(CHAR(8), DOC.fecha_vencimiento_documento, 112)		AS FECHA_VENCIMIENTO 
							, DOC.fecha_vencimiento_documento								AS FECHA_VENCIMIENTO_DATETIME 
							, DOC.NUMERO_DOCUMENTO 											AS NUMERO_DOCUMENTO 
							, DOC.valor_nominal_mx											AS MONTO_DOCUMENTO 
							, DOC.valor_futuro_mx											AS MONTO_ANTICIPADO 
							, ( isnull(DOC.interes_mx,0) + isnull(DOC.int_vencido_mx,0) )	AS DIFERENCIA_PRECIO 
							, CONVERT(NUMERIC(11),CASE WHEN ope.codigo_moneda = 1 THEN 
								(doc.VALOR_NOMINAL_DOCUMENTO-doc.INTERES_DOCUMENTO) ELSE 
								(doc.VALOR_NOMINAL_MX-doc.INTERES_MX) END)					AS PRECIO_COMPRA 
							, ( doc.valor_futuro_documento - doc.ABONO_CAPITAL - doc.ABONO_INTERES - IsNull( doc.abono_interes_vencido, 0 ) ) AS SALDO 
						FROM DBA.TB_FIN17 OPE WITH (NOLOCK)
							INNER JOIN DBA.TB_FIN24 DOC  WITH (NOLOCK) ON OPE.NUMERO_OPERACION = DOC.NUMERO_OPERACION 
							INNER JOIN DBA.TB_FIN08 DEU  WITH (NOLOCK) ON DOC.CODIGO_TERCERO  = DEU.CODIGO_TERCERO 
							INNER JOIN DBA.TB_FIN41 PDEU WITH (NOLOCK) ON DEU.CODIGO_PERSONA  = PDEU.CODIGO_PERSONA 
						WHERE	ope.numero_operacion = @numero_operacion 
						AND doc.estado_documento not in( 7 , 8 , 9 , 99 )
					

				--SALIDA1​
				Select count(1) Total From @tabla_salida​


				--SALIDA2
				SELECT dbo.f_formatea_rut_azure(RUT_DEUDOR) AS DebtorRUT
					 , RTRIM(LTRIM(NOMBRE_DEUDOR)) AS DebtorName
					 , FECHA_VENCIMIENTO AS ExpiryDateString
					 , FECHA_VENCIMIENTO_DATETIME AS ExpiryDate
					 , RTRIM(LTRIM(NUMERO_DOCUMENTO)) AS DocumentNumber
					 , MONTO_DOCUMENTO AS DocumentAmount
					 , MONTO_ANTICIPADO AS AdvancedAmount
					 , DIFERENCIA_PRECIO AS PriceDifference
					 , PRECIO_COMPRA AS PurchasePrice
					 , SALDO AS Balance
				FROM @tabla_salida
				 order by  NUMERO_DOCUMENTO  ASC
					OFFSET @inicio ROWS FETCH NEXT @fin ROWS ONLY";

			object param = new { numero_operacion = operationNumber, pagina = page, registros = pageSize };
			(string, object) result = (query, param);

			return result;
		}
	}
}
