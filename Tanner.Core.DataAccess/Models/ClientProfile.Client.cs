using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Tanner.RelationalDataAccess.Domain;

namespace Tanner.Core.DataAccess.Models
{
    [Table("Perfil_Cliente", Schema = "INE_MODELOS")]
    [ExcludeFromCodeCoverage]
    public partial class ClientProfile: IEntity
    {
        /// <summary>
        /// Client ID (int part of RUT)
        /// </summary>
        /// <summary xml:lang="es">
        /// Identificador del cliente (Parte entera del RUT)
        /// </summary>
        [Column("RUT")]
        public int ID { get; set; }

        /// <summary>
        /// Client RUT
        /// </summary>
        /// <summary xml:lang="es">
        /// RUT del cliente
        /// </summary>
        [Column("RUT_CLIENTE", TypeName = "char(10)")]
        public string RUT { get; set; }

        /// <summary>
        /// Client Name
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre del cliente
        /// </summary>
        [Column("CLIENTE", TypeName = "char(500)")]
        public string Name { get; set; }

        /// <summary>
        /// Validity status
        /// </summary>
        /// <summary xml:lang="es">
        /// Glosa estado vigencia
        /// </summary>
        [Column("glosa_estado_vigencia", TypeName = "char(50)")]
        public string ValidityStatus { get; set; }

        /// <summary>
        /// Debtor lock
        /// </summary>
        /// <summary xml:lang="es">
        /// Bloqueo de deudor
        /// </summary>
        [Column("BLOQUEO_DEUDOR", TypeName = "char(10)")]
        public string DebtorLock { get; set; }

        /// <summary>
        /// Commercial lock
        /// </summary>
        /// <summary xml:lang="es">
        /// Bloqueo comercial
        /// </summary>
        [Column("BLQ_COMERCIAL", TypeName = "char(10)")]
        public string CommercialLock { get; set; }

        /// <summary>
        /// Name zone
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre de la zona
        /// </summary>
        [Column("Zona", TypeName = "char(50)")]
        public string Zone { get; set; }

        /// <summary>
        /// Name branch office
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre de la sucursal
        /// </summary>
        [Column("Sucursal", TypeName = "char(50)")]
        public string BranchOffice{ get; set; }

        /// <summary>
        /// Executive name
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre del ejecutivo
        /// </summary>
        [Column("Ejecutivo", TypeName = "char(255)")]
        public string Executive { get; set; }

        /// <summary>
        /// Line business
        /// </summary>
        /// <summary xml:lang="es">
        /// Rubro
        /// </summary>
        [Column("Rubro", TypeName = "char(255)")]
        public string LineBusiness { get; set; }

        /// <summary>
        /// Sales section
        /// </summary>
        /// <summary xml:lang="es">
        /// Tramo de ventas
        /// </summary>
        [Column("TramoVentas", TypeName = "float")]
        public decimal SalesSection { get; set; }
    }
}
