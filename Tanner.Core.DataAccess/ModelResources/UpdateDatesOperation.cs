using System;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the update the dates of an operation
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase para la actualización de las fechas de una operación
    /// </summary>
    public class UpdateDatesOperation
    {
        /// <summary>
        /// Client RUT
        /// </summary>
        /// <summary xml:lang="es">
        /// RUT del cliente
        /// </summary>
        public string ClientRUT { get; set; }

        /// <summary>
        /// Document number
        /// </summary>
        /// <summary xml:lang="es">
        /// Número del documento
        /// </summary>
        public long DocumentNumber { get; set; }

        /// <summary>
        /// Issue Date
        /// </summary>
        /// <summary xml:lang="es">
        /// Fecha de emisión
        /// </summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// Granted Date
        /// </summary>
        /// <summary xml:lang="es">
        /// Fecha de cesión
        /// </summary>
        public DateTime? GrantedDate { get; set; }

        /// <summary>
        /// Reception Date
        /// </summary>
        /// <summary xml:lang="es">
        /// Fecha de recepción
        /// </summary>
        public DateTime? ReceptionDate { get; set; }
    }
}
