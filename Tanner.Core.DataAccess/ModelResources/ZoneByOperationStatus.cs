
using System.ComponentModel.DataAnnotations;
using Tanner.Core.DataAccess.Enums;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Request to search zones by Operation Status
    /// </summary>
    public class ZoneByOperationStatus
    {
        /// <summary>
        /// Operations Status
        /// </summary>
        /// <summary lang="es">
        /// Estados de las operaciones a devolver
        /// </summary>
        [Required]
        public OperationState[] OperationStatus { get; set; }

        /// <summary>
        /// If operation has executive approval.
        /// If value is null then return all. Operation with executive approval N or S
        /// </summary>
        /// <summary lang="es">
        /// Si cuenta con visto bueno ejecutivo la operación
        /// </summary>
        public bool? IsExecutiveApproval { get; set; }
        
        /// <summary>
        /// Operation days
        /// </summary>
        /// <summary lang="es">
        /// Días de la operación
        /// </summary>
        [Required]
        public int OperationDays { get; set; }
    }
}
