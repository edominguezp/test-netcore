
namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the zones 
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa las zonas
    /// </summary>
    public class ZoneResource
    {
        /// <summary>
        /// Code of zone
        /// </summary>
        /// <summary xml:lang="es">
        /// Código de la zona
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Name of zone
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre de la zona
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Query to obtain zones by operation status
        /// </summary>
        /// <summary xml:lang="es">
        /// Consulta para obtener las zonas por el estado de las operaciones
        /// </summary>
        public static (string, object) Query_GetZonesByState(ZoneByOperationStatus request)
        {
            var param = new
            {
                status = request.OperationStatus,
                @operation_days = request.OperationDays,
                executive_approval = request.IsExecutiveApproval == null ? 
                                    null : (request.IsExecutiveApproval == true ? "S" : "N")
            };

            //TODO
            //validar la lista de operaciones, revisar si es nula inicializar y devolver otra consulta que no incluya dentro de su where la comprobación del estado
            var query = $@"
                DECLARE @ld_fec_consulta DATETIME
                SET @ld_fec_consulta = DATEADD(day, -@{nameof(param.@operation_days)}, GETDATE() )
                
                SELECT
	                zo.codigo_zona AS Code,
	                LTRIM(RTRIM(zo.nombre_zona)) AS Name
                FROM 
	                dba.tb_fin17 ope WITH (NOLOCK)
                INNER JOIN 
	                dba.tb_fin44 suc WITH (NOLOCK) ON ope.codigo_sucursal = suc.codigo_sucursal
                INNER JOIN 
	                dba.tb_zona zo WITH (NOLOCK) ON suc.codigo_zona = zo.codigo_zona
                LEFT JOIN (
	                SELECT 
		                tarea_actual,
		                num_op,
		                vb_ejecutivo 
	                FROM 
		                dbo.credito WITH (NOLOCK)
                ) CRED ON (OPE.numero_operacion = CRED.num_op)
                WHERE   
	                ope.estado_operacion IN @{nameof(param.status)} AND 
	                ope.fecha_operacion >= @ld_fec_consulta AND
	                (@{nameof(param.executive_approval)} IS NULL OR CRED.vb_ejecutivo = @{nameof(param.executive_approval)})
                GROUP BY 
	                zo.codigo_zona,
	                zo.nombre_zona 
                ";
            
            (string, object) result = (query, param);
            return result;
        }

        /// <summary>
        /// Query to get zones
        /// </summary>
        /// <summary xml:lang="es">
        /// Consulta para obtener las zona
        /// </summary>
        public static string Query_GetZones()
        {
            var result = $@"                
                SELECT
	                zo.codigo_zona AS Code,
	                LTRIM(RTRIM(zo.nombre_zona)) AS Name
                FROM 
	                dba.tb_zona zo WITH(NOLOCK)                
            ";

            return result;
        }
    }
}
