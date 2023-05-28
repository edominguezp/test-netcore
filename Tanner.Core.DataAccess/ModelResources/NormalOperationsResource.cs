using System;
using System.Collections.Generic;
using System.Text;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the normals operation
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa las operaciones normales
    /// </summary>
    public class NormalOperationsResource
    {
        /// <summary>
        /// Normal date
        /// </summary>
        /// <summary xml:lang="es">
        /// Fecha normal
        /// </summary>
        public DateTime NormalDate { get; set; }

        /// <summary>
        /// Normal code
        /// </summary>
        /// <summary xml:lang="es">
        /// Código normal
        /// </summary>
        public string NormalCode { get; set; }

        /// <summary>
        /// Normal amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto normal
        /// </summary>
        public decimal NormalAmount { get; set; }

        /// <summary>
        /// Normal rate
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa normal
        /// </summary>
        public decimal NormalRate { get; set; }

        /// <summary>
        /// Normal commission
        /// </summary>
        /// <summary xml:lang="es">
        /// Comisión normal
        /// </summary>
        public decimal NormalCommission { get; set; }

        /// <summary>
        /// Normal expenses
        /// </summary>
        /// <summary xml:lang="es">
        /// Gastos normal
        /// </summary>
        public decimal NormalExpenses { get; set; }
    }
}
