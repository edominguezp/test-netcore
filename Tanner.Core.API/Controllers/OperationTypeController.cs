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
    /// Operation type controller
    /// </summary>
    public class OperationTypeController : BaseController<OperationTypeController>
    {        
        private readonly IOperationTypeRepository _operationTypeRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="mediator"></param>
        /// <param name="operationTypeRepository"></param>
        public OperationTypeController(ILogger<OperationTypeController> logger, IMediator mediator, IOperationTypeRepository operationTypeRepository) : base(logger, mediator)
        {
            _operationTypeRepository = operationTypeRepository;
        }

        /// <summary>
        /// Get all operation types
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener todos los tipos de operaciones
        /// </summary>
        /// <returns>All data of operation types</returns>
        [ProducesResponseType(typeof(CollectionResult<OperationTypeResource>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<CollectionResult<OperationTypeResource>>> Get()
        {
            OperationCollectionResult<OperationTypeResource> result = await _operationTypeRepository.GetOperationTypeAsync();
            return ReturnCode(result);
        }
    }
}
