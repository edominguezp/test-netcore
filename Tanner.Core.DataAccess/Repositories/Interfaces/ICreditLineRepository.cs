using System.Threading.Tasks;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.DataAccess.Repositories.Interfaces
{
    public interface ICreditLineRepository
    {
        /// Get client credit line 
        /// </summary>
        /// <summary lang="es">
        /// Obtener la linea de credito del cliente
        /// </summary>
        /// <param name="rut">Client RUT</param>
        /// <returns>Detail credit line by client</returns>
        Task<OperationResult<bool>> AddFile(string rut);
    }
}
