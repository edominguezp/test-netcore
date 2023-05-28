namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Request Simulation and Create operation
    /// </summary>
    /// <summary>
    /// Request de la simulacion y creación de una operacion
    /// </summary>
    public class SimulationAndCreateOperationResource
    {
        /// <summary>
        /// Add Simulation
        /// </summary>
        /// <summary xml:lang="es">
        /// Agrega una nueva simulación
        /// </summary>
        public AddSimulation Simulation { get; set; }

        /// <summary>
        /// Data Client
        /// </summary>
        /// /// <summary xml:lang="es">
        /// Datos del cliente
        /// </summary>
        public AddClientResource DataClient { get; set; }
    }
}