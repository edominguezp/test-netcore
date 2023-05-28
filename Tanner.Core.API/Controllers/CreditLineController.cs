using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.Commands;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.API.Controllers
{
    /// <summary>
    /// Credit line controller
    /// </summary>
    public class CreditLineController : BaseController<CreditLineController>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IFileRepository _fileRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="mediator"></param>
        /// <param name="clientRepository"></param>
        /// <param name="fileRepository"></param>
        public CreditLineController(ILogger<CreditLineController> logger, IMediator mediator, IClientRepository clientRepository, IFileRepository fileRepository) : base(logger, mediator)
        {
            _clientRepository = clientRepository;
            _fileRepository = fileRepository;
        }

        /// <summary>
        /// Add File CORE
        /// </summary>
        /// <summary xml:lang="es">
        /// Agregar un archivo a CORE
        /// </summary>       
        /// <returns>Address Client</returns>
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [Route("AddFile")]
        public async Task<ActionResult> AddAddressClient([FromBody] AddFileCommand command)
        {
            OperationResult<FileResource> result = await Mediator.Send(command);
            return ReturnCode(result);
        }
    }
}
