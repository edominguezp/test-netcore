namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the commercial conditions of an operation
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa las condiciones comerciales de una operación
    /// </summary>
    public class UpdateCommercialTermOperation
    {
        /// <summary>
        /// Operation number
        /// </summary>
        /// <summary xml:lang="es">
        /// Número de operación
        /// </summary>
        public int OperationNumber { get; set; }

        /// <summary>
        /// Operation Rate
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa de la operación
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// Operation commission
        /// </summary>
        /// <summary xml:lang="es">
        /// Comisión de la operación
        /// </summary>
        public decimal Commission { get; set; }

        /// <summary>
        /// Operation Expenses
        /// </summary>
        /// <summary xml:lang="es">
        /// Gastos de la operación
        /// </summary>
        public decimal Expenses { get; set; }

        /// <summary>
        /// User who makes the changes
        /// </summary>
        /// <summary xml:lang="es">
        /// Usuario que realiza los cambios
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// User type
        /// </summary>
        /// <summary xml:lang="es">
        /// Tipo de usuario
        /// </summary>
        public string OperationTypeCode { get; set; }
    }
}
