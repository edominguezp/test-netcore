using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.Enums;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.API.Controllers
{
    /// <summary>
    /// Change Type controller
    /// </summary>
    public class ChangeTypeController : BaseController<ChangeTypeController>
    {
        private readonly IChangeTypeRepository _changeTypeRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mediator"></param>
        /// <param name="changeTypeRepository"></param>
        public ChangeTypeController(ILogger<ChangeTypeController> logger, IMediator mediator, IChangeTypeRepository changeTypeRepository) : base(logger, mediator)
        {
            _changeTypeRepository = changeTypeRepository;
        }

        /// <summary>
        /// Get a specific exchange rate stored in the system for a specific date
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtiene un tipo de cambio especifico almacenado en el sistema para una fecha especifica
        /// </summary>
        /// <param name="changeType">1 - Peso chileno <br></br>2 - Unidad de fomento <br></br> 3 - Dólar estadounidense <br></br> 4 - Unidad tributaria mensual</param>
        /// <param name="date">Fecha seleccionada</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(OperationResult<ChangeTypeAndDefaultRateResource>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<OperationResult<ChangeTypeAndDefaultRateResource>>> GetChangeTypeValueInSystemByDateAndChangeType(ChangeTypeEnum changeType, DateTime date)
        {
            OperationResult<ChangeTypeAndDefaultRateResource> result = await _changeTypeRepository.GetChangeTypeAndDefaultRateAsync(changeType, date);
            return ReturnCode(result);
        }
    }
}
