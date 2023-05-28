using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Results;
using Tanner.RelationalDataAccess.Domain;

namespace Tanner.Core.DataAccess.Repositories.Interfaces
{
    public interface IIntelicomRepository: ITannerRepository
    {
        /// <summary>
        /// Get detail client by RUT
        /// </summary>
        /// <summary lang="es">
        /// Obtener el detalle de un client dado el RUT
        /// </summary>
        /// <param name="rut">Client RUT</param>
        /// <returns>Detail Client</returns>
        Task<OperationResult<ClientDetailResource>> GetDetailClientByRUTAsync(string rut);

        /// <summary>
        /// Get factoring client by RUT
        /// </summary>
        /// <summary lang="es">
        /// Obtener el detalle del factoring dado el RUT
        /// </summary>
        /// <param name="rut">Client RUT</param>
        /// <returns>Detail Client</returns>
        Task<OperationResult<FactoringResource>> GetDetailFactoringByRUTAsync(string rut);

        /// <summary>
        /// Get detail Debtor by RUT
        /// </summary>
        /// <summary lang="es">
        /// Obtener el detalle del deudor dado el RUT
        /// </summary>
        /// <param name="rut">Client RUT</param>
        /// <returns>Detail Debtor</returns>
        Task<OperationCollectionResult<FactoringDebtorResource>> GetDetailDebtorByRUTAsync(string rut);

        /// <summary>
        /// Get detail last operations by RUT
        /// </summary>
        /// <summary lang="es">
        /// Obtener el detalle de las últimas operaciones dado el RUT
        /// </summary>
        /// <param name="rut">Client RUT</param>
        /// <returns>Detail Debtor</returns>
        Task<OperationResult<LastOperationsResource>> GetDetailLastOperationsByRUTAsync(string rut);

        /// <summary>
        /// Get historic operations client by RUT
        /// </summary>
        /// <summary lang="es">
        /// Obtener las operaciones historicas del cliente dado el RUT
        /// </summary>
        /// <param name="rut">Client RUT</param>
        /// <returns>Historic operations</returns>
        Task<OperationResult<HistoricOperationsResource>> GetHistoricOperationsByRUTAsync(string rut);

        /// <summary>
        /// Get weighted term client by RUT
        /// </summary>
        /// <summary lang="es">
        /// Obtener el plazo ponderado el cliente dado el RUT
        /// </summary>
        /// <param name="rut">Client RUT</param>
        /// <returns>weighted term client by RUT</returns>
        Task<OperationCollectionResult<WeightedTermResource>> GetWeightedTermByRUTAsync(string rut);

        /// <summary>
        /// Get historic credits client by RUT
        /// </summary>
        /// <summary lang="es">
        /// Obtener las créditos historicos del cliente dado el RUT
        /// </summary>
        /// <param name="rut">Client RUT</param>
        /// <returns>Historic credits</returns>
        Task<OperationResult<CreditResource>> GetHistoricCreditsByRUTAsync(string rut);

        /// <summary>
        /// Get Total Payments client by RUT
        /// </summary>
        /// <summary lang="es">
        /// Obtener el total de pagos dado el RUT
        /// </summary>
        /// <param name="rut">Client RUT</param>
        /// <returns>Total payments by RUT</returns>
        Task<OperationResult<TotalResource>> GetTotalPaymentsByRUTAsync(string rut);

        /// <summary>
        /// Get percentages balance associated a Client
        /// </summary>
        /// <summary lang="es">
        /// Obtiene los porcentajes de los saldos asociados al cliente
        /// </summary>
        /// <param name="rut">Client RUT</param>
        /// <returns>Percentage without Sluggish by RUT</returns>
        Task<OperationResult<PercentageBalanceSluggishResource>> GetPercentagesBalanceSluggishAsync(string rut);
    }
}
