using System;
using System.Collections.Generic;
using System.Text;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the percentage by client
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa los porcentajes por cliente
    /// </summary>
    public class PercentageResource
    {
        /// <summary>
        /// Percentage Sluggish
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje monto mora
        /// </summary>
        public decimal PercentageSluggish { get; set; }

        /// <summary>
        /// Percentage Sluggish 1 to 30
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje de mora 1 a 30
        /// </summary>
        public decimal PercentageSluggish1to30 { get; set; }

        /// <summary>
        /// Percentage Sluggish 1 to 60
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje de mora 1 a 60
        /// </summary>
        public decimal PercentageSluggish31to60 { get; set; }

        /// <summary>
        /// Percentage Sluggish 61 or more
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje de mora 61 o más
        /// </summary>
        public decimal PercentageSluggish61orMore { get; set; }

        /// <summary>
        /// Percentage without Sluggish
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje sin mora
        /// </summary>
        public decimal PercentageWithoutSluggish { get; set; }
    }
}
