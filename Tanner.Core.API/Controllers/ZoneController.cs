using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.Enums;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Repositories;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.API.Controllers
{
    /// <summary>
    /// Zone controller
    /// </summary>
    public class ZoneController : BaseController<ZoneController>
    {        
        private readonly IZoneRepository _zoneRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="mediator"></param>
        /// <param name="zoneRepository"></param>
        public ZoneController(ILogger<ZoneController> logger, IMediator mediator, IZoneRepository zoneRepository) : base(logger, mediator)
        {
            _zoneRepository = zoneRepository;
        }
        
        /// <summary>
        /// Get branch office that include operation with one or more states
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener las sucursales que incluyen operaciones con uno o más estados
        /// </summary>
        /// <returns>All data of zone</returns>
        [ProducesResponseType(typeof(CollectionResult<BranchOfficeResource>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{id}/BranchOffice")]
        public async Task<ActionResult<CollectionResult<BranchOfficeResource>>> GetBranchOffice([FromQuery] IEnumerable<OperationState> statesOperation, [FromQuery] int daysOperations, [FromRoute] int id)
        {
            OperationCollectionResult<BranchOfficeResource> result = await _zoneRepository.GetBranchOfficeByZoneAndState(statesOperation, daysOperations, id);
            return ReturnCode(result);

        }

        /// <summary>
        /// Get zones that include operation with one or more states
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener las zonas que incluyen operaciones con uno o más estados
        /// </summary>
        /// <returns>All data of zone</returns>
        [ProducesResponseType(typeof(CollectionResult<ZoneResource>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("ByStatus")]
        public async Task<ActionResult<CollectionResult<ZoneResource>>> GetZones([FromQuery] ZoneByOperationStatus request)
        {
            OperationCollectionResult<ZoneResource> result = await _zoneRepository.GetZonesByState(request);
            return ReturnCode(result);
        }
        
        /// <summary>
        /// Get all zones
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener todas las zonas
        /// </summary>
        /// <returns>All data of zone</returns>
        [ProducesResponseType(typeof(CollectionResult<ZoneResource>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<CollectionResult<ZoneResource>>> Get()
        {
            OperationCollectionResult<ZoneResource> result = await _zoneRepository.GetZonesAsync();
            return ReturnCode(result);
        }
    }
}
