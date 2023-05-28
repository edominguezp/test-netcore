using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.API.Controllers
{
    /// <summary>
    /// Employee controller
    /// </summary>
    public class EmployeeController : BaseController<EmployeeController>
    {
        private readonly IEmployeeRepository _employeeRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="mediator"></param>
        /// <param name="employeeRepository"></param>
        public EmployeeController(ILogger<EmployeeController> logger, IMediator mediator, IEmployeeRepository employeeRepository) : base(logger, mediator)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Get Data by employee asociated to email
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener los datos de un empleado asociado a un correo electrónico
        /// </summary>
        /// <param name="email">Employee email</param>
        /// <returns>Data Employee</returns>
        [ProducesResponseType(typeof(IEnumerable<CollectionResult<EmployeeResource>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery]string email)
        {
            OperationResult<EmployeeResource> result = await _employeeRepository.GetDataEmployeeByEmailAsync(email);
            return ReturnCode(result);
        }
    }
}
