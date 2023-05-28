using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Tanner.RelationalDataAccess.Domain;

namespace Tanner.Core.DataAccess.Models
{
    [Table("ARCHIVO")]
    [ExcludeFromCodeCoverage]
    public class File : IEntity
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
        /// File URL
        /// </summary>
        /// <summary xml:lang="es">
        /// URL del archivo
        /// </summary>
        [Column("filesUrl")]
        public string URL { get; set; }

        /// <summary>
        /// URL Directory
        /// </summary>
        /// <summary xml:lang="es">
        /// Directorio del archivo
        /// </summary>
        [Column("fileDirectory")]
        public string Directory { get; set; }

        /// <summary>
        /// File name
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre del archivo
        /// </summary>
        [Column("nm_file")]
        public string Name { get; set; }
    }
}
