using System.ComponentModel.DataAnnotations.Schema;

namespace Tanner.Core.DataAccess.Models
{
    public partial class ClientProfile
    {
        /// <summary>
        /// Total operations
        /// </summary>
        /// <summary xml:lang="es">
        /// total de operaciones
        /// </summary>
        [Column("Ope_totales", TypeName = "int")]
        public int TotalOperations { get; set; }

        /// <summary>
        /// Total factoring operations
        /// </summary>
        /// <summary xml:lang="es">
        /// total de operaciones factoring
        /// </summary>
        [Column("Ope_totalesFact", TypeName = "int")]
        public int TotalFactoringOperations { get; set; }

        /// <summary>
        /// Total normal operations
        /// </summary>
        /// <summary xml:lang="es">
        /// total de operaciones normales
        /// </summary>
        [Column("NumOperNorma", TypeName = "int")]
        public int TotalNormalOperations { get; set; }

        /// <summary>
        /// Total reoperations
        /// </summary>
        /// <summary xml:lang="es">
        /// total de reoperaciones
        /// </summary>
        [Column("NumOperReop", TypeName = "int")]
        public int TotalOperationsReoperation { get; set; }

        /// <summary>
        /// Total credits
        /// </summary>
        /// <summary xml:lang="es">
        /// total de créditos
        /// </summary>
        [Column("Ope_Creditos", TypeName = "int")]
        public int TotalCreditOperations { get; set; }

    }
}
