using System.ComponentModel.DataAnnotations.Schema;

namespace Tanner.Core.DataAccess.Models
{
    public partial class ClientProfile 
    {

        /// <summary>
        /// Normal Recency
        /// </summary>
        /// <summary xml:lang="es">
        /// Recencia operaciones normales
        /// </summary>
        [Column("ultant1Normal_Recencia", TypeName = "int")]
        public int NormalRecency { get; set; }

        /// <summary>
        /// Normal amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto normal
        /// </summary>
        [Column("UltNormal_Monto", TypeName = "float")]
        public decimal NormalAmountOp { get; set; }

        /// <summary>
        /// Normal rate
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa normal
        /// </summary>
        [Column("UltNormal_tasa", TypeName = "float")]
        public decimal NormalRate { get; set; }

        /// <summary>
        /// Normal commission
        /// </summary>
        /// <summary xml:lang="es">
        /// Comisión normal
        /// </summary>
        [Column("UltNormal_Comision", TypeName = "float")]
        public decimal NormalCommission { get; set; }

        /// <summary>
        /// Normal expenses
        /// </summary>
        /// <summary xml:lang="es">
        /// Gastos normal
        /// </summary>
        [Column("UltNormal_Gastos", TypeName = "float")]
        public decimal NormalExpenses { get; set; }

        /// <summary>
        /// Normal amount 1
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto normal 1
        /// </summary>
        [Column("ultant1Normal_Monto", TypeName = "float")]
        public decimal NormalAmount1 { get; set; }

        /// <summary>
        /// Normal rate 1
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa normal 1
        /// </summary>
        [Column("ultant1Normal_tasa", TypeName = "float")]
        public decimal NormalRate1 { get; set; }

        /// <summary>
        /// Normal commission 1
        /// </summary>
        /// <summary xml:lang="es">
        /// Comisión normal 1
        /// </summary>
        [Column("ultant1Normal_Comision", TypeName = "float")]
        public decimal NormalCommission1 { get; set; }

        /// <summary>
        /// Normal expenses 1
        /// </summary>
        /// <summary xml:lang="es">
        /// Gastos normal 1
        /// </summary>
        [Column("ultant1Normal_Gastos", TypeName = "float")]
        public decimal NormalExpenses1 { get; set; }

        /// <summary>
        /// Normal amount 2
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto normal 2
        /// </summary>
        [Column("ultant2Normal_Monto", TypeName = "float")]
        public decimal NormalAmount2 { get; set; }

        /// <summary>
        /// Normal rate 2
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa normal 2
        /// </summary>
        [Column("ultant2Normal_tasa", TypeName = "float")]
        public decimal NormalRate2 { get; set; }

        /// <summary>
        /// Normal commission 2
        /// </summary>
        /// <summary xml:lang="es">
        /// Comisión normal 2
        /// </summary>
        [Column("ultant2Normal_Comision", TypeName = "float")]
        public decimal NormalCommission2 { get; set; }

        /// <summary>
        /// Normal expenses 2
        /// </summary>
        /// <summary xml:lang="es">
        /// Gastos normal 2
        /// </summary>
        [Column("Ultant2Normal_Gastos", TypeName = "float")]
        public decimal NormalExpenses2 { get; set; }

        /// <summary>
        /// Reoperation Recency
        /// </summary>
        /// <summary xml:lang="es">
        /// Recencia de reoperaciones
        /// </summary>
        [Column("Ultant1Reop_Recencia", TypeName = "int")]
        public int ReoperationRecency { get; set; }

        /// <summary>
        /// Reoperation Amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto de reoperaciones
        /// </summary>
        [Column("UltReop_Monto", TypeName = "float")]
        public decimal ReoperationsAmount { get; set; }

        /// <summary>
        /// Reoperation rate
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa de reoperaciones
        /// </summary>
        [Column("UltReop_tasa", TypeName = "float")]
        public decimal ReoperationRate { get; set; }

        /// <summary>
        /// Reoperation commission
        /// </summary>
        /// <summary xml:lang="es">
        /// Comisión de reoperaciones
        /// </summary>
        [Column("UltReop_Comision", TypeName = "float")]
        public decimal ReoperationCommission { get; set; }

