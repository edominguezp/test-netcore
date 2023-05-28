using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Tanner.RelationalDataAccess.Domain;

namespace Tanner.Core.DataAccess.Models
{
    [Table("tb_fin24_cotizador",Schema ="dba")]
    [ExcludeFromCodeCoverage]
    public class DocumentQuotation: IEntity
    {

        /// <summary>
        /// Document ID
        /// </summary>
        /// <summary xml:lang="es">
        /// Documento ID
        /// </summary>
        [Column("id_documento", TypeName = "decimal(8, 0)")]
        public decimal ID { get; set; }


        /// <summary>
        /// Document State
        /// </summary>
        /// <summary xml:lang="es">
        /// Estado de Documento
        /// </summary>
        [Column("estado_documento", TypeName = "int")]
        public decimal DocumentStatus { get; set; }


        /// <summary>
        /// Document Number
        /// </summary>
        /// <summary xml:lang="es">
        /// Numero de Documento
        /// </summary>
        [Column("numero_documento", TypeName = "char(20)")]
        public decimal DocumentNumber { get; set; }

        /// <summary>
        /// Operation Number
        /// </summary>
        /// <summary xml:lang="es">
        /// Numero de Operación
        /// </summary>
        [Column("numero_operacion", TypeName = "decimal(8, 0)")]
        public decimal OperationNumber { get; set; }

    }
}
