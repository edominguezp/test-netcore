using System;

namespace Tanner.Core.DataAccess.Models
{
    /// <summary>
    /// Simulation documents
    /// </summary>
    public class SimulationDocuments
    {
        /// <summary>
        /// Document ID
        /// </summary>
        /// <summary xml:lang="es">
        /// ID del documento
        /// </summary>
        public long DocumentID { get; set; }

        /// <summary>
        /// Document ID simulation
        /// </summary>
        /// <summary xml:lang="es">
        /// ID del documento de la simulación
        /// </summary>
        public string DocumentIDSimulation { get; set; }

        /// <summary>
        /// Document Folio
        /// </summary>
        /// <summary xml:lang="es">
        /// Folio del documento
        /// </summary>
        public long Folio { get; set; }

        /// <summary>
        /// Expiry date
        /// </summary>
        /// <summary xml:lang="es">
        /// Fecha de vencimiento
        /// </summary>
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// Document amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto del documento
        /// </summary>
        public decimal Amount { get; set; }
    }
}
