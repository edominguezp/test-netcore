using Tanner.Core.DataAccess.Extensions;

namespace Tanner.Core.DataAccess.ModelResources
{
    /// <summary>
    /// Class that represent the historic operations by client
    /// </summary>
    /// <summary xml:lang="es">
    /// Clase que representa las operaciones históricas por cliente
    /// </summary>
    public class HistoricOperationsResource
    {
        /// <summary>
        /// Total operations
        /// </summary>
        /// <summary xml:lang="es">
        /// total de operaciones
        /// </summary>
        public int TotalOperations { get; set; }

        /// <summary>
        /// Total factoring operations
        /// </summary>
        /// <summary xml:lang="es">
        /// total de operaciones factoring
        /// </summary>
        public int TotalFactoringOperations { get; set; }

        /// <summary>
        /// Total normal operations
        /// </summary>
        /// <summary xml:lang="es">
        /// total de operaciones normales
        /// </summary>
        public int TotalNormalOperations { get; set; }

        /// <summary>
        /// Total reoperations
        /// </summary>
        /// <summary xml:lang="es">
        /// total de reoperaciones
        /// </summary>
        public int TotalOperationsReoperation { get; set; }

        /// <summary>
        /// Total credits
        /// </summary>
        /// <summary xml:lang="es">
        /// total de créditos
        /// </summary>
        public int TotalCreditOperations { get; set; }

        public static (string, object) Query_HistoricOperationsByRUT(string rut)
        {
            var query = $@"
                SELECT 
	                Ope_totales as TotalOperations,
	                Ope_totalesFact as TotalFactoringOperations,
	                NumOperNorma as TotalNormalOperations,
	                NumOperReop as TotalOperationsReoperation,
	                Ope_Creditos as TotalCreditOperations
                FROM
                    INE_MODELOS.Perfil_Cliente
		        WHERE
                    RUT_CLIENTE = @{nameof(rut)}";
            var param = new
            {
                rut = rut.FillRUT(false)
            };
            (string, object) result = (query, param);
            return result;
        }
    }
}
