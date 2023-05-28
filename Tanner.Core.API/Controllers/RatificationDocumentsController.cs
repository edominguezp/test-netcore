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
    /// Ratification Document controller
    /// </summary>
    public class RatificationDocumentsController : BaseController<RatificationDocumentsController>
    {
        private readonly IRatificationDocumentRepository _ratificationDocumentRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="mediator"></param>
        /// <param name="ratificationDocumentRepository"></param>

        public RatificationDocumentsController(ILogger<RatificationDocumentsController> logger, IMediator mediator, IRatificationDocumentRepository ratificationDocumentRepository) : base(logger, mediator)
        {
            _ratificationDocumentRepository = ratificationDocumentRepository;
        }

        /// <summary>
        /// Update ratification status
        /// </summary>
        /// <summary xml:lang="es">
        /// Actualizar estado ratificación
        /// </summary>
        /// <param name="request"></param>
        /// <returns> If is ok return 204 </returns>
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPatch]
        [Route("{number}/UpdateRatificationStatus")]
        public async Task<ActionResult> UpdateRatificationStatus(UpdateRatificationStatus request)
        {
            OperationBaseResult result = await _ratificationDocumentRepository.UpdateRatificationStatus(request);
            return ReturnCode(result);
        }
    }
}
