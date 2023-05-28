using System.Collections.Generic;
using System.Linq;
using Tanner.Core.DataAccess.Enums;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the branch office 
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa las sucursales
    /// </summary>
    public class BranchOfficeResource
    {
        /// <summary>
        /// Code of branch Office
        /// </summary>
        /// <summary xml:lang="es">
        /// Código de la sucursal
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Name of branch Office
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre de la sucursal
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Query to get branch offices
        /// </summary>
        /// <summary xml:lang="es">
        /// Consulta para obtener las sucursales
        /// </summary>
        public static string Query_GetBranchOffice()
        {
            var result = $@"                
                SELECT	
	                suc.codigo_sucursal as Code,
	                LTRIM(RTRIM(suc.descripcion_sucursal)) as Name
                FROM 
                    dba.tb_fin44 suc WITH(NOLOCK)
            ";
            return result;
        }

        /// <summary>
        /// Query to obtain branch offices
        /// </summary>
        /// <summary xml:lang="es">
        /// Consulta para obtener las sucursales
        /// </summary>
        public static (string, object) Query_GetBranchOfficeByState(IEnumerable<OperationState> statesOperation, int daysOperations, int zoneCode)
        {
            //TODO
            //validar la lista de operaciones, revisar si es nula inicializar y devolver otra consulta que no incluya dentro de su where la comprobación del estado
            var query = $@"
                DECLARE @ld_fec_consulta DATETIME
                SET @ld_fec_consulta = DATEADD(day, -@{nameof(daysOperations)}, GETDATE() )
	            DECLARE @Zonesuc bigint 
	            SET @Zonesuc = @{nameof(zoneCode)}

                SELECT	
	                suc.codigo_sucursal as Code,
	                ltrim(rtrim(suc.descripcion_sucursal)) as Name
                FROM 
                    dba.tb_fin17 ope with (nolock)	
                    INNER JOIN dba.tb_fin44 suc WITH ( NOLOCK ) ON ope.codigo_sucursal = suc.codigo_sucursal
	                INNER JOIN dba.tb_zona zo   WITH ( NOLOCK ) ON suc.codigo_zona = zo.codigo_zona 
                WHERE   
                    ope.estado_operacion in @{nameof(statesOperation)} and ope.fecha_operacion >= @ld_fec_consulta  and zo.codigo_zona = @Zonesuc
	                group by  suc.codigo_sucursal, suc.descripcion_sucursal
            ";

            var param = new
            {
                statesOperation = statesOperation.Select(t => (int)t),
                daysOperations,
                zoneCode
            };
            (string, object) result = (query, param);
            return result;
        }

        /// <summary>
        /// Query to obtain branch office by code Client
        /// </summary>
        /// <summary xml:lang="es">
        /// Consulta para obtener la sucursal de un cliente por el código del cliente
        /// </summary>
        public static (string, object) Query_GetBranchOfficeByClient(int clientCode)
        {
            var query = @"
                SELECT	
	                suc.codigo_sucursal as Code,
	                LTRIM(RTRIM(suc.descripcion_sucursal)) as Name
                FROM dba.tb_fin44 suc WITH(NOLOCK)
                INNER JOIN dba.tb_fin01 p WITH(NOLOCK) ON suc.codigo_sucursal = p.codigo_sucursal
                WHERE p.codigo_cliente = @clientCode 
                ";

            var param = new
            {
                clientCode
            };

            (string, object) result = (query, param);
            return result;
        }
    }
}
