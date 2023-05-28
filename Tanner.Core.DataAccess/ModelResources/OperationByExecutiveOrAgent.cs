using System.ComponentModel.DataAnnotations;
using Tanner.Core.DataAccess.Enums;
using Tanner.RelationalDataAccess.Domain;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Request to search operations by Executive or Agent
    /// </summary>
    public class OperationByExecutiveOrAgent
    {
        /// <summary>
        /// Employee email
        /// </summary>
        /// <summary lang="es">
        /// Correo electrónico del empleado
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Paginate
        /// </summary>
        /// <summary lang="es">
        /// Paginado
        /// </summary>
        public TannerPaginate Paginate { get; set; }

        /// <summary>
        /// The operation number. It is an optional filter.
        /// </summary>
        /// <summary lang="es">
        /// El número de operación. Es un filtro opcional.
        /// </summary>
        public int? OperationNumber { get; set; }

        /// <summary>
        /// Operations Status
        /// </summary>
        /// <summary lang="es">
        /// Estados de las operaciones a devolver
        /// </summary>
        [Required]
        public OperationState[] Status { get; set; }

        /// <summary>
        /// If operation has executive approval.
        /// If value is null then return all. Operation with executive approval N or S
        /// </summary>
        /// <summary lang="es">
        /// Si cuenta con visto bueno ejecutivo
        /// </summary>
        public bool? IsExecutiveApproval { get; set; }

        /// <summary>
        /// Operation days
        /// </summary>
        /// <summary lang="es">
        /// Días de la operación
        /// </summary>
        public int Days { get; set; }
    }
}
