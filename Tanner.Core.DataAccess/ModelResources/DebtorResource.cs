namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent data of debtor
    /// </summary>
    ///<summary xml:lang="es">
    /// Clase que representa los datos del deudor
    /// </summary>
    public class DebtorResource
    {
        /// <summary>
        /// RUT debtor
        /// </summary>
        ///<summary xml:lang="es">
        /// RUT del deudor
        /// </summary>
        public string RUT { get; set; }

        /// <summary>
        /// Name Debtor
        /// </summary>
        ///<summary xml:lang="es">
        /// Nombre del deudor
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Document Code
        /// </summary>
        ///<summary xml:lang="es">
        /// Código del documento
        /// </summary>
        public int DocumentCode { get; set; }

        /// <summary>
        /// Document Type
        /// </summary>
        ///<summary xml:lang="es">
        /// Tipo de documento
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        /// Debtor balance
        /// </summary>
        ///<summary xml:lang="es">
        /// Saldo del deudor
        /// </summary>
        public string DebtorBalance { get; set; }

        /// <summary>
        /// Balance Client
        /// </summary>
        ///<summary xml:lang="es">
        /// Saldo del cliente
        /// </summary>
        public string ClientBalance { get; set; }

        /// <summary>
        /// Total Amount
        /// </summary>
        ///<summary xml:lang="es">
        /// Monto total
        /// </summary>
        public string TotalAmount { get; set; }

        /// <summary>
        /// Percentage 
        /// </summary>
        ///<summary xml:lang="es">
        /// Porcentaje
        /// </summary>
        public decimal Percentage { get; set; }

        /// <summary>
        /// Crossing
        /// </summary>
        ///<summary xml:lang="es">
        /// Cruce
        /// </summary>
        public string Crossing { get; set; }
    }
}
