using System;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Models;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.DataAccess.Repositories.Interfaces
{
    public interface IDocumentRepository : ICoreRepository
    {        
        /// <summary>
        /// Get detail operations by document number
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtiene el detalle de las operaciones dado el número de un documento
        /// </summary>
        /// <param name="rut"></param>
        /// <param name="number">Document number</param>
        /// <returns>Operations detail</returns>
        Task<OperationCollectionResult<DataOperationResource>> GetOperationsbyDocumentNumberAsync(string number, string rut);

        /// <summary>
        /// Update the grant status of the document
        /// </summary>
        /// <param name="documentGrantRequest">Document data</param>
        /// <returns></returns>
        Task<OperationBaseResult> UpdateGrantDocumentAsync(DocumentGrantRequest documentGrantRequest);

        /// <summary>
        /// Get days document
        /// </summary>
        /// <param name="expiryDate">Expiry date</param>
        /// <param name="product">product</param>
        /// <returns>days document</returns>
        Task<OperationResult<int>> GetDayDocumentAsync(DateTime expiryDate, string product);

        /// <summary>
        /// Create documents in a operation
        /// </summary>
        /// <summary xml:lang="es">
        /// Crea documentos en una operación
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<OperationBaseResult> CreateDocumentAsync(CreateDocumentRequest request);
    }
}


