using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tanner.Core.API.Model
{
    /// <summary>
    /// Archivo adjunto
    /// </summary>
    /// <summary lang="es">
    /// Attachment
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// Nombre del archivo
        /// </summary>
        /// <summary lang="es">
        /// File name
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Tipo de contenido
        /// </summary>
        /// <summary lang="es">
        /// Mime type
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Contenido del archivo en base64
        /// </summary>
        /// <summary lang="es">
        /// Content base 64
        /// </summary>
        public string ContentBase64 { get; set; }
    }
}
