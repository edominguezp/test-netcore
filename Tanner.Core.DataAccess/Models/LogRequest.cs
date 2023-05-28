using System.ComponentModel.DataAnnotations;

namespace Tanner.Core.DataAccess.Models
{
    /// <summary>
    /// Logs
    /// </summary>
    public class LogRequest
    {
        /// <summary>
        /// Operation number
        /// </summary>
        /// <summary xml:lang="es">
        /// Número de operación
        /// </summary>
        [Required]
        public long OperationNumber { get; set; }

        /// <summary>
        /// Action code
        /// </summary>
        /// <summary xml:lang="es">
        /// Código de acción
        /// </summary>
        [Required]
        public int ActionCode { get; set; }

        /// <summary>
        /// Action description
        /// </summary>
        /// <summary xml:lang="es">
        /// Descripción de la acción
        /// </summary>
        [Required]
        public string ActionDescription { get; set; }

        /// <summary>
        /// Login
        /// </summary>
        /// <summary xml:lang="es">
        /// Inicio de cesión 
        /// </summary>
        [Required]
        public string Login { get; set; }
    }
}
