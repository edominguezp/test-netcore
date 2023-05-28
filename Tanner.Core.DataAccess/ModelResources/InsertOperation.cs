namespace Tanner.Core.DataAccess.ModelResources
{
    // <summary>
    /// Class that represent the insert of operation
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa el ingreso de una operación
    /// </summary>
    public class InsertOperation
    {
        // <summary>
        /// Quotation ID
        /// </summary>
        /// <summary xml:lang="es">
        /// Id de la cotización
        /// </summary>
        public int QuotationID { get; set; }

        // <summary>
        /// Bank code
        /// </summary>
        /// <summary xml:lang="es">
        /// Código del banco
        /// </summary>
        public string BankCode { get; set; }

        // <summary>
        /// Current account
        /// </summary>
        /// <summary xml:lang="es">
        /// Cuenta corriente
        /// </summary>
        public string CurrentAccount { get; set; }
    }
}
