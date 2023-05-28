using Tanner.Core.DataAccess.Enums;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represents the operation status
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa el stado de una operación
    /// </summary>
    public class OperationStatusResource
    {
        /// <summary>
        /// Operations number
        /// </summary>
        /// <summary lang="es">
        /// Número de la operación
        /// </summary>
        public long Number { get; set; }

        /// <summary>
        /// Operations status
        /// </summary>
        /// <summary lang="es">
        /// Estado de la operación
        /// </summary>
        public OperationState Status { get; set; }

        /// <summary>
        /// Current task
        /// </summary>
        /// <summary lang="es">
        /// Tarea actual
        /// </summary>
        public string CurrentTask { get; set; }

        /// <summary>
        /// If operation has executive approval.
        /// </summary>
        /// <summary lang="es">
        /// Si cuenta con visto bueno ejecutivo.
        /// </summary>
        public bool IsExecutiveApproval { get; set; }
    }
}
