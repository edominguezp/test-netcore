
namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the channels
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa los canales
    /// </summary>
    public class ChannelResource
    {
        /// <summary>
        /// Channel code
        /// </summary>
        /// <summary xml:lang="es">
        /// Código del canal
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Channel description
        /// </summary>
        /// <summary xml:lang="es">
        /// Descripción del canal
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Query to obtain channels
        /// </summary>
        /// <summary xml:lang="es">
        /// Consulta para obtener los canales
        /// </summary>
        public static string Query_GetChannel()
        {
            var result = $@"                
                SELECT
	                LTRIM(RTRIM(ori.origen)) AS Code,
	                LTRIM(RTRIM(ori.descripcion)) AS Description
                FROM 
	                dbo.origen_operacion ori WITH(NOLOCK)
            ";
            return result;
        }
    }
}
