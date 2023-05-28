using System;
using System.Collections.Generic;
using System.Text;
using Tanner.Core.DataAccess.Enums;

namespace Tanner.Core.DataAccess.Models
{
    public class CreateOperationDirect
    {
        /// <summary>
        /// Código del cliente
        /// </summary>
        public int ClientCode { get; set; }

        /// <summary>
        /// Código de la sucursal
        /// </summary>
        public int BranchOfficeCode { get; set; }

        /// <summary>
        /// Código del producto
        /// </summary>
        public int ProductCode { get; set; }

        /// <summary>
        /// Código de moneda
        /// </summary>
        public ChangeTypeEnum CurrencyCode { get; set; }

        /// <summary>
        /// Fecha de la operación
        /// </summary>
        public DateTime OperationDate { get; set; }

        /// <summary>
        /// Tasa de la operación
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// Origen
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// Valor de la operación
        /// </summary>
        public decimal OperationValue { get; set; }

        /// <summary>
        /// Usuario
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Listado de documentos
        /// </summary>
        public IEnumerable<CreateDocumentRequest> Documents { get; set; }

        /// <summary>
        /// Tipo de operación
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// Porcentaje de descuento
        /// </summary>
        public decimal DiscountRate { get; set; }

        /// <summary>
        /// Valor nominal de la operación
        /// </summary>
        public decimal FaceValueOperation { get; set; }

        /// <summary>
        /// Valor futuro de la operación
        /// </summary>
        public decimal FutureValueOperation { get; set; }

        /// <summary>
        /// Valor presente de la operación
        /// </summary>
        public decimal PresentValueOperation { get; set; }

        /// <summary>
        /// Interés
        /// </summary>
        public decimal OperationInterest { get; set; }

        /// <summary>
        /// Aplicado favor
        /// </summary>
        public decimal AppliedInFavor { get; set; }

        /// <summary>
        /// Aplicado contra
        /// </summary>
        public decimal AppliedAgainst { get; set; }

        /// <summary>
        /// Saldo deudor
        /// </summary>
        public decimal DebitBalance { get; set; }

        /// <summary>
        /// Saldo Acreedor
        /// </summary>
        public decimal CreditBalance { get; set; }

        /// <summary>
        /// Estado de la operación
        /// </summary>
        public int OperationState { get; set; }

        /// <summary>
        /// Tipo de cambio
        /// </summary>
        public decimal ChangeType { get; set; }

        /// <summary>
        /// Tipo pagaré
        /// </summary>
        public int PromissoryNoteType { get; set; }

        /// <summary>
        /// Cargos afectos
        /// </summary>
        public decimal AffectedCharges { get; set; }

        /// <summary>
        /// Cargos exentos
        /// </summary>
        public decimal ExemptCharges { get; set; }

        /// <summary>
        /// Tipo de comicob
        /// </summary>
        public int ComiCobType { get; set; }

        /// <summary>
        /// Monto comicob
        /// </summary>
        public decimal ComiCobAmount { get; set; }

        /// <summary>
        /// Factor comicob
        /// </summary>
        public decimal ComiCobFactor { get; set; }

        /// <summary>
        /// Monto mínimo comicob
        /// </summary>
        public decimal ComiCobMin { get; set; }

        /// <summary>
        /// Monto máximo comicob
        /// </summary>
        public decimal ComiCobMax { get; set; }

        /// <summary>
        /// Con garantía
        /// </summary>
        public bool WithGuarantee { get; set; }

        /// <summary>
        /// Con responsabilidad
        /// </summary>
        public int WithResponsability { get; set; }

        /// <summary>
        /// Tipo de cobranza
        /// </summary>
        public int CollectionType { get; set; }

        /// <summary>
        /// Con notificación
        /// </summary>
        public int WithNotification { get; set; }

        /// <summary>
        /// Con custodia
        /// </summary>
        public int WithCustody { get; set; }

        /// <summary>
        /// Código de sección
        /// </summary>
        public int SectionCode { get; set; }

        /// <summary>
        /// Tasa mora
        /// </summary>
        public decimal DefaultRate { get; set; }

        /// <summary>
        /// Indicador fijo operación
        /// </summary>
        public int FixedIndicatorOperation { get; set; }

        /// <summary>
        /// Indicador primera operación
        /// </summary>
        public int FirstIndicatorOperation { get; set; }

        /// <summary>
        /// Indicador notificación
        /// </summary>
        public int IndicatorNotification { get; set; }

        /// <summary>
        /// Indicador cobranza
        /// </summary>
        public int CollectionIndicator { get; set; }

        /// <summary>
        /// Tipo de pago
        /// </summary>
        public int PaidType { get; set; }

        /// <summary>
        /// devolución intereses
        /// </summary>
        public string ReturnInterest { get; set; }

        /// <summary>
        /// Es simulación
        /// </summary>
        public string IsSimulation { get; set; }
    }
}
