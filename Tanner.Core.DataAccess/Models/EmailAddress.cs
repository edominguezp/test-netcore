namespace Tanner.Core.API.Model
{
    /// <summary>
    /// Dirección de correo electrónico y su nombre
    /// </summary>
    /// <summary lang="es">
    /// Email Address
    /// </summary>
    public class EmailAddress
    {
        /// <summary>
        /// Correo electrónico
        /// </summary>
        /// <summary lang="es">
        /// Email
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Nombre del usuario
        /// </summary>
        /// <summary lang="es">
        /// User name
        /// </summary>
        public string ToName { get; set; }
    }
}
