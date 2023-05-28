using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Tanner.RelationalDataAccess.Domain;

namespace Tanner.Core.DataAccess.Models
{
    [Table("OPER_ARCHIVO")]
    [ExcludeFromCodeCoverage]
    public class OperationFile : IEntity
    {
        // <summary>
        /// Operation Number
        /// </summary>
        /// <summary xml:lang="es">
        /// Numero de Operación
        /// </summary>
        [Column("numero_operacion", TypeName = "decimal(8, 0)")]
        public decimal OperationID { get; set; }

        /// <summary>
        /// File ID
        /// </summary>
        /// <summary xml:lang="es">
        /// Identificador del fichero
        /// </summary>
        [Column("id_archivo")]
        public int FileID { get; set; }
        public File File { get; set; }
    }
}
