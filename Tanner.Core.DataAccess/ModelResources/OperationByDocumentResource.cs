using System;
using System.Collections.Generic;
using System.Text;

namespace Tanner.Core.DataAccess.ModelResources
{
    public class OperationByDocumentResource
    {
        /// <summary>
        /// Operation Number
        /// </summary>
        public int OperationNumber { get; set; }

        public string Origin { get; set; }

        /// <summary>
        /// Query to get the Operation by document data.
        /// </summary>
        /// <param name="docummentNumber"></param>
        /// <param name="clientRut"></param>
        /// <param name="debtorRut"></param>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public static (string, Query_OperationByDocumentModel) Query_OperationByDocument(string docummentNumber,
            string clientRut,
            string debtorRut,
            int productCode)
        {
            string query = $@"
                    SELECT documentos.numero_operacion as {nameof(OperationNumber)}
                    FROM dba.tb_fin24 documentos
                    INNER JOIN dba.tb_fin01 clientes on clientes.codigo_cliente = documentos.codigo_cliente
                    INNER JOIN dba.tb_fin41 personas_clientes on personas_clientes.codigo_persona = clientes.codigo_persona
                    INNER JOIN dba.tb_fin08 terceros on terceros.codigo_tercero = documentos.codigo_tercero
                    INNER JOIN dba.tb_fin41 personas_terceros on personas_terceros.codigo_persona = terceros.codigo_persona
                    INNER JOIN dba.tb_fin80 productos on productos.tipo_documento = documentos.tipo_documento
                    WHERE 1=1 
                    AND documentos.numero_documento = @{nameof(docummentNumber)}
                    AND personas_clientes.rut_persona = @{nameof(clientRut)}
                    AND personas_terceros.rut_persona = @{nameof(debtorRut)}
                    AND productos.codigo_producto = @{nameof(productCode)};";

            Query_OperationByDocumentModel parameters = new Query_OperationByDocumentModel
            {
                docummentNumber = docummentNumber,
                clientRut  = clientRut,
                debtorRut = debtorRut,
                productCode = productCode
            };

            (string, Query_OperationByDocumentModel) result = (query, parameters);
            return result;
        }

        public class Query_OperationByDocumentModel
        {
            public string docummentNumber { get; set; }
            public string clientRut { get; set; }
            public string debtorRut { get; set; }
            public int productCode { get; set; }
        }
    }
}
