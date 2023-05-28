using System;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent indicators associated
    /// </summary>
    ///<summary xml:lang="es">
    /// Clase que representa los indicadores asociados
    /// </summary>
    public class IndicatorsResource
    {
        /// <summary>
        /// Rate of operation
        /// </summary>
        ///<summary xml:lang="es">
        /// Tasa de operación
        /// </summary>
        public string Rate { get; set; }

        /// <summary>
        /// Commission of operation
        /// </summary>
        ///<summary xml:lang="es">
        /// Comisión de la operación
        /// </summary>
        public string Commission { get; set; }

        /// <summary>
        /// Gastos of operation
        /// </summary>
        ///<summary xml:lang="es">
        /// Gastos de la operación
        /// </summary>
        public string Expense { get; set; }

        /// <summary>
        /// Date of operation
        /// </summary>
        ///<summary xml:lang="es">
        /// Fecha de la operación
        /// </summary>
        public DateTime OperationDate { get; set; }

        /// <summary>
        /// Code of product
        /// </summary>
        ///<summary xml:lang="es">
        /// Código del producto
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// Operation Amount
        /// </summary>
        ///<summary xml:lang="es">
        /// Monto de la operación
        /// </summary>
        public decimal OperationAmount { get; set; }
    }
}
