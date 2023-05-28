using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Results;
using Tanner.RelationalDataAccess.Domain;

namespace Tanner.Core.DataAccess.Repositories.Interfaces
{
    public interface IFileRepository : ITannerRepository
    {
        Task<OperationResult<FileResource>> GetFileByID(decimal number, int id);
    }
}
