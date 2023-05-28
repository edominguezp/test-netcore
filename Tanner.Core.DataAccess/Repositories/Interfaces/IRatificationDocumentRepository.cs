using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.DataAccess.Repositories.Interfaces
{
    public interface IRatificationDocumentRepository
    {
        /// <summary>
        /// Update ratification status
        /// </summary>
        /// <summary lang="es">
        /// Actualizar el estado de ratificación
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns>Operation response code</returns>
        Task<OperationBaseResult> UpdateRatificationStatus(UpdateRatificationStatus request);
    }
}
