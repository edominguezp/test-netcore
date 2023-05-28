using System;
using System.Collections.Generic;
using Tanner.Core.DataAccess.Models;

namespace Tanner.Core.DataAccess.ModelResources
{
	/// <summary>
	/// Assignment contract
	/// </summary>
	///<summary xml:lang="es">
	/// Contrato de cesión
	/// </summary>
	public class AssignmentContractResource
    {
		/// <summary>
		/// Document type
		/// </summary>
		///<summary xml:lang="es">
		/// Tipo de documento
		/// </summary>
		public string DocumentType { get; set; }

		/// <summary>
		/// Currency
		/// </summary>
		///<summary xml:lang="es">
		/// Moneda
		/// </summary>
		public string Currency { get; set; }

		/// <summary>
		/// Client name
		/// </summary>
		///<summary xml:lang="es">
		/// Nombre del cliente
		/// </summary>
		public string ClientName { get; set; }

		/// <summary>
		/// Client RUT
		/// </summary>
		///<summary xml:lang="es">
		/// RUT del cliente
		/// </summary>
		public string ClientRUT { get; set; }

		/// <summary>
		/// Operation status
		/// </summary>
		///<summary xml:lang="es">
		/// Estado de la operación
		/// </summary>
		public int OperationStatus { get; set; }

		/// <summary>
		/// Operation status description
		/// </summary>
		///<summary xml:lang="es">
		/// Descripción del estado de la operación
		/// </summary>
		public string OperationStatusDescription { get; set; }

		/// <summary>
		/// Grant date
		/// </summary>
		///<summary xml:lang="es">
		/// Fecha de cesión
		/// </summary>
		public DateTime GrantDate { get; set; }

		/// <summary>
		/// Amount documents
		/// </summary>
		///<summary xml:lang="es">
		/// Monto de documentos
		/// </summary>
		public decimal AmountDocuments { get; set; }

		/// <summary>
		/// Advance amount
		/// </summary>
		///<summary xml:lang="es">
		/// Monto anticipado
		/// </summary>
		public decimal AdvancedAmount { get; set; }

		/// <summary>
		/// Bank draft
		/// </summary>
		///<summary xml:lang="es">
		/// giro bancario
		/// </summary>
		public decimal BankDraft { get; set; }

		/// <summary>
		/// Documents number
		/// </summary>
		///<summary xml:lang="es">
		/// Cantidad de documentos
		/// </summary>
		public int DocumentsNumber { get; set; }

		/// <summary>
		/// Status AWO
		/// </summary>
		///<summary xml:lang="es">
		/// Estado del AWO
		/// </summary>
		public int StatusAWO { get; set; }

		/// <summary>
		/// Status AWO description
		/// </summary>
		///<summary xml:lang="es">
		/// Descripción del estado de AWO
		/// </summary>
		public string StatusAWODescription { get; set; }

		/// <summary>
		/// Operation origin
		/// </summary>
		///<summary xml:lang="es">
		/// Origen de la operación
		/// </summary>
		public string OperationOrigin { get; set; }

		/// <summary>
		/// Operation origin description
		/// </summary>
		///<summary xml:lang="es">
		/// Descripción del origen de la operación
		/// </summary>
		public string OperationOriginDescription { get; set; }

		/// <summary>
		/// Assignment contract documents
		/// </summary>
		///<summary xml:lang="es">
		/// Documentos del contrato de cesión
		/// </summary>
		public IEnumerable<AssignmentContractDocuments> Documents { get; set; }

