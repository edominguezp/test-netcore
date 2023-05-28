
namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class representing the Banks defined in CORE.
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa los bancos definidos en CORE.
    /// </summary>
    public class BankResource
    {
        /// <summary>
        /// Bank Code
        /// </summary>
        /// <summary xml:lang="es">
        /// Código de banco
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Bank Name
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre de banco
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ID Bank Name
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre de banco con ID
        /// </summary>
        public string IDBankName { get; set; }

        /// <summary>
        /// Sbif code
        /// </summary>
        /// <summary xml:lang="es">
        /// Codigo de la sbif
        /// </summary>
        public int SbifCode { get; set; }

        public static string Query_GetBank()
        {
            var query = $@"
                    select Banco.codigo_banco as ID,
	                       ltrim(rtrim(Banco.descripcion_banco)) as Name,
                           convert(varchar(5), Banco.codigo_banco)+' - '+ltrim(rtrim(Banco.descripcion_banco)) as IDBankName,
						   sbif.codigo_sbif SbifCode
                    FROM DBA.TB_FIN26 Banco
                    left join dba.tb_SBIF sbif on Banco.codigo_banco = sbif.codigo_banco 
                    order by 
	                        Banco.codigo_banco asc
                        ";
            return query;
        }

        public static string Query_GetFirtBank()
        {
            var query = $@"
                    select top 1 Banco.codigo_banco as ID,
	                       ltrim(rtrim(Banco.descripcion_banco)) as Name,
                           convert(varchar(5), Banco.codigo_banco)+' - '+ltrim(rtrim(Banco.descripcion_banco)) as IDBankName,
						   sbif.codigo_sbif SbifCode
                    FROM DBA.TB_FIN26 Banco
                    left join dba.tb_SBIF sbif on Banco.codigo_banco = sbif.codigo_banco
                    order by 
	                        Banco.codigo_banco asc
                        ";
            return query;
        }
    }
}
