using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.DataAccess.Repositories.Interfaces
{
    /// <summary>
    /// Interface to assignment contract
    /// </summary>
    /// <summary xml:lang="es">
    /// Interfaz para los bancos
    /// </summary>
    public interface IAssignmentContractRepository
    {
        /// <summary>
        /// Get assignment contract
        /// </summary>
        /// <param name="operationNumber">Operation number</param>
        /// <returns>Return assignment contract data</returns>
        Task<OperationResult<AssignmentContractResource>> GetAssignmentContractAsync(long operationNumber);
    }
}
