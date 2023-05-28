using System;

namespace Tanner.Core.DataAccess.Models
{
    public class OperationDocument
    {
        /// <summary>
        /// Operation ID
        /// </summary>
        /// <summary xml:lang="es">
        /// ID de la operación
        /// </summary>
        public long OperationNumber { get; set; }

        /// <summary>
        /// Grant date
        /// </summary>
        /// <summary xml:lang="es">
        /// Fecha de otorgamiento
        /// </summary>
        public DateTime GrantDate { get; set; }

        /// <summary>
        /// Document type
        /// </summary>
        /// <summary xml:lang="es">
        /// Tipo de documento
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        /// Document amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto del documento
        /// </summary>
        public decimal DocumentAmount { get; set; }

        /// <summary>
        /// Advance amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto anticipado
        /// </summary>
        public decimal AdvancedAmount { get; set; }

        /// <summary>
        /// Bank draft
        /// </summary>
        /// <summary xml:lang="es">
        /// Giro bancario
        /// </summary>
        public decimal BankDraft { get; set; }

        /// <summary>
        /// Operation status
        /// </summary>
        /// <summary xml:lang="es">
        /// Estado de la operación
        /// </summary>
        public string OperationStatus { get; set; }

        /// <summary>
        /// Operation status code
        /// </summary>
        /// <summary xml:lang="es">
        /// Código del estado de la operación
        /// </summary>
        public int OperationStatusCode { get; set; }
    }
}
