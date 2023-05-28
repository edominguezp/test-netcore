using System.Threading.Tasks;
using Tanner.Core.DataAccess.Enums;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Models;
using Tanner.Core.DataAccess.Results;
using Tanner.RelationalDataAccess.Domain;

namespace Tanner.Core.DataAccess.Repositories.Interfaces
{
    public interface IOperationRepository : ICoreRepository
    {
        /// <summary>
        /// Get data operations by Executive o Agent
        /// </summary>
        /// <summary lang="es">
        /// Obtener los datos de las operaciones de un ejecutivo o agente
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Data operations</returns>
        Task<OperationCollectionResult<OperationResource>> GetOperationsByExecutiveOrAgentAsync(OperationByExecutiveOrAgent request);


        /// <summary>
        /// Get the operation data by number
        /// </summary>
        /// <summary lang="es">
        /// Obtener dado el número de operación los datos de la operación
        /// </summary>
        /// <param name="number">Operation number</param>
        /// <returns>Operation data</returns>
        Task<OperationDetailResource> GetOperationsByNumberAsync(int number);


        /// <summary>
        /// Get documents asociated a operation number
        /// </summary>
        /// <summary lang="es">
        /// Obtener los documentos de una operación
        /// </summary>
        /// <param name="operationNumber">Operation number</param>
        /// <returns>Operation summary with documents</returns>
        Task<OperationResult<OperationSummaryResource>> GetDocumentsByOperationAsync(int operationNumber);
               

        /// <summary>
        /// Get operations by state
        /// </summary>
        /// <param name="request">Request</param>
        /// <summary lang="es">
        /// Obtener las operaciones por estado
        /// </summary>
        /// <returns>Operation summary by state </returns>
        Task<OperationCollectionResult<OperationsStatesResource>> GetOperationsByStateAsync(OperationByStatus request);


        /// <summary>
        /// Get proposed payment
        /// </summary>
        /// <summary lang="es">
        /// Obtener el pago propuesto
        /// </summary>
        /// <returns>Proposed payment by number operation</returns>
        Task<OperationResult<ProposedPaymentResource>> GetProposedPaymentAsync(int number, CommercialTermsResource request);


        /// <summary>
        /// Get data by operation or quotation
        /// </summary>
        /// <summary lang="es">
        /// Obtener la data de los documentos por numero de documento y código de operación o cotización
        /// </summary>
        /// <param name="isQuotation">true or false for quotation</param>
        /// <param name="number">number opertation</param>
        /// <returns>Data operation or quotation</returns>
        Task<OperationResult<OperationDetailSummaryResource>> GetOperationSummaryAsync(bool isQuotation, int number);


        /// <summary>
        /// Get documents by operation number
        /// </summary>
        /// <summary lang="es">
        /// Obtener los documentos por número de operación
        /// </summary>
        /// <param name="Number">Operation number</param>
        /// <returns>Operation documents</returns>
        Task<OperationCollectionResult<SummaryDocumentResource>> GetDataDocumentsByOperationAsync(int number);


        /// <summary>
        /// Get summary documents by operation number
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener el total de documentos por número de operación
        /// </summary>
        /// <param name="number"></param>
        /// <returns>Summary documents associated an operation </returns>
        Task<OperationCollectionResult<DocumentSummaryResource>> GetSummaryDocumentsByOperationAsync(int number);


        /// <summary>
        /// Update operation with the new commercial conditions
        /// </summary>
        /// <summary lang="es">
        /// Actualizar operación con las nuevas condiciones comerciales
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Operation response code</returns>
        Task<OperationBaseResult> UpdateCommercialTermOperationAsync(UpdateCommercialTermOperation request);

        /// <summary>
        /// Valid if the document already exists in CORE
        /// </summary>
        /// <summary lang="es">
        /// Valida si el documento ya existe en CORE
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Document status in CORE</returns>
        Task<OperationCollectionResult<ValidateDocumentResource>> ValidateDocument(OperationDocumentParameter request);

        /// <summary>
        /// Get the amount of Quotation
        /// </summary>
        /// <summary lang="es">
        /// Obtiene las los montos de la cotización
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Amounts Quotation</returns>
        Task<OperationResult<QuoteAmountResource>> GetAmountQuotationByNumberQuotationAsync(int number);


