using Newtonsoft.Json;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Answer Resource
    /// </summary>
    /// <summary xml:lang="es">
    /// Respuesta del SP
    /// </summary>
    public class AnswerResource
    {
        /// <summary>
        /// Code of answer
        /// </summary>
        /// <summary xml:lang="es">
        /// Código de respuesta
        /// </summary>
        [JsonProperty ("Cod_Respuesta")]
        public int ResponseCode { get; set; }

        /// <summary>
        /// Answer
        /// </summary>
        /// <summary xml:lang="es">
        /// Respuesta
        /// </summary>
        [JsonProperty("Respuesta")]
        public string Response { get; set; }

    }
}
