using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.RelationalDataAccess;

namespace Tanner.Core.DataAccess.Repositories.Interfaces
{
    /// <summary>
    /// Interface to Commercial Hierarchy
    /// </summary>
    public interface ICommercialHierarchyRepository : ICoreRepository
    {
        /// <summary>
        /// Get Commercial Hierarchy by email
        /// </summary>
        /// <summary lang="es">
        /// Obtener la jerarquia comercial en base a un correo de ejecutivo
        /// </summary>
        /// <param name="email">Employee email</param>
        /// <returns>Commercial Hierachy</returns>
        Task<BusinessHierarchyResource> GetCommercialHierarchyByEmailAsync(string email);
    }
}
