
using System;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the variables of proposed payment
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa las variables del pago propuesto
    /// </summary>
    public class ProposedPaymentResource
    {
        /// <summary>
        /// Proposed Amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto pago propuesto
        /// </summary>
        public long ProposedAmount { get; set; }

        /// <summary>
        /// Proposed All In Rate
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa All In propuesta
        /// </summary>
        public decimal ProposedAllIn { get; set; }

        /// <summary>
        /// Actual Amount
        /// </summary>
        /// <summary xml:lang="es">
        /// Monto pago actual
        /// </summary>
        public long ActualAmount { get; set; }

        /// <summary>
        /// Actual All In Rate
        /// </summary>
        /// <summary xml:lang="es">
        /// Tasa All In actual
        /// </summary>
        public decimal ActualAllIn { get; set; }
        
        /// <summary>
        /// Monthly Rate
        /// </summary>
        /// <summary xml:lang="es">
        /// Pago mensual
        /// </summary>
        //TODO: Remover luego pase Pricing a PROD
        [Obsolete]
        public decimal MonthlyRate
        {
            get
            {
                return ProposedAllIn;
            }
        }
    }
}
