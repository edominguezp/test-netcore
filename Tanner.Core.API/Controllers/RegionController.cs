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
    /// Region controller
    /// </summary>
    public class RegionController: BaseController<RegionController>
    {

        private readonly IRegionRepository _RegionRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="mediator"></param>
        /// <param name="regionRepository"></param>
        public RegionController(ILogger<RegionController> logger, IMediator mediator, IRegionRepository regionRepository) : base(logger, mediator)
        {
            _RegionRepository = regionRepository;
        }

        /// <summary>
        /// Get regions 
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener las regiones
        /// </summary>
        /// <returns>Regions</returns>
        [ProducesResponseType(typeof(CollectionResult<RegionResource>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("/Regions")]
        public async Task<ActionResult<CollectionResult<RegionResource>>> GetRegions()
        {
            OperationCollectionResult<RegionResource> result = await _RegionRepository.GetRegionsAsync();
            return ReturnCode(result);

        }

        /// <summary>
        /// Get communes by region ID
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtiene las comunas de acuerdo al identificador de la región
        /// /// </summary>
        /// <returns>Communes by region</returns>
        [ProducesResponseType(typeof(CollectionResult<CommuneResource>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{id}/Communes")]
        public async Task<ActionResult<CollectionResult<CommuneResource>>> GetCommunes([FromRoute] int id)
        {
            OperationCollectionResult<CommuneResource> result = await _RegionRepository.GetCommunesByRegionAsync(id);
            return ReturnCode(result);
        }
    }
}
