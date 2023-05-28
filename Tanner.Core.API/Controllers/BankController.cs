using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Tanner.Core.API.Helpers;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.API.Controllers
{
    /// <summary>
    /// Bank controller
    /// </summary>
    [Route("Bank")]
    public class BankController : BaseController<BankController>
    {

        private readonly IBankRepository _bankRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="mediator"></param>
        /// <param name="bankRepository"></param>
        public BankController(ILogger<BankController> logger, IMediator mediator, IBankRepository bankRepository) : base(logger, mediator)
        {
            _bankRepository = bankRepository;
        }

        /// <summary>
        /// Get banks 
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener los bancos
        /// </summary>
        /// <returns>Banks</returns>
        [ProducesResponseType(typeof(CollectionResult<BankResource>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<CollectionResult<BankResource>>> GetBank()
        {
            
            OperationCollectionResult<BankResource> result = await _bankRepository.GetBanksAsync();
            return ReturnCode(result);
        }
        
    }
}
