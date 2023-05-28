using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tanner.Core.API.Infrastructure.Configurations
{
    /// <summary>
    /// Configuración para el envío de correo
    /// </summary>
    /// <summary lang="es">
    /// Configuration for send email
    /// </summary>
    public class EmailConfiguration
    {
        /// <summary>
        /// Codigo de la configuración
        /// </summary>
        /// <summary lang="es">
        /// Key configuration
        /// </summary>
        public static string Key = "EmailConfiguration";

        /// <summary>
        /// Suscripción del API de envío de correos electrónicos
        /// </summary>
        /// <summary lang="es">
        /// Subscription key
        /// </summary>
        public string SubscriptionKey { get; set; }
    }
}
