
using Microsoft.Extensions.Configuration;

namespace Tanner.Core.API.Infrastructure.Configurations
{
    /// <summary>
    /// Represent a cors configuration
    /// </summary>
    public class CorsConfiguration
    {
        /// <summary>
        /// Section name to cors configuration
        /// </summary>
        /// <summary lang="es">
        /// Nombre de la sección de configuración para CORS
        /// </summary>
        public static string SectionName = nameof(CorsConfiguration);

        /// <summary>
        /// Origins allowed
        /// </summary>
        /// <summary lang="es">
        /// Origenes permitidos
        /// </summary>
        public string[] Origins { get; set; }
    }
}