        /// <summary>
        /// Update the Dates of operation
        /// </summary>
        /// <summary xml:lang="es">
        ///  Actualiza las fechas de la operación
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Response Code and answer</returns>
        Task<OperationBaseResult> UpdateDatesOperationAsync(UpdateDatesOperation request);

        /// <summary>
        /// Update the state of approved 
        /// </summary>
        /// <summary xml:lang="es">
        /// Actualizar el estado del visto bueno
        /// </summary>
        /// <param name="request"></param>
        /// <returns> Operation response code </returns>
        Task<OperationBaseResult> RegisterApproved(RegisterApprovedParameters request);

        /// <summary>
        /// Mark document like rejected
        /// </summary>
        /// <summary xml:lang="es">
        /// Marcar documento como rechazado
        /// </summary>
        /// <param name="request"></param>
        /// <returns>If document was mark like rejected</returns>
        Task<OperationBaseResult> MarkRejectedDocument(UpdateDocument request);

        /// <summary>
        /// Insert Operation
        /// </summary>
        /// <summary xml:lang="es">
        /// Insertar operación
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Return the number of operation</returns>
        Task<OperationResult<long?>> InsertOperation(InsertOperation request);

        /// <summary>
        /// Simulation and create Operation 
        /// </summary>
        /// <sumary xml:lang="es">
        /// simula y crea una operación
        /// </sumary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<OperationResult<long?>> SimulationAndCreateOperation(SimulationAndCreateOperationResource request);

        /// <summary>
        /// Get operation document by RUT customer
        /// </summary>
        /// <sumary xml:lang="es">
        /// Obtiene los documento de la operacion por RUT del cliente
        /// </sumary>
        /// <param name="rut"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<OperationResult<OperationDocumentResource>> GetOperationDocumentByRUTAsync(string rut, OperationByRutRequest request);

        /// <summary>
        /// Get settlement form by operation number
        /// </summary>
        /// <sumary xml:lang="es">
        /// Obtiene la planilla de liquidación por número de operación
        /// </sumary>
        /// <param name="operationNumber">Operation number</param>
        /// <returns>Return settlement form data</returns>
        Task<OperationResult<SettlementFormResource>> GetSettlementFormByOperationNumberAsync(long operationNumber);

        /// <summary>
        /// Get documents by operation ID
        /// </summary>
        /// <sumary xml:lang="es">
        /// Obtiene los documentos por número de operación
        /// </sumary>
        /// <param name="operationNumber">Operation number</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Return document operation</returns>
        Task<OperationResult<OperationDocumentByIDResource>> GetOperationDocumentByIDAsync(long operationNumber, int? page, int? pageSize);

        /// <summary>
        /// Get operation status
        /// </summary>
        /// <sumary xml:lang="es">
        /// Obtiene el estado de una operación
        /// </sumary>
        /// <param name="number">Operation number</param>
        /// <returns>Operation status</returns>
        Task<OperationResult<OperationStatusResource>> GetStatusAsync(long number);

        /// <summary>
        /// Get operation by user
        /// </summary>
        /// <sumary xml:lang="es">
        /// Obtiene las operaciónes por usuario
        /// </sumary>
        /// <param name="userID">user ID</param>
        /// <returns>Operation data by user</returns>
        Task<OperationResult<OperationUserResource>> GetOperationByUserAsync(string userID);

        /// <summary>
        /// Get credit detail
        /// </summary>
        /// <sumary xml:lang="es">
        /// Obtiene detalles del credito
        /// </sumary>
        /// <param name="number">Operation number</param>
        /// <returns>Operation credit detail</returns>
        Task<OperationResult<CreditDetailResource>> GetCreditDetailAsync(long number);

        /// <summary>
        /// Create operation without simulation
        /// </summary>
        /// <summary xml:lang="es">
        /// Crea una operación sin simular
        /// </summary>
        /// <param name="clientCode"></param>
        /// <returns></returns>
        Task<OperationResult<long?>> CreateOperationWithClientInfoNoSimulation(CreateOperationWithClientNoSimulation request);

        /// <summary>
        /// Gets an operation by document data.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<OperationResult<long?>> GetOperationByDocumentAsync(OperationByDocumentRequest request);
    }
}


