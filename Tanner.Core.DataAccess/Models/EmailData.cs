using System;
using System.Collections.Generic;
using System.Text;

namespace Tanner.Core.DataAccess.Models
{
    /// <summary>
    /// Datos del email
    /// </summary>
    /// <summary lang="es">
    /// Email data
    /// </summary>
    public class EmailData
    {
        /// <summary>
        /// Email del deudor
        /// </summary>
        /// <summary lang="es">
        /// Debtor Email
        /// </summary>
        public string EmailDebtor { get; set; }

        /// <summary>
        /// Nombre del deudor
        /// </summary>
        /// <summary lang="es">
        /// Debtor name
        /// </summary>
        public string DebtorName { get; set; }

        public string CompleteDebtorRUT { get; set; }

    }
}
