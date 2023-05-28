using System;
using System.Collections.Generic;
using System.Text;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the collections of amounts
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa la colección de documentos
    /// </summary>
    public class CollectionResource
    {
        /// <summary>
        /// Amount collected
        /// </summary>
        /// <summary xml:lang="es">
        /// Total recaudado
        /// </summary>
        public decimal AmountCollected { get; set; }

        /// <summary>
        /// Percentage collected
        /// </summary>
        /// <summary xml:lang="es">
        /// Porcentaje recaudado
        /// </summary>
        public decimal PercentageCollected { get; set; }

    }
}
