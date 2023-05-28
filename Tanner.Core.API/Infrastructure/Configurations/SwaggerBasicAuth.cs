using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tanner.Core.API.Infrastructure.Configurations
{
    /// <summary>
    /// Class that represent the parameters of Basic Auth
    /// </summary>
    ///<summary xml:lang="es">
    /// Clase que representa los parametros de la autorización 
    /// </summary>
    public class SwaggerBasicAuth
    {

        /// <summary>
        /// Key basic auth
        /// </summary>
        ///<summary xml:lang="es">
        ///  Llave de acceso a los parámetros
        /// </summary>
        public static string Key = "SwaggerBasicAuth";

        /// <summary>
        /// User API
        /// </summary>
        ///<summary xml:lang="es">
        ///  Usuario de la API
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Password API
        /// </summary>
        ///<summary xml:lang="es">
        ///  Contraseña para la API
        /// </summary>
        public string Password { get; set; }
    }
}
