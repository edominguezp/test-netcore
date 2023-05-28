using System;
using System.Collections.Generic;
using System.Text;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the payments of the client
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa los pagos por cliente
    /// </summary>
    public class PaymentResource
    {
        /// <summary>
        /// Amount payments
        /// </summary>
        /// <summary xml:lang="es">
        /// Total de pagos
        /// </summary>
        public int AmountPayments { get; set; }

        /// <summary>
        /// Percentage payments
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje de pagos
        /// </summary>
        public int PercentagePayments { get; set; }
    }
}
