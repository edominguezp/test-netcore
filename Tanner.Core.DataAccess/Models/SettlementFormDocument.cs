using System;

namespace Tanner.Core.DataAccess.Models
{
    public class SettlementFormDocument
    {
        /// <summary>
        /// Document ID
        /// </summary>
        /// <summary xml:lang="es">
        /// ID del documento
        /// </summary>
        public long DocumentID { get; set; }

        /// <summary>
        /// Folio
        /// </summary>
        /// <summary xml:lang="es">
        /// Folio
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
        /// Amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Debtor name
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre del deudor
        /// </summary>
        public string DebtorBusinessName { get; set; }

        /// <summary>
        /// Debtor rut
        /// </summary>
        /// <summary xml:lang="es">
        /// Rut del deudor
        /// </summary>
        public string DebtorRut { get; set; }

        /// <summary>
        /// Insurance date
        /// </summary>
        /// <summary xml:lang="es">
        /// Fecha de emision
        /// </summary>
        public DateTime IssuanceDate { get; set; }
    }
}
