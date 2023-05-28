using Newtonsoft.Json;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Simulation Resource
    /// </summary>
    /// <summary xml:lang="es">
    /// Simulación
    /// </summary>
    public class SimulationResource
    {
        /// <summary>
        /// Number of quotation
        /// </summary>
        /// <summary xml:lang="es">
        /// Número de cotización
        /// </summary>
        [JsonProperty ("Numero_cotizacion")]
        public long QuotationNumber { get; set; }

        /// <summary>
        /// Answer SP
        /// </summary>
        /// <summary xml:lang="es">
        /// Respuesta del SP
        /// </summary>
        public AnswerResource AnswerResource { get; set; }
    }
}
