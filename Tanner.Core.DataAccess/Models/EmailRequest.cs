using System.Collections.Generic;

namespace Tanner.Core.API.Model
{
    /// <summary>
    /// Solicitud de envío de correo electrónico
    /// </summary>
    /// <summary lang="es">
    /// Email request
    /// </summary>
    public class EmailRequest
    {
        /// <summary>
        /// Email ID
        /// </summary>
        /// <summary lang="es">
        /// ID del correo electrónico
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Nombre del usuario
        /// </summary>
        /// <summary lang="es">
        /// User Name
        /// </summary>
        public string FromName { get; set; }

        /// <summary>
        /// Asunto
        /// </summary>
        /// <summary lang="es">
        /// Subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Lista de destinatarios
        /// </summary>
        /// <summary lang="es">
        /// List to send mail
        /// </summary>
        public List<EmailAddress> ToAddress { get; set; }

        /// <summary>
        /// Lista de destinatarios copia
        /// </summary>
        /// <summary lang="es">
        /// List copy email
        /// </summary>
        public List<EmailAddress> CcAddress { get; set; }

        /// <summary>
        /// Lista de destinatarios copia oculta
        /// </summary>
        /// <summary lang="es">
        /// List copy email 
        /// </summary>
        public List<EmailAddress> CcoAddress { get; set; }

        /// <summary>
        /// Cuerpo del mensaje
        /// </summary>
        /// <summary lang="es">
        /// Body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Cuerpo del mensaje en HTML
        /// </summary>
        /// <summary lang="es">
        /// Bool is body
        /// </summary>
        public bool IsBodyHtml { get; set; }

        /// <summary>
        /// Lista de archivos adjuntos
        /// </summary>
        /// <summary lang="es">
        /// Attachments
        /// </summary>
        public List<Attachment> Attachments { get; set; }
    }
}
