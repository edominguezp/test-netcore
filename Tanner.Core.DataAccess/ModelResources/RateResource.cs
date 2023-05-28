using System;
using System.Collections.Generic;
using System.Text;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the Sluggish rate
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa la tasa de mora
    /// </summary>
    public class RateResource
    {
        /// <summary>
        /// Weighted Sluggish rate
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa de mora ponderada
        /// </summary>
        public decimal WeightedSluggishRate { get; set; }

        /// <summary>
        /// Maximum Sluggish rate
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa de mora máxima
        /// </summary>
        public decimal MaximumSluggishRate { get; set; }

        /// <summary>
        /// Maximum Sluggish Days
        /// </summary>
        /// <summary xml:lang="es">
        /// Días de mora máxima
        /// </summary>
        public decimal MaximumSluggishDays { get; set; }

        /// <summary>
        /// Sluggish weightd days
        /// </summary>
        /// <summary xml:lang="es">
        /// Días de mora ponderada
        /// </summary>
        public decimal SluggishWeightdDays { get; set; }
    }
}
