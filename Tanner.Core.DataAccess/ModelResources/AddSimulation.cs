using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Add Simulation
    /// </summary>
    /// <summary xml:lang="es">
    /// Agregar una nueva simulación
    /// </summary>
    public class AddSimulation
    {
        /// <summary>
        /// Client RUT
        /// </summary>
        /// <summary xml:lang="es">
        /// RUT del cliente
        /// </summary>
        public string ClientRUT { get; set; }

        /// <summary>
        /// Operation Date
        /// </summary>
        /// <summary xml:lang="es">
        /// Fecha de la operación
        /// </summary>
        public DateTime OperationDate { get; set; }

        /// <summary>
        /// Operation Rate
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa de la operación
        /// </summary>
        public decimal OperationRate { get; set; }

        /// <summary>
        /// Percentage of discount
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje descuento
        /// </summary>
        public decimal PercentageDiscount { get; set; }

        /// <summary>
        /// Currency code
        /// </summary>
        /// <summary xml:lang="es">
        /// Código de moneda
        /// </summary>
        public int CurrencyCode { get; set; }

        /// <summary>
        /// Product code
        /// </summary>
        /// <summary xml:lang="es">
        /// Código de producto
        /// </summary>
        public int ProductCode { get; set; }

        /// <summary>
        /// Type commision cob
        /// </summary>
        /// <summary xml:lang="es">
        /// Tipo de comisión de cobranza
        /// </summary>
        public int TypeCommisionCob { get; set; }

        /// <summary>
        /// Commission Amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto de comisión
        /// </summary>
        public decimal CommissionAmount { get; set; }

        /// <summary>
        /// Commission Factor
        /// </summary>
        /// <summary xml:lang="es">
        /// Factor de comisión
        /// </summary>
        public decimal CommissionFactor { get; set; }

        /// <summary>
        /// Minimum Commission
        /// </summary>
        /// <summary xml:lang="es">
        /// Comisión mínima
        /// </summary>
        public decimal MinimumCollectionCommission { get; set; }

        /// <summary>
        /// Maximum Commission
        /// </summary>
        /// <summary xml:lang="es">
        /// Comisión máxima
        /// </summary>
        public decimal MaximumCollectionCommission { get; set; }

        /// <summary>
        /// Fixed expense
        /// </summary>
        /// <summary xml:lang="es">
        /// Gastos
        /// </summary>
        public decimal FixedExpense { get; set; }

        /// <summary>
        /// Operation type
        /// </summary>
        /// <summary xml:lang="es">
        /// Tipo de operación
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// Simulation Number
        /// </summary>
        /// <summary xml:lang="es">
        /// Número de simulación
        /// </summary>
        public string SimulationNumber { get; set; }

        /// <summary>
        /// Origin simulation
        /// </summary>
        /// <summary xml:lang="es">
        /// Origen de la simulación
        /// </summary>
        public string Av_Origin { get; set; }

        /// <summary>
        /// Document list
        /// </summary>
        /// <summary xml:lang="es">
        /// Lista de documentos
        /// </summary>
        public List<AddDocumentToSimulation> Documents { get; set; }

    }

    public class AddDocumentToSimulation
    {
        /// <summary>
        /// Document Number
        /// </summary>
        /// <summary xml:lang="es">
        /// Número de documento
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Nominal value
        /// </summary>
        /// <summary xml:lang="es">
        /// Valor nominal
        /// </summary>
        public decimal NominalValue { get; set; }

        /// <summary>
        /// Expired date
        /// </summary>
        /// <summary xml:lang="es">
        /// Fecha de expiración
        /// </summary>
        public DateTime ExpiredDate { get; set; }

        /// <summary>
        /// Debtor RUT
        /// </summary>
        /// <summary xml:lang="es">
        /// Rut del deudor
        /// </summary>
        public string DebtorRUT { get; set; }

        /// <summary>
        /// Issue Date
        /// </summary>
        /// <summary xml:lang="es">
        /// Fecha de emisión
        /// </summary>
        public DateTime IssueDate { get; set; }

        /// <summary>
        /// Dte Type
        /// </summary>
        /// <summary xml:lang="es">
        /// Tipo de Dte
        /// </summary>
        public int? DteType { get; set; }

        /// <summary>
        /// ID doc simulation
        /// </summary>
        /// <summary xml:lang="es">
        /// Identificador del documento
        /// </summary>
        public string IdDocSimulation { get; set; }
    }
}
