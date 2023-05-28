namespace Tanner.Core.DataAccess.ModelResources
{
    public class MainContactResource
    {
        // <summary>
        /// Contact name
        /// </summary>
        /// <summary xml:lang="es">
        /// Nombre de contacto
        /// </summary>
        public string ContactName { get; set; }

        // <summary>
        /// Contact phone
        /// </summary>
        /// <summary xml:lang="es">
        /// Teléfono de contacto
        /// </summary>
        public string ContactPhone { get; set; }

        // <summary>
        /// Contact adress
        /// </summary>
        /// <summary xml:lang="es">
        /// Dirección de contacto
        /// </summary>
        public string ContactAddress { get; set; }

        // <summary>
        /// Contact Email
        /// </summary>
        /// <summary xml:lang="es">
        /// Correo electrónico de contacto
        /// </summary>
        public string ContactEmail { get; set; }

        public static (string, object) Query_MainContactByRUT(string rut)
        {
            var query = $@"SET @rut = Right('0000000000'+Ltrim(Replace(@rut,'-','')),10)
                 SELECT c.nombre_contacto as ContactName, 
                    c.telefono as ContactPhone, 
                    c.direccion as ContactAddress, 
                    c.mail as ContactEmail
                 FROM dba.tbl_contacto c with(nolock)
                 INNER JOIN dba.tb_fin41 p with(nolock) ON c.codigo_persona = p.codigo_persona
                 WHERE c.contacto_principal = 1
                 AND p.rut_persona = @rut";

            var param = new
            {
                rut
            };

            (string, object) result = (query, param);
            
            return result;
        }
    }
}
