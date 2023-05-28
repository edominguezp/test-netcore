namespace Tanner.Core.DataAccess.Models
{
    public class SummaryStates
    {
        /// <summary>
        /// Operation state description
        /// </summary>
        /// <summary xml:lang="es">
        /// Descripción del estado de la operación
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Operation state total
        /// </summary>
        /// <summary xml:lang="es">
        /// Total de operaciones por estado
        /// </summary>
        public int Total { get; set; }
    }
}
