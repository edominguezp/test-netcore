using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Tanner.RelationalDataAccess.Domain;

namespace Tanner.Core.DataAccess.Models
{
    [Table("tbl_comunas", Schema = "dbo")]
    [ExcludeFromCodeCoverage]
    public class Commune : IEntity
    {
        /// <summary>
        /// Commune Code
        /// </summary>
        /// <summary xml:lang="es">
        /// Código de comuna
        /// </summary>
        [Column("codigo_comuna")]
        public int ID { get; set; }
    }
}
