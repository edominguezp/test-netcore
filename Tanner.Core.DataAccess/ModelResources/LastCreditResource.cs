using System;
using System.Collections.Generic;
using System.Text;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the last credits by client
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa los últimos créditos por cliente
    /// </summary>
    public class LastCreditResource
    {

        /// <summary>
        /// Credits amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Montos de créditos
        /// </summary>
        public decimal CreditAmount { get; set; }

        /// <summary>
        /// Credit rate
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa de créditos
        /// </summary>
        public decimal CreditRate { get; set; }

    }
}
