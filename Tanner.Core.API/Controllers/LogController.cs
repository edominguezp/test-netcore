using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tanner.Core.DataAccess.Models;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.API.Controllers
{
    /// <summary>
    /// Manager log
    /// </summary>
    public class LogController : BaseController<LogController>
    {
        private readonly ILogRepository _logRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="mediator"></param>
        /// <param name="logRepository"></param>
        public LogController(ILogger<LogController> logger, IMediator mediator, ILogRepository logRepository) : base(logger, mediator)
        {
            _logRepository = logRepository;
        }

        /// <summary>
        /// Add log
        /// </summary>
        /// <param name="request">Log data</param>
        /// <returns>Return true if add log</returns>
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<bool>> AddLog([FromBody] LogRequest request)
        {
            OperationResult<bool> result = await _logRepository.AddLogAsync(request);
            return ReturnCode(result);
        }
    }
}
