using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Tanner.RelationalDataAccess.Domain;

namespace Tanner.Core.DataAccess.Models
{
    [Table("tb_fin17", Schema = "dba")]
    [ExcludeFromCodeCoverage]
    public class Operation : IEntity
    {
        /// <summary>
        /// Operation Number
        /// </summary>
        /// <summary xml:lang="es">
        /// Numero de Operación
        /// </summary>
        [Column("numero_operacion", TypeName = "decimal(8, 0)")]
        public decimal ID { get; set; }

        /// <summary>
        /// Quote Number
        /// </summary>
        /// <summary xml:lang="es">
        /// Numero de Cotización
        /// </summary>
        [Column("numero_cotizacion", TypeName = "decimal(8, 0)")]
        public decimal QuotationNumber { get; set; }
    }
}
