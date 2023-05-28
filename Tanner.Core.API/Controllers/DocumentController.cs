using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Models;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.API.Controllers
{
    /// <summary>
    /// Document API
    /// </summary>
    public class DocumentController : BaseController<DocumentController>
    {
        private readonly IDocumentRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="mediator"></param>
        /// <param name="repository"></param>
        public DocumentController(ILogger<DocumentController> logger, IMediator mediator, IDocumentRepository repository) : base(logger, mediator)
        {
            _repository = repository;
        }


        /// <summary>
        /// Get detail operations by document number
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtiene el detalle de las operaciones dado el número de un documento
        /// </summary>
        /// <returns>Operations details</returns>
        [ProducesResponseType(typeof(CollectionResult<DataOperationResource>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{number}/Operations")]
        public async Task<ActionResult<CollectionResult<DataOperationResource>>> GetOperations([FromRoute]string number, string rut)
        {
            OperationCollectionResult<DataOperationResource> result = await _repository.GetOperationsbyDocumentNumberAsync(number, rut);
            return ReturnCode(result);
        }

        /// <summary>
        /// Update the grant status of the document
        /// </summary>
        /// <summary xml:lang="es">
        /// Actualizar el estado de cesión del documento
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPatch]
        [Route("grant/status")]
        public async Task<ActionResult> UpdateGrantDocument([FromBody] DocumentGrantRequest request)
        {
            OperationBaseResult result = await _repository.UpdateGrantDocumentAsync(request);
            return ReturnCode(result);
        }

        /// <summary>
        /// Get day document
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener dias de documento
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("days")]
        public async Task<ActionResult> GetDayDocument([FromQuery]  DateTime expiryDate, string product)
        {
            OperationResult<int> result = await _repository.GetDayDocumentAsync(expiryDate, product);
            return ReturnCode(result);
        }
    }
}
