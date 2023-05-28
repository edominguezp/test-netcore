using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.DataAccess.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface to operation types
    /// </summary>
    /// <summary xml:lang="es">
    /// Interfaz del repositorio para los tipos de operaciones
    /// </summary>
    public interface IOperationTypeRepository : ICoreRepository
    {
        /// <summary>
        /// Get all operation types
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener todos los tipos de operaciones
        /// </summary>
        /// <returns>All data of operation types</returns>
        Task<OperationCollectionResult<OperationTypeResource>> GetOperationTypeAsync();
    }
    
}
