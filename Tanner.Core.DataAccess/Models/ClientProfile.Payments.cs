using System.ComponentModel.DataAnnotations.Schema;

namespace Tanner.Core.DataAccess.Models
{
    public partial class ClientProfile
    {
        /// <summary>
        /// Weighted Sluggish rate
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa de mora ponderada
        /// </summary>
        [Column("TasaMPP", TypeName = "float")]
        public decimal WeightedSluggishRate { get; set; }

        /// <summary>
        /// Maximum Sluggish rate
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa de mora máxima
        /// </summary>
        [Column("TasaMoraMax", TypeName = "float")]
        public decimal MaximumSluggishRate { get; set; }

        /// <summary>
        /// Maximum Sluggish Days
        /// </summary>
        /// <summary xml:lang="es">
        /// Días de mora máxima
        /// </summary>
        [Column("DiasMoraMax", TypeName = "float")]
        public decimal MaximumSluggishDays { get; set; }

        /// <summary>
        /// Sluggish weightd days
        /// </summary>
        /// <summary xml:lang="es">
        /// Días de mora ponderada
        /// </summary>
        [Column("DiasMoraMPP", TypeName = "float")]
        public decimal SluggishWeightdDays { get; set; }

        /// <summary>
        /// Total Amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto total
        /// </summary>
        [Column("Monto_Pago", TypeName = "float")]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Amount Sluggish
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto total mora
        /// </summary>
        [Column("Monto_totalmORA", TypeName = "float")]
        public decimal AmountSluggish { get; set; }

        /// <summary>
        /// Amount Sluggish 1 to 30
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto de mora 1 a 30
        /// </summary>
        [Column("Monto_Mora1_30", TypeName = "float")]
        public decimal AmountSluggish1to30 { get; set; }

        /// <summary>
        /// Amount Sluggish 1 to 60
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto de mora 1 a 60
        /// </summary>
        [Column("Monto_Mora31_60", TypeName = "float")]
        public decimal AmountSluggish31to60 { get; set; }

        /// <summary>
        /// Amount Sluggish 61 or more
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto de mora 61 o más
        /// </summary>
        [Column("Monto_Mora61", TypeName = "float")]
        public decimal AmountSluggish61orMore { get; set; }

        /// <summary>
        /// Amount without Sluggish
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto sin mora
        /// </summary>
        [Column("Monto_Mora_0", TypeName = "float")]
        public decimal AmountWithoutSluggish { get; set; }

        /// <summary>
        /// Percentage Sluggish
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje monto mora
        /// </summary>
        [Column("PorcMontoMora", TypeName = "float")]
        public decimal PercentageSluggish { get; set; }

        /// <summary>
        /// Percentage Sluggish 1 to 30
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje de mora 1 a 30
        /// </summary>
        [Column("PorcMontoMora130", TypeName = "float")]
        public decimal PercentageSluggish1to30 { get; set; }

        /// <summary>
        /// Percentage Sluggish 1 to 60
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje de mora 1 a 60
        /// </summary>
        [Column("PorcMontoMora3160", TypeName = "float")]
        public decimal PercentageSluggish31to60 { get; set; }

        /// <summary>
        /// Percentage Sluggish 61 or more
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje de mora 61 o más
        /// </summary>
        [Column("PorcMontoMora61", TypeName = "float")]
        public decimal PercentageSluggish61orMore { get; set; }

        /// <summary>
        /// Percentage without Sluggish
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje sin mora
        /// </summary>
        [Column("PorcMontoMora0", TypeName = "float")]
        public decimal PercentageWithoutSluggish { get; set; }

        /// <summary>
        /// Amount collected
        /// </summary>
        /// <summary xml:lang="es">
        /// Total recaudado
        /// </summary>
        [Column("totalRecaudado", TypeName = "float")]
        public decimal AmountCollected { get; set; }

        /// <summary>
        /// Raised client
        /// </summary>
        /// <summary xml:lang="es">
        /// Recaudado cliente
        /// </summary>
        [Column("RecaudadoCli", TypeName = "float")]
        public decimal RaisedClient { get; set; }

        /// <summary>
        /// Percentage raised client
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje recaudado cliente
        /// </summary>
        [Column("PorcRecaudadoCli", TypeName = "float")]
        public decimal PercentageRaisedClient { get; set; }

        /// <summary>
        /// Raised debtor
        /// </summary>
        /// <summary xml:lang="es">
        /// Recaudado deudor
        /// </summary>
        [Column("RecaudadoDeu", TypeName = "float")]
        public decimal RaisedDebtor { get; set; }

        /// <summary>
        /// Percentage raised debtor
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje recaudado deudor
        /// </summary>
        [Column("PorcRecaudadoDeu", TypeName = "float")]
        public decimal PercentageRaisedDebtor { get; set; }

        /// <summary>
        /// Amount payments
        /// </summary>
        /// <summary xml:lang="es">
        /// Total de pagos
        /// </summary>
        [Column("totalPagos", TypeName = "int")]
        public int AmountPayments { get; set; }

        /// <summary>
        /// Client payments
        /// </summary>
        /// <summary xml:lang="es">
        /// Pagos cliente
        /// </summary>
        [Column("Pagos_Cliente", TypeName = "int")]
        public int ClientPayments { get; set; }

        /// <summary>
        /// Percentage client payment
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje pago clientes
        /// </summary>
        [Column("PorcPagosCli", TypeName = "float")]
        public decimal PercentageClientPayments { get; set; }

        /// <summary>
        /// Debtor payments
        /// </summary>
        /// <summary xml:lang="es">
        /// Pagos deudor
        /// </summary>
        [Column("Pagos_Deudor", TypeName = "int")]
        public int DebtorPayments { get; set; }

        /// <summary>
        /// Percentage debtor payment
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje pago deudor
        /// </summary>
        [Column("PorcPagosDeu", TypeName = "float")]
        public decimal PercentageDebtorPayments { get; set; }

    }
}
