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
    /// Branch Office controller
    /// </summary>
    public class BranchOfficeController : BaseController<BranchOfficeController>
    {        
        private readonly IBranchOfficeRepository _branchOfficeRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="mediator"></param>
        /// <param name="branchOfficeRepository"></param>
        public BranchOfficeController(ILogger<BranchOfficeController> logger, IMediator mediator, IBranchOfficeRepository branchOfficeRepository) : base(logger, mediator)
        {
            _branchOfficeRepository = branchOfficeRepository;
        }

        /// <summary>
        /// Get all branch offices
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener todas las sucursales
        /// </summary>
        /// <returns>All data of branch offices</returns>
        [ProducesResponseType(typeof(CollectionResult<BranchOfficeResource>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<CollectionResult<BranchOfficeResource>>> Get()
        {
            OperationCollectionResult<BranchOfficeResource> result = await _branchOfficeRepository.GetBranchOfficeAsync();
            return ReturnCode(result);
        }
    }
}
