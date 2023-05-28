using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.DataAccess.Repositories.Interfaces
{
    /// <summary>
    /// Interface to branch offices
    /// </summary>
    /// <summary xml:lang="es">
    /// Interfaz para las sucursales
    /// </summary>
    public interface IBranchOfficeRepository : ICoreRepository
    {
        /// <summary>
        /// Get all branch offices
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener todas las sucursales
        /// </summary>
        /// <returns>All data of branch offices</returns>
        Task<OperationCollectionResult<BranchOfficeResource>> GetBranchOfficeAsync();

        /// <summary>
        /// Get branch office by client code
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtiene la sucursal 
        /// </summary>
        /// <param name="clientCode"></param>
        /// <returns></returns>
        Task<OperationCollectionResult<BranchOfficeResource>> GetBranchOfficeByClientCodeAsync(int clientCode);
    }
    
}
