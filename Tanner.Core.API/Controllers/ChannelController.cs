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
    /// Channel controller
    /// </summary>
    public class ChannelController : BaseController<ChannelController>
    {        
        private readonly IChannelRepository _channelRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="mediator"></param>
        /// <param name="channelRepository"></param>
        public ChannelController(ILogger<ChannelController> logger, IMediator mediator, IChannelRepository channelRepository) : base(logger, mediator)
        {
            _channelRepository = channelRepository;
        }

        /// <summary>
        /// Get all channels
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener todos los canales
        /// </summary>
        /// <returns>All data of channels</returns>
        [ProducesResponseType(typeof(CollectionResult<ChannelResource>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<CollectionResult<ChannelResource>>> Get()
        {
            OperationCollectionResult<ChannelResource> result = await _channelRepository.GetChannelAsync();
            return ReturnCode(result);
        }
    }
}
