using System.ComponentModel.DataAnnotations.Schema;

namespace Tanner.Core.DataAccess.Models
{
    public partial class ClientProfile
    {
        /// <summary>
        /// Standard weighted term
        /// </summary>
        /// <summary xml:lang="es">
        /// Plazo ponderado norma
        /// </summary>
        [Column("PlazoPondNorma", TypeName = "float")]
        public decimal Operations { get; set; }

        /// <summary>
        /// Standard reoperation term
        /// </summary>
        /// <summary xml:lang="es">
        /// Plazo ponderado reoperación
        /// </summary>
        [Column("PlazoPondReop", TypeName = "float")]
        public decimal Reoperations { get; set; }

        /// <summary>
        /// Normal debtors number
        /// </summary>
        /// <summary xml:lang="es">
        /// Número de deudores normal
        /// </summary>
        [Column("NumDeudoresNorma", TypeName = "int")]
        public int NormalDebtorsNumber { get; set; }

        /// <summary>
        /// Reoperation debtors number
        /// </summary>
        /// <summary xml:lang="es">
        /// Número de deudores de reoperaciones
        /// </summary>
        [Column("NumDeudoresReop", TypeName = "int")]
        public int ReoperationDebtorsNumber { get; set; }

        /// <summary>
        /// Normal minimun rate
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa mímima normal
        /// </summary>
        [Column("MinTasaNorma", TypeName = "float")]
        public decimal NormalMinRate { get; set; }

        /// <summary>
        /// Reoperation maximun rate
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa máxima reoperación 
        /// </summary>
        [Column("MaxTasaReop", TypeName = "float")]
        public decimal ReoperationMaxRate { get; set; }

        /// <summary>
        /// Normal PP rate
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa PP normal 
        /// </summary>
        [Column("TasaPPNorma", TypeName = "float")]
        public decimal NormalPPRate { get; set; }

        /// <summary>
        /// Reoperation PP rate
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa PP reoperación 
        /// </summary>
        [Column("TasaPPReop", TypeName = "float")]
        public decimal ReoperationPPRate { get; set; }

        /// <summary>
        /// Normal maximun rate
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa máxima normal
        /// </summary>
        [Column("MaxTasaNorma", TypeName = "float")]
        public decimal NormalMaxRate { get; set; }

        /// <summary>
        /// Reoperation minimun rate
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa mímima reoperación
        /// </summary>
        [Column("MinTasaReop", TypeName = "float")]
        public decimal ReoperationMinRate { get; set; }
        
    }
}
