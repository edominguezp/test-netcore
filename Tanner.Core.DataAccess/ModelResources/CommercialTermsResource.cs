namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent indicators by operation
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa los indicadores para una operación
    /// </summary>
    public class CommercialTermsResource
    {
        /// <summary>
        /// Rate by document
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa asociada al documento
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// Commission by document
        /// </summary>
        /// <summary xml:lang="es">
        /// Comisión asociada al documento          
        /// </summary>
        public decimal Commission { get; set; }

        /// <summary>
        /// Expenses by document
        /// </summary>
        /// <summary xml:lang="es">
        /// Gastos asociados al documento       
        /// </summary>
        public decimal Expenses { get; set; }
    }
}
