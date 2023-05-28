
namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the operation types
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa los tipos de operaciones
    /// </summary>
    public class OperationTypeResource
    {
        /// <summary>
        /// Operation type code
        /// </summary>
        /// <summary xml:lang="es">
        /// Código del tipo de operación
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Operation type description
        /// </summary>
        /// <summary xml:lang="es">
        /// Descripción del tipo de operación
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Query to obtain operation types
        /// </summary>
        /// <summary xml:lang="es">
        /// Consulta para obtener los tipos de operaciones
        /// </summary>
        public static string Query_GetOperationType()
        {
            var result = $@"                
                SELECT 
	                LTRIM(RTRIM(codigo_tipo_operacion)) AS Code,
	                LTRIM(RTRIM(descripcion_tipo_operacion)) AS Description
                FROM 
	                dbo.tipo_operacion WITH(NOLOCK)
            ";
            return result;
        }
    }
}
