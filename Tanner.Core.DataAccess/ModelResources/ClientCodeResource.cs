using System;
using System.Collections.Generic;
using System.Text;

namespace Tanner.Core.DataAccess.ModelResources
{
    public class ClientCodeResource
    {
        public int Code { get; set; }

        /// <summary>
        /// Query to obtain code Client by rut client
        /// </summary>
        /// <summary xml:lang="es">
        /// Consulta para obtener el código del cliente a través de su rut
        /// </summary>
        public static (string, object) Query_GetClientCodeByRut(string clientRut)
        {
            string query = @"
                DECLARE @RUT_CLI			VARCHAR(50),
		                @codigo_cliente     Integer

	                SET @RUT_CLI			= replace(@clientRut,'.','')
	                SET @RUT_CLI			= replace(@RUT_CLI,'-','')
	                SET @RUT_CLI			= right( '0000000000' + @RUT_CLI , 10 )
  
                SELECT 
	                @codigo_cliente = isnull(c.codigo_cliente, 0)
                FROM  dba.tb_fin01 c With (nolock)
                INNER JOIN dba.tb_fin41 p With (nolock) ON c.codigo_persona = p.codigo_persona 
                WHERE p.rut_persona = @RUT_CLI

                SELECT @codigo_cliente
                ";

            var param = new
            {
                clientRut
            };

            (string, object) result = (query, param);

            return result;
        }
    }
}
