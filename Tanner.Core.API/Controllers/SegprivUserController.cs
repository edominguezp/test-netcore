using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.Models.Segpriv;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;
using Tanner.Core.Service.Dtos;
using Tanner.Core.Service.Interfaces;

namespace Tanner.Core.API.Controllers
{
    /// <summary>
    /// Segpriv user controller
    /// </summary>
    public class SegprivUserController : BaseController<SegprivUserController>
    {
        private readonly ISegprivUserService _pricingService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="mediator"></param>
        /// <param name="pricingService"></param>
        public SegprivUserController(ILogger<SegprivUserController> logger, IMediator mediator, ISegprivUserService pricingService) : base(logger, mediator)
        {
            _pricingService = pricingService;
        }

        /// <summary>
        /// Find all the non-blocked and non-absent commercial managers.
        /// </summary>
        /// <returns></returns>
        [HttpGet("comercialManagers")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<CollectionResult<CommercialManagerDto>>> GetCommercialManagers()
        {
            var comercialManagersResult = await _pricingService.GetCommercialManagersAsync();
            return ReturnCode(comercialManagersResult);
        }
    }
}
