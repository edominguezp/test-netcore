using Tanner.Core.DataAccess.Extensions;
using Tanner.Core.DataAccess.Models;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the address of client
    /// </summary>
    ///<summary xml:lang="es">
    /// Clase que representa la dirección del cliente
    /// </summary>
    public class AddressResource
    {
        /// <summary>
        /// Address ID
        /// </summary>
        ///<summary xml:lang="es">
        /// Identificador de la dirección
        /// </summary>
        public decimal ID { get; set; }

        /// <summary>
        /// Person ID
        /// </summary>
        ///<summary xml:lang="es">
        /// ID de la persona
        /// </summary>
        public int PersonID { get; set; }

        /// <summary>
        /// Commune address
        /// </summary>
        ///<summary xml:lang="es">
        /// Comuna de la dirección
        /// </summary>
        public int CommuneID { get; set; }

        /// <summary>
        /// Address of client
        /// </summary>
        ///<summary xml:lang="es">
        /// Dirección del cliente
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Number of address
        /// </summary>
        ///<summary xml:lang="es">
        /// Número de la dirección
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Client Phone
        /// </summary>
        ///<summary xml:lang="es">
        /// Telefono del cliente
        /// </summary>
        public string Phone { get; set; }

        public static implicit operator AddressResource(Address data)
        {
            var result = new AddressResource
            {
                ID = data.ID,
                PersonID = data.PersonID,
                CommuneID = data.CommuneID,
                Address = data.AddressClient,
                Number = data.Number,
                Phone = data.Phone
            };
            return result;
        }

        public static (string, object) Query_GetAddress(decimal id)
        {
            var query = $@"
                SELECT * 
	                FROM 
		                dba.tb_fin05 
	                WHERE 
		                id_direccion = @{nameof(id)}
                        ";
            var param = new
            {
             id 
            };
            (string, object) result = (query, param);
            return result;
        }

        /// <summary>
        /// Query that represent the detail of address
        /// </summary>
        ///<summary xml:lang="es">
        /// Consulta que representa la dirección del cliente
        /// </summary>
        public static (string, object) Query_AddressDetailByRUT(string rut)
        {
            var query = $@"
                DECLARE @rut_cliente VARCHAR (50)
                SET @rut_cliente = @{nameof(rut)}
                DECLARE @RUTC VARCHAR(50)
                SET @RUTC = replace(@rut_cliente,'-','') 
                SET @RUTC = right( '0000000000' + @RUT , 10 ) 

                select     
		                id_direccion as ID,
                        ISNULL(LTRIM(RTRIM(DIR.direccion)),'') AS Address,
                        ISNULL(LTRIM(RTRIM(DIR.numero_direccion)),'') AS Number,
                        ISNULL(LTRIM(RTRIM(DIR.fono)),'') AS Phone,
		                ISNULL(LTRIM(RTRIM(comuna)),'') AS Commune

                from  
		                dba.tb_fin41 as PER 

                INNER join (select id_direccion, codigo_persona, direccion, numero_direccion, fono, comuna from dba.tb_fin05 with (nolock)) DIR on ( PER.codigo_persona = DIR.codigo_persona) 

                where   
		                PER.rut_persona = @RUTC

                order by DIR.id_direccion

                        ";
            var param = new
            {
                rut = rut.FillRUT(false)
            };
            (string, object) result = (query, param);
            return result;
        }
    }
}
