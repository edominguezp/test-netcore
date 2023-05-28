using System;
using System.Collections.Generic;
using System.Text;

namespace Tanner.Core.DataAccess.Models
{
    /// <summary>
    /// Creación de documento
    /// </summary>
    public class CreateDocumentRequest
    {
        /// <summary>
        /// ID del documento
        /// </summary>
        public decimal? DocumentId { get; set; }

        /// <summary>
        /// Número de operación
        /// </summary>
        public decimal OperationNumber { get; set; }

        /// <summary>
        /// Número del documento
        /// </summary>
        public long? DocumentNumber { get; set; }

        /// <summary>
        /// Valor del documento
        /// </summary>
        public decimal DocumentValue { get; set; }

        /// <summary>
        /// Fecha de expiración
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Valor futuro
        /// </summary>
        public decimal FutureValue { get; set; }

        /// <summary>
        /// Valor presente
        /// </summary>
        public decimal PresentValue { get; set; }

        /// <summary>
        /// Interés
        /// </summary>
        public decimal DocumentInterest { get; set; }

        /// <summary>
        /// Código de plaza
        /// </summary>
        public int PlaceCode { get; set; }

        /// <summary>
        /// Código de banco
        /// </summary>
        public int BankCode { get; set; }

        /// <summary>
        /// Código del cliente
        /// </summary>
        public int ClientCode { get; set; }

        /// <summary>
        /// Código tercero
        /// </summary>
        public int ThirdCode { get; set; }

        /// <summary>
        /// Estado del documento
        /// </summary>
        public int DocumentState { get; set; }

        /// <summary>
        /// Cuenta corriente
        /// </summary>
        public string CurrentAccount { get; set; }

        /// <summary>
        /// Glosa
        /// </summary>
        public string Gloss { get; set; }

        /// <summary>
        /// Usuario
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Fecha de emisión
        /// </summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// Indica si es un simulación
        /// </summary>
        public string IsSimulation { get; set; }

        /// <summary>
        /// Fecha cheque
        /// </summary>
        public DateTime? CheckDueDate { get; set; }

        /// <summary>
        /// Tipo de documento sii
        /// </summary>
        public int DocumentTypeSii { get; set; }
    }
}
