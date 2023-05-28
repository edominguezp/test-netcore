namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class representing the percentage of associated indicators 
    /// </summary>
    ///<summary xml:lang="es">
    /// Clase que representa el porcentaje de los indicadores asociados
    /// </summary>
    public class IndicatorsPercentageResource
    {
        /// <summary>
        /// Paid amount percentage to client
        /// </summary>
        ///<summary xml:lang="es">
        /// Porcentaje cancelado al cliente
        /// </summary>
        public decimal PaidAmountPercentageToCli { get; set; }

        /// <summary>
        /// Paid amount percentage to debtor
        /// </summary>
        ///<summary xml:lang="es">
        /// Porcentaje cancelado al deudor
        /// </summary>
        public decimal PaidAmountPercentageToDebtor { get; set; }

        /// <summary>
        /// Paid amount percentage by operation
        /// </summary>
        ///<summary xml:lang="es">
        /// Porcentaje cancelado por operación
        /// </summary>
        public decimal PaidAmountPercentageByOpe { get; set; }
    }
}
