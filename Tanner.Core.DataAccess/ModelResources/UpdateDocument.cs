using System;
using Tanner.Core.DataAccess.Enums;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represents the the document rejection update
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa la actualización del rechazo del documento
    /// </summary>
    public class UpdateDocument
    {
        /// <summary>
        /// Operation number
        /// </summary>
        /// <summary xml:lang="es">
        /// Número de operación
        /// </summary>
        public string OperationNumber { get; set; }

        /// <summary>
        /// Document number
        /// </summary>
        /// <summary xml:lang="es">
        /// Número de documento
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Rejection Date of document
        /// </summary>
        /// <summary xml:lang="es">
        /// Fecha de rechazo del documento
        /// </summary>
        public DateTime RejectionDate { get; set; }

        /// <summary>
        /// Code of invoice
        /// </summary>
        /// <summary xml:lang="es">
        /// Código de factura
        /// </summary>
        public InvoiceCode InvoiceCode { get; set; }
    }
}
