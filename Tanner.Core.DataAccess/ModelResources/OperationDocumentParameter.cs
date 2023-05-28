using System;
using System.Collections.Generic;
using System.Text;

namespace Tanner.Core.DataAccess.ModelResources
{
    public class OperationDocumentParameter
    {
        /// <summary>
        /// Client RUT
        /// </summary>
        /// <summary xml:lang="es">
        /// RUT del cliente
        /// </summary>
        public string ClientRUT { get; set; }

        /// <summary>
        /// Debtor RUT
        /// </summary>
        /// <summary xml:lang="es">
        /// RUT del deudor
        /// </summary>
        public string DebtorRUT { get; set; }

        /// <summary>
        /// Documents
        /// </summary>
        /// <summary xml:lang="es">
        /// Documentos a buscar
        /// </summary>
        public string Documents { get; set; }

        /// <summary>
        /// Document Type
        /// </summary>
        /// <summary xml:lang="es">
        /// Tipo de documento
        /// </summary>
        public int DocumentType { get; set; }
        
    }
}
