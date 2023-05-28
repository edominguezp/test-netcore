using System.ComponentModel.DataAnnotations.Schema;

namespace Tanner.Core.DataAccess.Models
{
    public partial class ClientProfile
    {
        /// <summary>
        /// Credit operations
        /// </summary>
        /// <summary xml:lang="es">
        /// Operaciones de crédito
        /// </summary>
        [Column("ope_credito", TypeName = "int")]
        public int CreditOperations { get; set; }

        /// <summary>
        /// Weighted Term
        /// </summary>
        /// <summary xml:lang="es">
        /// Plazo ponderado
        /// </summary>
        [Column("Plazo_PP_credito", TypeName = "float")]
        public decimal WeightedTerm { get; set; }

        /// <summary>
        /// Minimun credit Rate 
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa mínima de crédito
        /// </summary>
        [Column("MinTasa_credito", TypeName = "float")]
        public decimal MinimunRate { get; set; }

        /// <summary>
        /// TPP credit
        /// </summary>
        /// <summary xml:lang="es">
        /// TPP crédito
        /// </summary>
        [Column("TPP_credito", TypeName = "float")]
        public decimal TPP { get; set; }

        /// <summary>
        /// Maximun credit rate 
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa máxima de crédito
        /// </summary>
        [Column("MaxTasa_credito", TypeName = "float")]
        public decimal MaximunRate { get; set; }

    }
}
