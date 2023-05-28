using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.DataAccess.Repositories.Interfaces
{
    /// <summary>
    /// Interface to banks
    /// </summary>
    /// <summary xml:lang="es">
    /// Interfaz para los bancos
    /// </summary>
    public interface IBankRepository
    {
        /// <summary>
        /// Get Banks
        /// </summary>
        /// <summary lang="es">
        /// Obtener los bancos
        /// </summary>
        /// <returns> Banks </returns>
        Task<OperationCollectionResult<BankResource>> GetBanksAsync();
   }
}
