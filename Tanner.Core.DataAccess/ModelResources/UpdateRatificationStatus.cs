using System;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the parameters of SP
    /// </summary>
    ///<summary xml:lang="es">
    /// Clase que representa los parametros del SP
    /// </summary>
    public class UpdateRatificationStatus
    {
        /// <summary>
        /// Document ID
        /// </summary>
        ///<summary xml:lang="es">
        /// Identificador del documento
        /// </summary>
        public decimal DocumentID { get; set; }

        /// <summary>
        /// Confirmation status
        /// </summary>
        ///<summary xml:lang="es">
        /// Estado de confirmación
        /// </summary>
        public int ConfirmationStatus { get; set; }

        /// <summary>
        /// Observation
        /// </summary>
        ///<summary xml:lang="es">
        /// Observación
        /// </summary>
        public string Observation { get; set; }

        /// <summary>
        /// Login CORE
        /// </summary>
        ///<summary xml:lang="es">
        /// Nombre de inicio de sesión en CORE
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Is Granted
        /// </summary>
        ///<summary xml:lang="es">
        /// devuelve verdadero o falso si el documento está cedido
        /// </summary>
        public bool IsGranted { get; set; }
    }
}
