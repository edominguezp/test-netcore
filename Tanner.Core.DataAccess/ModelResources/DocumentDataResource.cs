
using System;
using Tanner.Core.DataAccess.Enums;

namespace Tanner.Core.DataAccess.ModelResources
{
    public class DocumentDataResource
    {
        /// <summary>
        /// Document ID
        /// </summary>
        ///<summary xml:lang="es">
        /// ID del documento
        /// </summary>
        public int DocumentID { get; set; }

        /// <summary>
        /// Document Number
        /// </summary>s
        ///<summary xml:lang="es">
        /// Número de documento
        /// </summary>
        public decimal DocumentNumber { get; set; }

        /// <summary>
        /// Document Amount
        /// </summary>
        ///<summary xml:lang="es">
        /// Monto de documento
        /// </summary>
        public decimal DocumentAmount { get; set; }

        /// <summary>
        /// Debtor Name
        /// </summary>
        ///<summary xml:lang="es">
        /// Nombre del deudor
        /// </summary>
        public string DebtorName { get; set; }

        /// <summary>
        /// Debtor RUT
        /// </summary>
        ///<summary xml:lang="es">
        /// RUT del deudor
        /// </summary>
        public string DebtorRUT { get; set; }

        /// <summary>
        /// DocumentType
        /// </summary>
        ///<summary xml:lang="es">
        /// Tipo de documento
        /// </summary>
        public int DocumentType { get; set; }

        /// <summary>
        /// Date Reception SII
        /// </summary>
        ///<summary xml:lang="es">
        /// Fecha recepción SII
        /// </summary>
        public DateTime? ReceptionDateSII { get; set; }

        /// <summary>
        /// Amount Number Transactions Debtor
        /// </summary>
        ///<summary xml:lang="es">
        /// Monto de transacciones de deudor
        /// </summary>
        public decimal AmountNumberTransactionsDebtor { get; set; }

        /// <summary>
        /// Debtor payment quantity
        /// </summary>
        ///<summary xml:lang="es">
        /// Cantidad de documentos a pagar por el deudor
        /// </summary>
        public int DebtorPaymentQuantity { get; set; }

        /// <summary>
        /// Debtor payment total
        /// </summary>
        ///<summary xml:lang="es">
        /// Monto total a pagar por el deudor
        /// </summary>
        public decimal DebtorPaymentTotal { get; set; }

        /// <summary>
        /// Ratify Status
        /// </summary>
        ///<summary xml:lang="es">
        /// Estado de ratificación
        /// </summary>
        public RatificationState RatifyStatus { get; set; }

        /// <summary>
        /// Debtor economic activity
        /// </summary>
        /// <summary xml:lang="es">
        /// Actividad económica del deudor
        /// </summary>
        public string EconomicActivity { get; set; }

        /// <summary>
        /// Debtor economic activity number
        /// </summary>
        /// <summary xml:lang="es">
        /// Número de ctividad económica del deudor
        /// </summary>
        public int EconomicActivityNumber { get; set; }

        /// <summary>
        /// Commercial business
        /// </summary>
        /// <summary xml:lang="es">
        /// Giro comercial
        /// </summary>
        public string CommercialBusiness { get; set; }

        /// <summary>
        /// Document type SII.
        /// </summary>
        /// <summary xml:lang="es">
        /// Tipo de documento SII.
        /// </summary>
        public int DocumentTypeSII { get; set; }

        /// <summary>
        /// Ratify observation.
        /// </summary>
        /// <summary xml:lang="es">
        /// Observación de ratificación.
        /// </summary>
        public string RatifyObservation { get; set; }

        /// <summary>
        /// Issuance date.
        /// </summary>
        /// <summary xml:lang="es">
        /// Fecha de emisión.
        /// </summary>
        public DateTime? IssuanceDate { get; set; }

    }
}