		public static (string, object) Query_AssignmentContract(long operationNumber)
		{
			var query = @"---operacion
				SELECT LTRIM(RTRIM(tipo_documento.descripcion_documento)) AS DocumentType,
						LTRIM(RTRIM(monedas.nombre_moneda)) AS Currency,
						LTRIM(RTRIM(clientes.nombre_cliente)) AS ClientName,
						dbo.f_formatea_rut_azure(personas.rut_persona) AS ClientRUT,
						ope.estado_operacion as OperationStatus,
						(CASE 
						WHEN ope.estado_operacion = 0 THEN 'En analisis'
						WHEN ope.estado_operacion = 1 THEN 'Aprobada' 
						WHEN ope.estado_operacion = 2 THEN 'Financiada y Vigente'
						WHEN ope.estado_operacion = 3 THEN 'Financiada y Recaudada'
						END) AS OperationStatusDescription,
						ISNULL(log_evaluacion.fecha_otorgamiento,ope.fecha_operacion) as GrantDate,
						SUM(CONVERT(NUMERIC(11),
							CASE
							WHEN ope.codigo_moneda = 1
							THEN doc.valor_nominal_documento
							ELSE doc.valor_nominal_mx
							END)) AS AmountDocuments,
						SUM(CONVERT(NUMERIC(11),
							CASE
							WHEN ope.codigo_moneda = 1
							THEN doc.VALOR_FUTURO_DOCUMENTO
							ELSE doc.VALOR_FUTURO_MX
							END)) AS AdvancedAmount,
						CONVERT(NUMERIC(11), CRED.monto_giro) as BankDraft,
						COUNT(doc.numero_documento) AS DocumentsNumber,
						MAX(AWO.codigo)  as StatusAWO,
						(CASE 
							WHEN MAX(AWO.codigo) = 1 THEN 'amarillo'
							WHEN MAX(AWO.codigo) = 0 THEN 'verde' 
							WHEN MAX(AWO.codigo) = 2 THEN 'rojo'
							END ) AS StatusAWODescription,
						LTRIM(RTRIM(CRED.Origen)) AS OperationOrigin,
						LTRIM(RTRIM(ORI.Descripcion)) AS OperationOriginDescription
				FROM dba.tb_fin17 ope WITH (NOLOCK)
					INNER JOIN dba.tb_fin61 tipo_documento WITH (NOLOCK) ON ope.tipo_documento = tipo_documento.tipo_documento
					INNER JOIN dba.tb_fin45 monedas WITH (NOLOCK) ON ope.codigo_moneda = monedas.codigo_moneda
					INNER JOIN dba.tb_fin01 clientes WITH (NOLOCK) ON ope.codigo_cliente = clientes.codigo_cliente
					INNER JOIN dba.tb_fin41 personas WITH (NOLOCK)  ON clientes.codigo_persona = personas.codigo_persona
					INNER JOIN DBA.TB_FIN24 DOC WITH (NOLOCK) ON ope.numero_operacion = doc.numero_operacion
					LEFT JOIN (select numero_operacion, MAX(codigo) as codigo from FACTOR_SV.dbo.TBL_reglas_awo_log awo WITH (NOLOCK) where codigo in (0,1)  and awo.numero_operacion = @operacion  group by awo.numero_operacion) awo on ope.numero_operacion = awo.numero_operacion
					LEFT JOIN dbo.credito CRED WITH (NOLOCK) ON ope.numero_operacion = cred.num_op
					LEFT JOIN [dbo].[origen_operacion] ORI WITH (NOLOCK) ON ORI.Origen = cred.Origen
					LEFT JOIN (select num_oper, fyh_log as fecha_otorgamiento from log_evaluacion WITH (NOLOCK) where num_oper = @operacion and cod_accion = 20 and des_accion like '%otorgo%')log_evaluacion on ope.numero_operacion = log_evaluacion.num_oper
				WHERE ope.numero_operacion = @operacion
				and ope.estado_operacion IN (0, 1, 2, 3) 
				and DOC.estado_documento not in( 7 , 8 , 9 , 99 )
				GROUP BY ope.tipo_operacion,
					log_evaluacion.fecha_otorgamiento,
					ope.fecha_operacion,
					ope.numero_operacion,
					ope.cargos_afectos,
					iva_operacion,
					ope.estado_operacion,
					tipo_documento.descripcion_documento,
					monedas.nombre_moneda,
					clientes.nombre_cliente,
					personas.rut_persona,
					awo.numero_operacion,
					CRED.origen,
					CRED.monto_giro,
					ORI.Descripcion

				---documentos
				SELECT documentos.numero_documento as DocumentNumber,
						dbo.f_formatea_rut_azure(personas.rut_persona) as DebtorRUT,
						deudores.nombre_tercero as DebtorName,
						documentos.fecha_emision as EmissionDate,
						documentos.fecha_original as OriginalExpiredDate,
						documentos.fecha_vencimiento_documento as EffectiveExpiredDate,
						CONVERT(NUMERIC(11),documentos.valor_nominal_documento) as AmountDocuments
					FROM dba.tb_fin17 operaciones WITH (NOLOCK)
					INNER JOIN dba.tb_fin24 documentos WITH (NOLOCK) ON documentos.numero_operacion = operaciones.numero_operacion
					INNER JOIN dba.tb_fin61 tipo_documento WITH (NOLOCK) ON operaciones.tipo_documento = tipo_documento.tipo_documento
					INNER JOIN dba.tb_fin08 deudores WITH (NOLOCK) ON documentos.codigo_tercero = deudores.codigo_tercero
					INNER JOIN dba.tb_fin41 personas WITH (NOLOCK) ON deudores.codigo_persona = personas.codigo_persona
				WHERE operaciones.numero_operacion =  @operacion
				and documentos.estado_documento not in( 7 , 8 , 9 , 99 )
				ORDER BY operaciones.numero_operacion DESC;";

			object param = new { operacion = operationNumber };
			(string, object) result = (query, param);

			return result;
		}
	}
}
