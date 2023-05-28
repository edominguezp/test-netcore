using Tanner.Core.DataAccess.Enums;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the Register approved
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa el registro del visto bueno
    /// </summary>
    public class RegisterApprovedParameters
    {
        /// <summary>
        /// Operation number
        /// </summary>
        /// <summary xml:lang="es">
        /// Número de operación
        /// </summary>
        public int OperationNumber { get; set; }

        /// <summary>
        /// Login Core 
        /// </summary>
        /// <summary xml:lang="es">
        /// Login del inicio de sesión en Core
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Approved state      
        /// </summary>
        /// <summary xml:lang="es">
        /// Estado del visto bueno
        /// </summary>
        public ApprovedState ApprovedState { get; set; }
    }
}
