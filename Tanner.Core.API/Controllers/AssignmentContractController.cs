using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.API.Controllers
{
    /// <summary>
    /// Assignment Contract
    /// </summary>
    public class AssignmentContractController : BaseController<AssignmentContractController>
    {
        private readonly ILogger<AssignmentContractController> _logger;
        private readonly IAssignmentContractRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository">Repository</param>
        /// <param name="logger">Logger instance</param>
        /// <param name="mediator"></param>
        public AssignmentContractController(IAssignmentContractRepository repository, ILogger<AssignmentContractController> logger, IMediator mediator) : base(logger, mediator)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Get assignment contract data
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener datos del contrato de cesión
        /// </summary>
        /// <returns>assignment contract data</returns>
        [ProducesResponseType(typeof(AssignmentContractResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("operation/{number}")]
        public async Task<ActionResult<AssignmentContractResource>> GetAssignmentContract([FromRoute] long number)
        {
            OperationResult<AssignmentContractResource> result = await _repository.GetAssignmentContractAsync(number);
            return ReturnCode(result);
        }
    }
}
