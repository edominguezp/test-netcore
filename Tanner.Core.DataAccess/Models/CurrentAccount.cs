using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Tanner.RelationalDataAccess.Domain;

namespace Tanner.Core.DataAccess.Models
{
    [Table("tb_fin51", Schema = "dba")]
    [ExcludeFromCodeCoverage]
    public class CurrentAccount : IEntity
    {
        /// <summary>
        /// Bank code
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
    }
}
