using System.Threading.Tasks;
using Tanner.Core.DataAccess.Results;
using Tanner.Core.Service.Dtos;

namespace Tanner.Core.Service.Interfaces
{
    public interface ISegprivUserService
    {
        Task<OperationCollectionResult<CommercialManagerDto>> GetCommercialManagersAsync();
    }
}
