using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Tanner.RelationalDataAccess.Domain;

namespace Tanner.Core.DataAccess.Models.Segpriv
{
    [Table("usuario", Schema = "dba")]
    [ExcludeFromCodeCoverage]
    public class User : IEntity
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("login_name", TypeName = "char(15)")]
        public string LoginName { get; set; }

        [Required]
        [Column("password", TypeName = "char(15)")]
        public string Password { get; set; }

        [Column("sucursal", TypeName = "int")]
        public int BranchOffice { get; set; }

        [Column("applpass", TypeName = "char(64)")]
        public string Applpass { get; set; }

        [Column("supervisor", TypeName = "decimal(1,0)")]
        public decimal? Supervisor { get; set; }

        [Column("fyh_cambiopass", TypeName = "datetime")]
        public DateTime FyhCambiopass { get; set; }

        [Column("reintentos", TypeName = "smallint")]
        public int Retries { get; set; }

        [Column("bloqueado", TypeName = "char(1)")]
        public char Blocked { get; set; }

        [Column("ausente", TypeName = "char(1)")]
        public char Absent { get; set; }

        [Column("nombre", TypeName = "varchar(64)")]
        public string Name { get; set; }

        [Column("apellido", TypeName = "varchar(64)")]
        public string Surname { get; set; }

        [Column("cargo", TypeName = "varchar(64)")]
        public string Charge { get; set; }
        [Column("formato_pass", TypeName = "char(1)")]
        public char PassFormat { get; set; }

        [Column("CORREO", TypeName = "varchar(255)")]
        public string Email { get; set; }

        [Column("TELEFONO", TypeName = "varchar(100)")]
        public string Telephone { get; set; }

        [Column("rut_usuario", TypeName = "char(10)")]
        public string Rut { get; set; }

        [Column("tipo_usuario", TypeName = "char(30)")]
        public string UserType { get; set; }

        public virtual ICollection<UserProfile> Profiles { get; set; }

    }
}
