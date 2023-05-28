namespace Tanner.Core.DataAccess.Configurations
{
    /// <summary>
    /// Configuración para Tanner IOF
    /// </summary>
    /// <summary lang="es">
    /// Configuration for Tanner IOF
    /// </summary>
    public class TannerIOFConfiguration
    {
        /// <summary>
        /// Codigo de la configuración
        /// </summary>
        /// <summary lang="es">
        /// Key configuration
        /// </summary>
        public static string Key = "TannerIOFConfiguration";

        /// <summary>
        /// Versión de la API
        /// </summary>
        /// <summary lang="es">
        /// Api Version
        /// </summary>
        public string Version { get; set; }
    }
}