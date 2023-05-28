using System;
using System.Collections.Generic;
using System.Text;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the answer of SP   
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa las respuestas que entrega el SP
    /// </summary>
    public class CodeSPResponse
    {
        /// <summary>
        /// Answer Code
        /// </summary>
        /// <summary xml:lang="es">
        /// Código de respuesta
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Answer content
        /// </summary>
        /// <summary xml:lang="es">
        /// Contenido de la respuesta
        /// </summary>
        public string Answer { get; set; }
    }
}
