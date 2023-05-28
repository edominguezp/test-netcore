using Microsoft.OpenApi.Models;

namespace Tanner.Core.API.Infrastructure.Configurations
{
    /// <summary>
    /// Swagger Configuration
    /// </summary>
    /// <summary xml:lang="es">
    /// Configuración del swagger  
    /// </summary>
    public class SwaggerConfiguration
    {
        /// <summary>
        /// Tittle of endpoint
        /// </summary>
        /// <summary xml:lang="es">
        /// Título del endpoint     
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description of endpoint
        /// </summary>
        /// <summary xml:lang="es">
        /// Descripción del endpoint     
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// URL of endpoint
        /// </summary>
        /// <summary xml:lang="es">
        /// URL del endpoint     
        /// </summary>
        public string EndpointUrl { get; set; }

        /// <summary>
        /// Description of endpoint
        /// </summary>
        /// <summary xml:lang="es">
        /// Descripción del endpoint     
        /// </summary>
        public string EndpointDescription { get; set; }

        /// <summary>
        /// Version of endpoint
        /// </summary>
        /// <summary xml:lang="es">
        /// Versión del endpoint     
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Create new Api Info
        /// </summary>
        /// <summary xml:lang="es">
        /// Crea un nuevo objeto para ocupar en el  swagger extensión
        /// </summary>
        public OpenApiInfo GetOpenApiInfo()
        {
            var result = new OpenApiInfo
            {
                Title = EndpointDescription,
                Version = Version
            };
            return result;
        }
    }
}
