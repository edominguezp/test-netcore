using System.ComponentModel.DataAnnotations.Schema;

namespace Tanner.Core.DataAccess.Models
{
    public partial class ClientProfile 
    {        
        /// <summary>
        /// RUT debtor
        /// </summary>
        /// <summary xml:lang="es">
        /// RUT del deudor
        /// </summary>
        [Column("Rut_Deu_Mayor", TypeName = "char(20)")]
        public string RUTDebtor { get; set; }

        /// <summary>
        /// Debtor Name
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre del deudor
        /// </summary>
        [Column("Deu_Mayor", TypeName = "char(20)")]
        public string Debtor { get; set; }

        /// <summary>
        /// Amount debtor
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto Deudor mayor
        /// </summary>
        [Column("MontoDeuMayor", TypeName = "float")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Percentage debtor 
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje deudor mayor
        /// </summary>
        [Column("PorcDeudorMayor", TypeName = "float")]
        public decimal Percentage { get; set; }

        /// <summary>
        /// RUT debtor 2
        /// </summary>
        /// <summary xml:lang="es">
        /// RUT del deudor 2
        /// </summary>
        [Column("Rut_Deu_Mayor2", TypeName = "char(20)")]
        public string RUTDebtor2 { get; set; }

        /// <summary>
        /// Amount debtor 2
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto Deudor mayor 2
        /// </summary>
        [Column("MontoDeuMayor2", TypeName = "float")]
        public decimal Amount2 { get; set; }

        /// <summary>
        /// Debtor Name 2
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre del deudor 2
        /// </summary>
        [Column("Deu_Mayor2", TypeName = "char(20)")]
        public string Debtor2 { get; set; }

        /// <summary>
        /// Percentage debtor 2
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje deudor mayor 2
        /// </summary>
        [Column("PorcDeudorMayor2", TypeName = "float")]
        public decimal Percentage2 { get; set; }

        /// <summary>
        /// RUT debtor 3
        /// </summary>
        /// <summary xml:lang="es">
        /// RUT del deudor 3
        /// </summary>
        [Column("Rut_Deu_Mayor3", TypeName = "char(20)")]
        public string RUTDebtor3 { get; set; }

        /// <summary>
        /// Amount debtor 3
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto Deudor mayor 3
        /// </summary>
        [Column("MontoDeuMayor3", TypeName = "float")]
        public decimal Amount3 { get; set; }

        /// <summary>
        /// Debtor Name 3
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre del deudor 3
        /// </summary>
        [Column("Deu_Mayor3", TypeName = "char(20)")]
        public string Debtor3 { get; set; }

        /// <summary>
        /// Percentage debtor 3
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje deudor mayor 3
        /// </summary>
        [Column("PorcDeudorMayor3", TypeName = "float")]
        public decimal Percentage3 { get; set; }

        /// <summary>
        /// RUT debtor 4
        /// </summary>
        /// <summary xml:lang="es">
        /// RUT del deudor 4
        /// </summary>
        [Column("Rut_Deu_Mayor4", TypeName = "char(20)")]
        public string RUTDebtor4 { get; set; }

        /// <summary>
        /// Amount debtor 4
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto Deudor mayor 4
        /// </summary>
        [Column("MontoDeuMayor4", TypeName = "float")]
        public decimal Amount4 { get; set; }

        /// <summary>
        /// Debtor Name 4
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre del deudor 4
        /// </summary>
        [Column("Deu_Mayor4", TypeName = "char(20)")]
        public string Debtor4 { get; set; }
        
        /// <summary>
        /// Percentage debtor 4
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje deudor mayor 4
        /// </summary>
        [Column("PorcDeudorMayor4", TypeName = "float")]
        public decimal Percentage4 { get; set; }

    }
}
