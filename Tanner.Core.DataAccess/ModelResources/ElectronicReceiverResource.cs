namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the response when registering the electronic receiver
    /// </summary>
    ///<summary xml:lang="es">
    /// Clase que representa la respuesta al registrar el receptor electrónico
    /// </summary>
    public class ElectronicReceiverResource
    {
        /// <summary>
        /// Answer Code
        /// </summary>
        ///<summary xml:lang="es">
        /// Código de respuesta
        /// </summary>
        public int CodigoRespuesta { get; set; }

        /// <summary>
        /// Message of Answer
        /// </summary>
        ///<summary xml:lang="es">
        /// Mensaje de respuesta
        /// </summary>
        public string MensajeRespuesta { get; set; }

    }
}
