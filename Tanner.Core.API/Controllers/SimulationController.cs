using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class SimulationController : BaseController<SimulationController>
    {
        private readonly ISimulationRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="mediator"></param>
        /// <param name="repository"></param>
        public SimulationController(ILogger<SimulationController> logger, IMediator mediator, ISimulationRepository repository) : base(logger, mediator)
        {
            _repository = repository;
        }

        /// <summary>
        /// Add simulation
        /// </summary>
        /// <summary xml:lang="es">
        /// Agregar una nueva simulación
        /// </summary>
        /// <returns>Number of quotation</returns>
        [ProducesResponseType(typeof(long), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        public async Task<ActionResult> AddSimulation(AddSimulation request)
        {
            OperationResult<long> result = await _repository.AddSimulation(request);
            return ReturnCode(result);
        }

        /// <summary>
        /// Get simulation results
        /// </summary>
        /// <summary xml:lang="es">
        /// Consulta los resultados de una simulación
        /// </summary>
        /// <returns>Simulation results</returns>
        [ProducesResponseType(typeof(SimulationResults), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{simulationId}")]
        public async Task<ActionResult> GetSimulationResults([FromRoute] long simulationId)
        {
            OperationResult<SimulationResults> result = await _repository.GetSimulationResults(simulationId);
            return ReturnCode(result);
        }
    }
}
