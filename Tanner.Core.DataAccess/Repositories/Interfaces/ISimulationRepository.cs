using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Models;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.DataAccess.Repositories.Interfaces
{
    /// <summary>
    /// Interface to simulation
    /// </summary>
    public interface ISimulationRepository
    {
        /// <summary>
        /// Add simulation
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<OperationResult<long>> AddSimulation(AddSimulation request);

        /// <summary>
        /// Get simulation results
        /// </summary>
        /// <param name="simulationId">Simulation ID</param>
        /// <returns>Simulation results</returns>
        Task<OperationResult<SimulationResults>> GetSimulationResults(long simulationId);
    }
}
