using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tanner.RelationalDataAccess.Domain;

namespace Tanner.Core.DataAccess.Models.Segpriv
{
    [Table("perfil_usuario", Schema = "dbo")]
    public class UserProfile : IEntity
    {
        [Column("id_perfil", TypeName = "int")]
        public int ProfileId { get; set; }

        [Column("login_name", TypeName = "char(15)")]
        public string LoginName { get; set; }

        [ForeignKey("LoginName")]
        public virtual User User { get; set; }
    }
}

