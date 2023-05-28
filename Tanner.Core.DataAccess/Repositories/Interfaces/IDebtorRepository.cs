using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.DataAccess.Repositories.Interfaces
{
    /// <summary>
    /// Interface to Debtor repository
    /// </summary>
    public interface IDebtorRepository : ICoreRepository
    {
        /// <summary>
        /// Debtor Creation
        /// </summary>
        /// <summary lang="es">
        /// Se crea el deudor
        /// </summary>
        /// <param name="debtor"></param>
        /// <returns>Data of the debtor created</returns>
        Task<OperationResult<DebtorBaseResource>> AddDebtorAsync(DebtorBaseResource debtor);

        /// <summary>
        /// Update state of electronic receiver
        /// </summary>
        /// <summary xml:lang="es">
        ///  Actualiza el estado del receptor electrónico
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Response Code and answer</returns>
        Task<OperationBaseResult> UpdateElectronicReceiverAsync(UpdateElectronicReceiver request);

        /// <summary>
        /// Get data debtor by rut
        /// </summary>
        /// <summary xml:lang="es">
        ///  Obtiene los datos del deudor
        /// </summary>
        /// <param name="rut"> Debtor RUT </param>
        /// <returns> Data of debtor </returns>
        Task<OperationResult<DebtorDataResource>> GetDebtorDetailAsync(string rut);

        /// <summary>
        /// Get data documents
        /// </summary>
        /// <summary xml:lang="es">
        ///  Obtiene los datos del documento
        /// </summary>
        /// <returns> Data of documents </returns>
        Task<OperationCollectionResult<DocumentDebtorResource>> GetDataDocumentsDebtorAsync();
    }
}
