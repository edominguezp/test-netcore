using Tanner.Core.DataAccess.Extensions;
using Tanner.Core.DataAccess.Models;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class representing the client bank account
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa la cuenta bancaria del cliente
    /// </summary>
    public class BankAccountClientResource
    {
        /// <summary>
        /// ID
        /// </summary>
        ///<summary xml:lang="es">
        /// Identificador del Banco
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre del banco
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ID Bank Name
        /// </summary>
        /// <summary xml:lang="es">
        /// Identificador y nombre del banco
        /// </summary>
        public string IDBankName { get; set; }

        /// <summary>
        /// Bank account
        /// </summary>
        /// <summary xml:lang="es">
        /// Cuenta del banco
        /// </summary>
        public string BankAccount { get; set; }

        /// <summary>
        /// Query representing the client bank account
        /// </summary>
        /// <summary xml:lang="es">
        /// Consulta que representa la cuenta bancaria del cliente
        /// </summary>
        public static (string, object) Query_BankAccountDetailByRUT(string rut)
        {
            var query = $@"
                            DECLARE @rut_cliente	VARCHAR (50)
                            DECLARE @codigo_persona Integer

                            SET @rut_cliente = @{nameof(rut)}

                            DECLARE @RUTC VARCHAR(50)
                            SET @RUTC = replace(@rut_cliente,'-','') 
                            SET @RUTC = right( '0000000000' + @RUTC , 10 ) 

                            SELECT @codigo_persona = isnull(c.codigo_persona, 0)
                            FROM  dba.tb_fin01 c With (nolock)
	                            INNER JOIN dba.tb_fin41 p With (nolock) ON c.codigo_persona = p.codigo_persona 
                            WHERE p.rut_persona = @RUTC

                            select Cuenta.codigo_banco as ID,   
	                               (select ltrim(rtrim(Banco.descripcion_banco)) FROM DBA.TB_FIN26 Banco where Banco.codigo_banco  = Cuenta.codigo_banco) as Name,
	                               (select convert(varchar(5), Banco.codigo_banco)+' - '+ltrim(rtrim(Banco.descripcion_banco)) FROM DBA.TB_FIN26 Banco where Banco.codigo_banco  = Cuenta.codigo_banco) as IDBankName,
                                   ltrim(rtrim(ctacte)) as BankAccount         
                            from dbo.cuenta_entidad Cuenta with (nolock)
                            where codigo_persona = @codigo_persona
                            order by Cuenta.codigo_banco asc
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
