using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Tanner.RelationalDataAccess.Domain;

namespace Tanner.Core.DataAccess.Models
{
    [Table("cuenta_entidad", Schema = "dbo")]
    [ExcludeFromCodeCoverage]
    public class Account : IEntity
    {
        /// <summary>
        /// Banck Code
        /// </summary>
        /// <summary xml:lang="es">
        /// Codigo de banco
        /// </summary>
        [Column("codigo_banco", TypeName = "int")]
        public decimal ID { get; set; }

        /// <summary>
        /// Current Account 
        /// </summary>
        /// <summary xml:lang="es">
        /// Cuenta Corriente
        /// </summary>
        [Column("ctacte", TypeName = "char(20)")]
        public string CurrentAcount { get; set; }

        /// <summary>
        /// Code Person
        /// </summary>
        /// <summary xml:lang="es">
        /// Codigo de Persona
        /// </summary>
        [Column("codigo_persona", TypeName = "int")]
        public int CodePerson { get; set; }

    }
}
