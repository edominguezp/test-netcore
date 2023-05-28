
namespace Tanner.Core.DataAccess.ModelResources
{
    public class OperationSummaryResource
    {
        public OperationDataResource Operation { get; set; }

        /// <summary>
        /// Total Operations
        /// </summary>
        ///<summary xml:lang="es">
        /// Total de operaciones
        /// </summary>
        public int TotalOperations { get; set; }

        /// <summary>
        /// Total operations of 9 months
        /// </summary>
        ///<summary xml:lang="es">
        /// Total de operaciones en 9 meses
        /// </summary>
        public int TotalOperation9Months { get; set; }

        /// <summary>
        /// Total operations of 6 months
        /// </summary>
        ///<summary xml:lang="es">
        /// Total de operaciones en 6 meses
        /// </summary>
        public int TotalOperation6Months { get; set; }

        /// <summary>
        /// Maximun operation amount
        /// </summary>
        ///<summary xml:lang="es">
        /// Monto máximo de la operación
        /// </summary>
        public long? MaximunOperationAmount { get; set; }

        /// <summary>
        /// Debtor Payment Percentage
        /// </summary>
        ///<summary xml:lang="es">
        /// Porcentaje de pago del deudor
        /// </summary>
        public decimal? PaymentPercentage { get; set; }
    }
}
