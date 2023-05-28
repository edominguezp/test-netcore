using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Tanner.RelationalDataAccess.Domain;

namespace Tanner.Core.DataAccess.Models
{
    [Table("tb_fin41", Schema = "dba")]
    [ExcludeFromCodeCoverage]
    public class Person : IEntity
    {
        /// <summary>
        /// Person Code
        /// </summary>
        /// <summary xml:lang="es">
        /// Código de Persona
        /// </summary>
        [Column("codigo_persona", TypeName = "int")]
        public int ID { get; set; }

        /// <summary>
        /// Person Rut
        /// </summary>
        /// <summary xml:lang="es">
        /// Rut de persona
        /// </summary>
        [Column("rut_persona", TypeName = "char(10)")]
        public string RUT { get; set; }
    }
}
