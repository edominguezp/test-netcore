using System;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the documents by operation number
    /// </summary>s
    ///<summary xml:lang="es">
    /// Clase que representa los documentos por número de operación
    /// </summary>
    public class SummaryDocumentResource
    {
         /// <summary>
         /// Document Number
         /// </summary>s
         ///<summary xml:lang="es">
         /// Número de documento
         /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Debtor RUT
        /// </summary>
        ///<summary xml:lang="es">
        /// RUT del deudor
        /// </summary>
        public string DebtorRUT { get; set; }

        /// <summary>
        /// Debtor Name
        /// </summary>
        ///<summary xml:lang="es">
        /// Nombre del deudor
        /// </summary>
        public string DebtorName { get; set; }

        /// <summary>
        /// Document Amount
        /// </summary>
        ///<summary xml:lang="es">
        /// Monto del documento
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Expired date
        /// </summary>
        ///<summary xml:lang="es">
        /// Fecha de vencimiento del documento
        /// </summary>
        public DateTime ExpiredDate { get; set; }

        public static (string, object) Query_GetDataDocumentsByOperation(int number)
        {
            var query = $@"
	                SELECT
			                LTRIM(RTRIM(doc.numero_documento)) as Number,
			                LTRIM(RTRIM(ISNULL(deu.razon_social,deu.nombre_tercero))) as DebtorName,
			                Replace(LTrim(Replace(per.rut_persona, '0', ' ')), ' ', 0) as DebtorRUT,
							doc.valor_nominal_documento as Amount,
			                doc.fecha_vencimiento_documento as ExpiredDate
	                FROM
			                dba.tb_fin24 doc WITH (NOLOCK)
			                JOIN
				                dba.tb_fin08 deu WITH (NOLOCK) ON doc.codigo_tercero = deu.codigo_tercero
			                JOIN
				                dba.tb_fin41 per WITH (NOLOCK) ON deu.codigo_persona = per.codigo_persona
			                LEFT JOIN
				                dba.tb_fin01 cli WITH (NOLOCK) ON per.codigo_persona = cli.codigo_persona
			                JOIN
				                dba.tb_fin17 op WITH (NOLOCK) ON doc.numero_operacion = op.numero_operacion
	                WHERE
			                op.numero_operacion = @{nameof(number)} AND
			                op.estado_operacion IN (0, 1, 2) AND
			                doc.estado_documento IN (0, 1, 2)
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
