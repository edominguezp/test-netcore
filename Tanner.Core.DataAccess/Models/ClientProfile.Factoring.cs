using System.ComponentModel.DataAnnotations.Schema;
namespace Tanner.Core.DataAccess.Models
{
    public partial class ClientProfile
    {        
        /// <summary>
        /// Approved amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto aprobado
        /// </summary>
        [Column("MONTO_APROBADO", TypeName = "float")]
        public decimal ApprovedAmount { get; set; }

        /// <summary>
        /// Factoring amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto de factoring
        /// </summary>
        [Column("Stock_Factoring", TypeName = "float")]
        public decimal FactoringAmount { get; set; }

        /// <summary>
        /// Normal Amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje disponible factoring
        /// </summary>
        [Column("Stock_Normal", TypeName = "float")]
        public decimal NormalAmount { get; set; }

        /// <summary>
        /// Reoperation Amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto por reoperación
        /// </summary>
        [Column("Stock_reop", TypeName = "float")]
        public decimal ReoperationAmount { get; set; }

        /// <summary>
        /// Credit amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto de crédito
        /// </summary>
        [Column("Stock_credito", TypeName = "float")]
        public decimal CreditAmount { get; set; }

        /// <summary>
        /// Stock amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto disponible
        /// </summary>
        [Column("Stock", TypeName = "float")]
        public decimal StockAmount { get; set; }

        /// <summary>
        /// Percentage Stock Factoring
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje disponible factoring
        /// </summary>
        [Column("PorcStock_Factoring", TypeName = "float")]
        public decimal PercentageStockFact { get; set; }

        /// <summary>
        /// Percentage Normal Stock
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje normal disponible
        /// </summary>
        [Column("PorcStock_Normal", TypeName = "float")]
        public decimal PercentageNormalStock { get; set; }

        /// <summary>
        /// Percentage stock Reoperation
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje de reoperación disponible
        /// </summary>
        [Column("PorcStock_reop", TypeName = "float")]
        public decimal PercentagestockReoperation { get; set; }

        /// <summary>
        /// Percentage Stock Credit
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje disponible de crédito
        /// </summary>
        [Column("PorcStock_credito", TypeName = "float")]
        public decimal PercentageStockCredit { get; set; }

        /// <summary>
        /// Percentage line bussy
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje de línea ocupado
        /// </summary>
        [Column("PorcLneaOcupado", TypeName = "float")]
        public decimal PercentageLineBussy { get; set; }
    }
}
