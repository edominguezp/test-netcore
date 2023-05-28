using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Tanner.RelationalDataAccess.Domain;

namespace Tanner.Core.DataAccess.Models
{
    [Table("LINEA_ARCHIVO")]
    [ExcludeFromCodeCoverage]
    public class FileLine : IEntity
    {
        /// <summary>
        /// File ID
        /// </summary>
        /// <summary xml:lang="es">
        /// Identificador del fichero
        /// </summary>
        [Column("id_archivo")]
        public int ID { get; set; }

        /// <summary>
        /// Line ID
        /// </summary>
        /// <summary xml:lang="es">
        /// ID de la Linea
        /// </summary>
        [Column("id_linea")]
        public int LineID { get; set; }

        /// <summary>
        /// File ID
        /// </summary>
        /// <summary xml:lang="es">
        /// Identificador del fichero
        /// </summary>
        [Column("tipo_archivo")]
        public int FileType { get; set; }
    }
}
