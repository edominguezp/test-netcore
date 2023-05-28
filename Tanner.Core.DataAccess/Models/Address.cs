using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tanner.RelationalDataAccess.Domain;

namespace Tanner.Core.DataAccess.Models
{
    [Table("tb_fin05", Schema = "dba")]
    public class Address : IEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        ///<summary xml:lang="es">
        /// Dirección id
        /// </summary>
        [Column("id_direccion")]
        public decimal ID { get; set; }

        /// <summary>
        /// Person ID
        /// </summary>
        ///<summary xml:lang="es">
        /// indicador persona
        /// </summary>
        [Column("codigo_persona")]
        public int PersonID { get; set; }
        public Person Person { get; set; }


        /// <summary>
        /// Commune address
        /// </summary>
        ///<summary xml:lang="es">
        /// Comuna de la dirección
        /// </summary>
        [Column("codigo_comuna")]
        public int CommuneID { get; set; }

        /// <summary>
        /// Address of client
        /// </summary>
        ///<summary xml:lang="es">
        /// Dirección del cliente
        /// </summary>
        [Column("direccion")]
        public string AddressClient { get; set; }

        /// <summary>
        /// Number of address
        /// </summary>
        ///<summary xml:lang="es">
        /// Número de la dirección
        /// </summary>
        [Column("numero_direccion")]
        public string Number { get; set; }

        /// <summary>
        /// Client Phone
        /// </summary>
        ///<summary xml:lang="es">
        /// Teléfono del cliente
        /// </summary>
        [Column("fono")]
        public string Phone { get; set; }

        /// <summary>
        /// Country code
        /// </summary>
        ///<summary xml:lang="es">
        /// Código país
        /// </summary>
        [Column("codigo_pais")]
        public int CountryCode { get; set; }
    }
}
