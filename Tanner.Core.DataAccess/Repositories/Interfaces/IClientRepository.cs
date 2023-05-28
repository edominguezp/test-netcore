
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.DataAccess.Repositories.Interfaces
{
    /// <summary>
    /// Interface to client repository
    /// </summary>
    public interface IClientRepository : ICoreRepository
    {
        /// <summary>
        /// Get data client by RUT
        /// </summary>
        /// <summary lang="es">
        /// Obtener los datos de un cliente dado el RUT
        /// </summary>
        /// <param name="rut">Client RUT</param>
        /// <returns>Data client</returns>
        Task<OperationResult<ClientResource>> GetClientByRUTAsync(string rut);


        /// <summary>
        /// Get financial detail of client by RUT
        /// </summary>
        /// <summary lang="es">
        /// Obtener el detalle financiero de un cliente dado el RUT
        /// </summary>
        /// <param name="rut">Client RUT</param>
        /// <returns>financial detail</returns>
        Task<OperationResult<FinancialDetailResource>> GetFinancialDetailRUTAsync(string rut);

        /// <summary>
        /// Add client
        /// </summary>
        /// <summary lang="es">
        /// Se crea el cliente
        /// </summary>
        /// <param name="client"></param>
        /// <returns>Main data of the created client</returns>
        Task<OperationResult<ClientBaseResource>> AddClientAsync(AddClientResource client);

        
        /// Get detail address by RUT
        /// </summary>
        /// <summary lang="es">
        /// Obtiene el detalle de la dirección del cliente en base al RUT
        /// </summary>
        /// <param name="rut">Client RUT</param>
        /// <returns>Detail address client</returns>
        Task<OperationCollectionResult<AddressResource>> GetAddressDetailByClient(string rut);

        /// Get bank account by client RUT
        /// </summary>
        /// <summary lang="es">
        /// Obtener cuenta bancaria por RUT de cliente
        /// </summary>
        /// <param name="rut">Client RUT</param>
        /// <returns>Detail bank account by client</returns>
        Task<OperationCollectionResult<BankAccountClientResource>> GetBankAccountDetailsByClient(string rut);

        /// Get main contact by client RUT
        /// </summary>
        /// <summary lang="es">
        /// Obtener el contacto principal por RUT de cliente
        /// </summary>
        /// <param name="rut">Client RUT</param>
        /// <returns>Detail main contact data by client</returns>
        Task<OperationResult<MainContactResource>> GetMainContactByClient(string rut);

        /// Get client credit line 
        /// </summary>
        /// <summary lang="es">
        /// Obtener la linea de credito del cliente
        /// </summary>
        /// <param name="rut">Client RUT</param>
        /// <returns>Detail credit line by client</returns>
        Task<OperationResult<ClientCreditLine>> GetClientCreditLine(string rut);

        /// Get credit line by Id
        /// </summary>
        /// <summary lang="es">
        /// Obtener la linea de credito por Id
        /// </summary>
        /// <param name="lineId">Line Id</param>
        /// <returns>Return true if line exist</returns>
        Task<bool> GetCreditLineById(int lineId);

        /// <summary>
        /// Get client code by Rut
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtiene el código del cliente por el Rut
        /// </summary>
        /// <param name="clientRut"></param>
        /// <returns></returns>
        Task<int?> GetClientCodeByRut(string clientRut);
    }
}
