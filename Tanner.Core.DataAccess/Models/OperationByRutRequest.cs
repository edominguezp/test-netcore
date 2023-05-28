namespace Tanner.Core.DataAccess.Models
{
    /// <summary>
    /// Parámetros de búsqueda, filtrado y ordenamiento de operaciones por rut del cliente
    /// </summary>
    public class OperationByRutRequest
    {
        /// <summary>
        /// Página
        /// </summary>
        public int? Page { get; set; }
        /// <summary>
        /// Cantidad de registros por página
        /// </summary>
        public int? PageSize { get; set; }
        /// <summary>
        /// Ordenamiento según columna
        /// </summary>
        public string OrderBy { get; set; }
        /// <summary>
        /// Ordenamiento ascendente o descendente
        /// </summary>
        public string Order { get; set; }
        /// <summary>
        /// Número de la operación
        /// </summary>
        public string OperationNumber { get; set; }

        /// <summary>
        /// Fecha inicio
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// Fecha fin
        /// </summary>
        public string EndDate { get; set; }
    }
}
