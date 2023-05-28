using System;
using System.Collections.Generic;
using System.Text;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the amount of documents
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa el monto de los documentos
    /// </summary>
    public class AmountResource
    {
        /// <summary>
        /// Total Amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto total
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Amount Sluggish
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto total mora
        /// </summary>
        public decimal AmountSluggish { get; set; }

        /// <summary>
        /// Amount Sluggish 1 to 30
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto de mora 1 a 30
        /// </summary>
        public decimal AmountSluggish1to30 { get; set; }

        /// <summary>
        /// Amount Sluggish 1 to 60
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto de mora 1 a 60
        /// </summary>
        public decimal AmountSluggish31to60 { get; set; }

        /// <summary>
        /// Amount Sluggish 61 or more
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto de mora 61 o más
        /// </summary>
        public decimal AmountSluggish61orMore { get; set; }

        /// <summary>
        /// Amount without Sluggish
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto sin mora
        /// </summary>
        public decimal AmountWithoutSluggish { get; set; }
    }
}
