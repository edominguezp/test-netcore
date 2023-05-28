using System;

namespace Tanner.Core.DataAccess.Models
{
    public class OperationDocumentByID
    {
        /// <summary>
        /// Debtor RUT
        /// </summary>
        /// <summary xml:lang="es">
        /// RUT del deudor
        /// </summary>
        public string DebtorRUT { get; set; }

        /// <summary>
        /// Debtor Name
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre del deudor
        /// </summary>
        public string DebtorName { get; set; }

        /// <summary>
        /// Expiry date string
        /// </summary>
        /// <summary xml:lang="es">
        /// Fecha de vencimiento en texto
        /// </summary>
        public string ExpiryDateString { get; set; }

        /// <summary>
        /// Expiry date
        /// </summary>
        /// <summary xml:lang="es">
        /// Fecha de vencimiento
        /// </summary>
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// Document number
        /// </summary>
        /// <summary xml:lang="es">
        /// Numero de documento
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Document amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto del documento
        /// </summary>
        public decimal DocumentAmount { get; set; }

        /// <summary>
        /// Advanced amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto anticipado
        /// </summary>
        public decimal AdvancedAmount { get; set; }

        /// <summary>
        /// Price difference
        /// </summary>
        /// <summary xml:lang="es">
        /// Diferencia de precio
        /// </summary>
        public decimal PriceDifference { get; set; }

        /// <summary>
        /// Purchase price
        /// </summary>
        /// <summary xml:lang="es">
        /// Precio de compra
        /// </summary>
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// Balance
        /// </summary>
        /// <summary xml:lang="es">
        /// Saldo
        /// </summary>
        public decimal Balance { get; set; }
    }
}