        /// <summary>
        /// Reoperation expenses
        /// </summary>
        /// <summary xml:lang="es">
        /// Gastos de reoperaciones
        /// </summary>
        [Column("UltReop_Gastos", TypeName = "float")]
        public decimal ReoperationExpenses { get; set; }

        /// <summary>
        /// Reoperation Amount 1
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto de reoperaciones 1
        /// </summary>
        [Column("Ultant1Reop_Monto", TypeName = "float")]
        public decimal ReoperationsAmount1 { get; set; }

        /// <summary>
        /// Reoperation rate 1
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa de reoperaciones 1
        /// </summary>
        [Column("Ultant1Reop_tasa", TypeName = "float")]
        public decimal ReoperationRate1 { get; set; }

        /// <summary>
        /// Reoperation commission 1
        /// </summary>
        /// <summary xml:lang="es">
        /// Comisión de reoperaciones 1
        /// </summary>
        [Column("Ultant1Reop_Comision", TypeName = "float")]
        public decimal ReoperationCommission1 { get; set; }

        /// <summary>
        /// Reoperation expenses 1
        /// </summary>
        /// <summary xml:lang="es">
        /// Gastos de reoperaciones 1
        /// </summary>
        [Column("Ultant1Reop_Gastos", TypeName = "float")]
        public decimal ReoperationExpenses1 { get; set; }

        /// <summary>
        /// Reoperation Amount 2
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto de reoperaciones 2
        /// </summary>
        [Column("Ultant2Reop_Monto", TypeName = "float")]
        public decimal ReoperationsAmount2 { get; set; }

        /// <summary>
        /// Reoperation rate 2
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa de reoperaciones 2
        /// </summary>
        [Column("Ultant2Reop_tasa", TypeName = "float")]
        public decimal ReoperationRate2 { get; set; }

        /// <summary>
        /// Reoperation commission 2
        /// </summary>
        /// <summary xml:lang="es">
        /// Comisión de reoperaciones 2
        /// </summary>
        [Column("Ultant2Reop_Comision", TypeName = "float")]
        public decimal ReoperationCommission2 { get; set; }

        /// <summary>
        /// Reoperation expenses 2
        /// </summary>
        /// <summary xml:lang="es">
        /// Gastos de reoperaciones 2
        /// </summary>
        [Column("Ultant2Reop_Gastos", TypeName = "float")]
        public decimal ReoperationExpenses2 { get; set; }

        /// <summary>
        /// Credit Recency
        /// </summary>
        /// <summary xml:lang="es">
        /// Recencia de créditos
        /// </summary>
        [Column("UltcreditoRecencia", TypeName = "int")]
        public int CreditRecency { get; set; }

        /// <summary>
        /// Credits amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Montos de créditos
        /// </summary>
        [Column("UltcreditoMonto", TypeName = "float")]
        public decimal CreditsAmount { get; set; }

        /// <summary>
        /// Credit rate
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa de créditos
        /// </summary>
        [Column("UltcreditoTasa", TypeName = "float")]
        public decimal CreditRate { get; set; }

        /// <summary>
        /// Credits amount 2
        /// </summary>
        /// <summary xml:lang="es">
        /// Montos de créditos 2
        /// </summary>
        [Column("UltcreditoMonto2", TypeName = "float")]
        public decimal CreditsAmount2 { get; set; }

        /// <summary>
        /// Credit rate 2
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa de créditos 2
        /// </summary>
        [Column("UltcreditoTasa2", TypeName = "float")]
        public decimal CreditRate2 { get; set; }

        /// <summary>
        /// Credits amount 3
        /// </summary>
        /// <summary xml:lang="es">
        /// Montos de créditos 3
        /// </summary>
        [Column("UltcreditoMonto3", TypeName = "float")]
        public decimal CreditsAmount3 { get; set; }

        /// <summary>
        /// Credit rate 3
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa de créditos 3
        /// </summary>
        [Column("UltcreditoTasa3", TypeName = "float")]
        public decimal CreditRate3 { get; set; }
    }
}
