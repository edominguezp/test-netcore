using System;
using System.ComponentModel.DataAnnotations;
using Tanner.Core.DataAccess.Enums;

namespace Tanner.Core.DataAccess.Models
{
    /// <summary>
    /// Document grant status
    /// </summary>
    public class DocumentGrantRequest
    {
        /// <summary>
        /// Operation number
        /// </summary>
        /// <summary xml:lang="es">
        /// Número de operación
        /// </summary>
        [Required]
        public long OperationNumber { get; set; }

        /// <summary>
        /// Document number
        /// </summary>
        /// <summary xml:lang="es">
        /// Número de documento
        /// </summary>
        [Required]
        public long DocumentNumber { get; set; }

        /// <summary>
        /// Debtor RUT
        /// </summary>
        /// <summary xml:lang="es">
        /// RUT del deudor
        /// </summary>
        [Required]
        public string DebtorRUT { get; set; }

        /// <summary>
        /// Banck Code
        /// </summary>
        /// <summary xml:lang="es">
        /// Codigo de banco
        /// </summary>
        [Required]
        public GrantStatus GrantStatus { get; set; }

        /// <summary>
        /// Grant date
        /// </summary>
        /// <summary xml:lang="es">
        /// Fecha de cesión
        /// </summary>
        [Required]
        public DateTime GrantDate { get; set; }

        /// <summary>
        /// Grant result
        /// </summary>
        /// <summary xml:lang="es">
        /// Resultado de cesión
        /// </summary>
        [Required]
        public string GrantResult { get; set; }
    }
}
