using System.Collections.Generic;

namespace Tanner.Core.DataAccess.Models
{
    /// <summary>
    /// Simulation results
    /// </summary>
    public class SimulationResults
    {
        /// <summary>
        /// Quotation ID
        /// </summary>
        /// <summary xml:lang="es">
        /// ID de la cotización
        /// </summary>
        public long QuotationID { get; set; }

        /// <summary>
        /// Simulation ID
        /// </summary>
        /// <summary xml:lang="es">
        /// ID de la simulación
        /// </summary>
        public string SimulationID { get; set; }

        /// <summary>
        /// Total amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto total
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Total anticipated
        /// </summary>
        /// <summary xml:lang="es">
        /// Total anticipado
        /// </summary>
        public decimal TotalAnticipated { get; set; }

        /// <summary>
        /// Price difference
        /// </summary>
        /// <summary xml:lang="es">
        /// Diferencia de precio
        /// </summary>
        public decimal PriceDifference { get; set; }

        /// <summary>
        /// Commission
        /// </summary>
        /// <summary xml:lang="es">
        /// Comisión
        /// </summary>
        public decimal Commission { get; set; }

        /// <summary>
        /// Expenses
        /// </summary>
        /// <summary xml:lang="es">
        /// Gastos
        /// </summary>
        public decimal Expenses { get; set; }

        /// <summary>
        /// IVA
        /// </summary>
        /// <summary xml:lang="es">
        /// IVA
        /// </summary>
        public decimal IVA { get; set; }

        /// <summary>
        /// SubTotal discounts
        /// </summary>
        /// <summary xml:lang="es">
        /// SubTotal descuentos
        /// </summary>
        public decimal SubTotalDiscounts { get; set; }

        /// <summary>
        /// Amount to pay
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto a pagar
        /// </summary>
        public decimal AmountToPay { get; set; }

        /// <summary>
        /// Simulation documents
        /// </summary>
        /// <summary xml:lang="es">
        /// Documentos de la simulación
        /// </summary>
        public IEnumerable<SimulationDocuments> Documents { get; set; }
    }
}