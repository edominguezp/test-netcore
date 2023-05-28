using System.Threading.Tasks;
using Tanner.Core.DataAccess.Models;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.DataAccess.Repositories.Interfaces
{
    public interface ILogRepository
    {
        /// <summary>
        /// Add log
        /// </summary>
        /// <param name="logsRequest">log data</param>
        /// <returns>return true if add log</returns>
        Task<OperationResult<bool>> AddLogAsync(LogRequest logsRequest);
    }
}
