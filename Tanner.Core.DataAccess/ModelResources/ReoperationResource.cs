using System;
using System.Collections.Generic;
using System.Text;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the reoperations by client
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa las reoperaciones por cliente
    /// </summary>
    public class ReoperationResource
    {
        /// <summary>
        /// Reoperation date
        /// </summary>
        /// <summary xml:lang="es">
        /// Fecha reoperación
        /// </summary>
        public DateTime ReoperationDate { get; set; }

        /// <summary>
        /// Reoperation code
        /// </summary>
        /// <summary xml:lang="es">
        /// Código reoperación
        /// </summary>
        public string ReoperationCode { get; set; }

        /// <summary>
        /// Reoperation amount 1
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto reoperación 1
        /// </summary>
        public decimal ReoperationAmount { get; set; }

        /// <summary>
        /// Reoperation rate 1
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa reoperación 1
        /// </summary>
        public decimal ReoperationRate { get; set; }

        /// <summary>
        /// Reoperation commission 1
        /// </summary>
        /// <summary xml:lang="es">
        /// Comisión reoperación 1
        /// </summary>
        public decimal ReoperationCommission { get; set; }

        /// <summary>
        /// Reoperation expenses 1
        /// </summary>
        /// <summary xml:lang="es">
        /// Gastos reoperación 1
        /// </summary>
        public decimal ReoperationExpenses { get; set; }
    }
}
