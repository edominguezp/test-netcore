using System;
using System.Collections.Generic;
using System.Text;
using Tanner.Core.DataAccess.Enums;
using Tanner.Core.DataAccess.Models;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Clase para crear una operación sin simulación
    /// </summary>
    public class CreateOperationWithClientNoSimulation
    {
        /// <summary>
        /// Tipo de cambio
        /// </summary>
        public decimal ChangeType { get; set; }

        /// <summary>
        /// Tipo de cobranza
        /// </summary>
        public int CollectionType { get; set; }

        /// <summary>
        /// Tipo comicob
        /// </summary>
        public int ComiCobType { get; set; }

        /// <summary>
        /// Tipo de moneda
        /// </summary>
        public ChangeTypeEnum CurrencyType { get; set; }

        /// <summary>
        /// Data del cliente
        /// </summary>
        public AddClientResource DataClient { get; set; }

        /// <summary>
        /// Tasa mora
        /// </summary>
        public int DefaultRate { get; set; }

        /// <summary>
        /// Tasa de descuento
        /// </summary>
        public decimal DiscountRate { get; set; }

        /// <summary>
        /// Documentos
        /// </summary>
        public IEnumerable<CreateDocumentNoSimulation> Documents { get; set; }

        /// <summary>
        /// Indica si es una simulación
        /// </summary>
        public bool IsSimulation { get; set; }

        /// <summary>
        /// Fecha de operación
        /// </summary>
        public DateTime OperationDate { get; set; }

        /// <summary>
        /// Tasa de la operación
        /// </summary>
        public decimal OperationRate { get; set; }

        /// <summary>
        /// Monto de la operación
        /// </summary>
        public decimal OperationValue { get; set; }

        /// <summary>
        /// Origen
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// Código del producto
        /// </summary>
        public int ProductCode { get; set; }

        /// <summary>
        /// Indica si hay devolución de intereses
        /// </summary>
        public bool ReturnInterest { get; set; }

        /// <summary>
        /// Tipo de pago
        /// </summary>
        public int PaidType { get; set; }

        /// <summary>
        /// Tipo de pagaré
        /// </summary>
        public int PromissoryNoteType { get; set; }

        /// <summary>
        /// Usuario
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Cargos afectos
        /// </summary>
        public decimal AffectedCharges { get; set; }

        /// <summary>
        /// Aplicado en contra
        /// </summary>
        public decimal AppliedAgainst { get; set; }

        /// <summary>
        /// Aplicado a favor
        /// </summary>
        public decimal AppliedInFavor { get; set; }
    }


    /// <summary>
    /// Clase de creación de documentos
    /// </summary>
    public class CreateDocumentNoSimulation
    {
        /// <summary>
        /// Folio
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Valor del documento
        /// </summary>
        public decimal NominalValue { get; set; }

        /// <summary>
        /// Fecha de vencimiento
        /// </summary>
        public DateTime ExpiredDate { get; set; }

        /// <summary>
        /// Rut del deudor
        /// </summary>
        public string DebtorRUT { get; set; }

        /// <summary>
        /// Fecha de emision
        /// </summary>
        public DateTime IssueDate { get; set; }

        /// <summary>
        /// Tipo de documento sii
        /// </summary>
        public int DocumentTypeSii { get; set; }
    }
}
