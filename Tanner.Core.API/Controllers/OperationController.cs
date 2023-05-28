using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Tanner.Core.API.Model;
using Tanner.Core.DataAccess;
using Tanner.Core.DataAccess.Commands;
using Tanner.Core.DataAccess.Enums;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Models;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.API.Controllers
{
    /// <summary>
    /// Operation controller
    /// </summary>
    [Produces("application/json")]
    public class OperationController : BaseController<OperationController>
    {
        private readonly IOperationRepository _repository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IFileRepository _fileRepository;
        private readonly ILogger<OperationController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="mediator"></param>
        /// <param name="repository"></param>
        /// <param name="employeeRepository"></param>
        /// <param name="fileRepository"></param>
        public OperationController(ILogger<OperationController> logger, IMediator mediator, IOperationRepository repository,
            IEmployeeRepository employeeRepository, IFileRepository fileRepository) : base(logger, mediator)
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
            _fileRepository = fileRepository;
            _logger = logger;
        }

        /// <summary>
        /// Get operation data by number
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener la operación de acuerdo a número de operación
        /// </summary>
        /// <param name="number">Operation number</param>
        /// <returns>Operation detail</returns>
        [ProducesResponseType(typeof(OperationDetailResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{number}")]
        public async Task<ActionResult> Get([FromRoute] int number)
        {
            OperationDetailResource element = await _repository.GetOperationsByNumberAsync(number);
            if (element == null)
            {
                return NotFound(number);
            }
            var result = new OperationResult<OperationDetailResource>
            {
                Data = element
            };
            return Ok(result);
        }

        /// <summary>
        /// Get operations by employee asociated to email
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener operaciones de un empleado asociado a un correo electrónico
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Operations</returns>
        [ProducesResponseType(typeof(CollectionResult<OperationResource>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("ByExecutiveOrAgent")]
        public async Task<ActionResult> GetByExecutiveOrAgent([FromQuery] OperationByExecutiveOrAgent request)
        {
            bool existEmployee = await _employeeRepository.ExistEmployeeAsync(request.Email);
            if (!existEmployee)
            {
                return NotFound(request.Email);
            }

            OperationCollectionResult<OperationResource> result = await _repository.GetOperationsByExecutiveOrAgentAsync(request);
            return ReturnCode(result);
        }

        /// <summary>
        /// Get data and documents of operation by number operation
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener los datos y los documentos de una operación por numero de operación
        /// </summary>
        /// <param name="number"></param>
        /// <returns>Documents of operation associated a operation </returns>
        [ProducesResponseType(typeof(OperationSummaryResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet]
        [Route("{number}/Documents")]
        public async Task<ActionResult> GetDocuments([FromRoute]int number)
        {
            OperationResult<OperationSummaryResource> result = await _repository.GetDocumentsByOperationAsync(number);
            return ReturnCode(result);
        }

        /// <summary>
        /// Get all operations by state
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener las operaciones por estados
        /// </summary>
        /// <returns>All operations by Status</returns>
        [ProducesResponseType(typeof(CollectionResult<OperationsStatesResource>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("ByStatus")]
        public async Task<ActionResult<CollectionResult<OperationsStatesResource>>> Get([FromQuery] OperationByStatus request)
        {
            OperationCollectionResult<OperationsStatesResource> result = await _repository.GetOperationsByStateAsync(request);
            return ReturnCode(result);
        }

        /// <summary>
        /// Get Proposed payment and monthly Rate
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener el pago propuesto y la tasa mensual
        /// </summary>
        /// <param name="number">Number of operation</param>
        /// <param name="request">Request</param>
        /// <returns>Operations</returns>
        [ProducesResponseType(typeof(ProposedPaymentResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet]
        [Route("{number}/ProposedPayment")]
        public async Task<ActionResult> GetProposedPayment([FromRoute]int number, [FromQuery] CommercialTermsResource request)
        {
            OperationResult<ProposedPaymentResource> result = await _repository.GetProposedPaymentAsync(number, request);
            return ReturnCode(result);
        }

        /// <summary>
        /// Get business iteration by user, operation and ID
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener la iteración de la negociación por ID, usuario y operación
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(FileResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{number}/Files/{id}")]
        public async Task<IActionResult> Get([FromRoute] decimal number, [FromRoute] int id)
        {
            OperationResult<FileResource> result = await _fileRepository.GetFileByID(number, id);
            return ReturnCode(result);
        }

        /// <summary>
        /// Save file by operation number
        /// </summary>
        /// <summary xml:lang="es">
        /// Guardar archivo por número de operación
        /// </summary>
        /// <param name="number">Operation number</param>
        /// <param name="command"></param>
        /// <returns>File data</returns>
        [ProducesResponseType(typeof(FileResource), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPost]
        [Route("{number}/InsertFile")]
        public async Task<ActionResult<FileResource>> Post([FromRoute] int number, InsertFileOperationCommand command)
        {
            if (number != command.OperationNumber)
            {
                return BadRequest($"Number {number} is different to {nameof(command)}.{nameof(command.OperationNumber)} {command.OperationNumber}");
            }

            OperationResult<FileResource> result;

            bool existOperation = await _repository.AnyAsync<Operation>(t => t.ID == command.OperationNumber);
            if (!existOperation)
            {
                var msg = $"Operación: {command.OperationNumber} no existe";
                result = OperationResult<FileResource>.NotFoundResult(command.OperationNumber);
            }
            else
            {
                result = await Mediator.Send(command);
            }

            return ReturnCode(result, nameof(OperationController), nameof(Get), routeValues: new { number, id = result.Data?.ID });
        }

        /// <summary>
        /// Get data operation by operation or quotation
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener datos por operación o cotización
        /// </summary>
        /// <returns>Data operation or quotation</returns>
        [ProducesResponseType(typeof(OperationDetailSummaryResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{number}/GetSummary")]
        public async Task<ActionResult<OperationDetailSummaryResource>> GetOperationSummary([FromRoute]int number, [FromQuery] bool isQuotation)
        {
            OperationResult<OperationDetailSummaryResource> result = await _repository.GetOperationSummaryAsync(isQuotation, number);
            return ReturnCode(result);
        }

        /// <summary>
        /// Get documents by number operation
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener los documentos por número de operación
        /// </summary>
        /// <param name="number"></param>
        /// <returns>Documents associated an operation </returns>
        [ProducesResponseType(typeof(IEnumerable<SummaryDocumentResource>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet]
        [Route("{number}/DataDocuments")]
        public async Task<ActionResult> GetDataDocuments([FromRoute]int number)
        {
            OperationCollectionResult<SummaryDocumentResource> result = await _repository.GetDataDocumentsByOperationAsync(number);
            return ReturnCode(result);
        }

        /// <summary>
        /// Get summary documents by operation number
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener el total de documentos por número de operación
        /// </summary>
        /// <param name="number"></param>
        /// <returns>Summary documents associated an operation </returns>
        [ProducesResponseType(typeof(IEnumerable<DocumentSummaryResource>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet]
        [Route("{number}/SummaryDocuments")]
        public async Task<ActionResult> GetSummaryDocuments([FromRoute]int number)
        {
            OperationCollectionResult<DocumentSummaryResource> result = await _repository.GetSummaryDocumentsByOperationAsync(number);
            return ReturnCode(result);
        }

        /// <summary>
        /// Update operation with the new commercial conditions
        /// </summary>
        /// <summary xml:lang="es">
        /// Actualizar operación con las nuevas condiciones comerciales
        /// </summary>
        /// <param name="request"></param>
        /// <returns> Operation response code </returns>
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPatch]
        [Route("{number}/UpdateCommercialTerm")]
        public async Task<ActionResult> UpdateCommercialTermOperation(UpdateCommercialTermOperation request)
        {
            OperationBaseResult result = await _repository.UpdateCommercialTermOperationAsync(request);
            return ReturnCode(result);
        }

        /// <summary>
        /// Get the amount of Quotation
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener datos por numero de cotización
        /// </summary>
        /// <returns>Data quotation</returns>
        [ProducesResponseType(typeof(QuoteAmountResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{number}/GetQuotationAmount")]
        public async Task<ActionResult<QuoteAmountResource>> GetAmountQuotationByNumberQuotation([FromRoute]int number)
        {
            OperationResult<QuoteAmountResource> result = await _repository.GetAmountQuotationByNumberQuotationAsync(number);
            return ReturnCode(result);
        }

        /// <summary>
        /// Update the Dates of operation
        /// </summary>
        /// <summary xml:lang="es">
        /// Actualiza las fechas de la operación
        /// </summary>
        /// <param name="request"></param>
        /// <returns> Code and Answer of process  </returns>
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPatch]
        [Route("{number}/UpdateDatesOperation")]
        public async Task<ActionResult> UpdateDatesOfOperation(UpdateDatesOperation request)
        {
            OperationBaseResult result = await _repository.UpdateDatesOperationAsync(request);
            return ReturnCode(result);
        }

        /// <summary>
        /// Update the state of approved
        /// </summary>
        /// <summary xml:lang="es">
        /// Actualizar el estado del visto bueno
        /// </summary>
        /// <param name="request"></param>
        /// <returns> Operation response code </returns>
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPatch]
        [Route("{number}/RegisterApproved")]
        public async Task<ActionResult> RegisterApproved(RegisterApprovedParameters request)
        {
            if (!Enum.IsDefined(typeof(ApprovedState), request.ApprovedState))
            {
                return BadRequest($"Estado de visto bueno: {request.ApprovedState} no es válido");
            }
            OperationBaseResult result = await _repository.RegisterApproved(request);
            return ReturnCode(result);
        }

        /// <summary>
        /// Mark Rejected Document
        /// </summary>
        /// <summary xml:lang="es">
        /// Marcar documento rechazado
        /// </summary>
        /// <param name="request"></param>
        /// <returns> Response Code </returns>
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPatch]
        [Route("{number}/MarkRejectedDocument")]
        public async Task<ActionResult> MarkRejectedDocument(UpdateDocument request)
        {
            if (!Enum.IsDefined(typeof(InvoiceCode), request.InvoiceCode))
            {
                return BadRequest($"Código de rechazo: { request.InvoiceCode} no válido ");
            }
            OperationBaseResult result = await _repository.MarkRejectedDocument(request);
            return ReturnCode(result);
        }

        /// <summary>
        /// Returns list if the list of documents exists in core
        /// </summary>
        /// <summary xml:lang="es">
        /// Retorna listado si el listado de documentos existe en core
        /// </summary>
        /// <param name="request"></param>
        /// <returns> Table with validated documents </returns>
        [ProducesResponseType(typeof(CollectionResult<ValidateDocumentResource>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet]
        [Route("ValidateDocuments")]
        public async Task<ActionResult> ValidateDocuments([FromQuery]OperationDocumentParameter request)
        {
            OperationCollectionResult<ValidateDocumentResource> result = await _repository.ValidateDocument(request);
            return ReturnCode(result);
        }

        /// <summary>
        /// Insert operation
        /// </summary>
        /// <summary xml:lang="es">
        /// Ingresar una operación
        /// </summary>
        /// <returns> Operation number</returns>
        [ProducesResponseType(typeof(long?), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((typeof(ErrorResponse)), (int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [Route("InsertOperation")]
        public async Task<ActionResult> InsertOperation(InsertOperation request)
        {
            OperationResult<long?> result = await _repository.InsertOperation(request);
            return ReturnCode(result);
        }

        /// <summary>
        /// Simulation and Create operation
        /// </summary>
        /// <summary xml:lang="es">
        /// Simula y crea una nueva operación
        /// Si el deudor no existe como cliente, responderá con error
        /// Si el deudor no existe como deudor, responderá con error
        /// Si el cliente no existe como cliente, lo creará
        /// </summary>
        /// <returns> Operation number</returns>
        [ProducesResponseType(typeof(long?), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [Route("SimulationAndCreate")]
        public async Task<ActionResult> SimulationAndCreateOperation(SimulationAndCreateOperationResource request)
        {
            OperationResult<long?> result = await _repository.SimulationAndCreateOperation(request);

            return ReturnCode(result);
        }

        /// <summary>
        /// Create an operation without simulating
        /// </summary>
        /// <summary xml:lang="es">
        /// Si el deudor no existe como cliente, responderá con error
        /// Si el deudor no existe como deudor, responderá con error
        /// Si el cliente no existe como cliente, lo creará
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(OperationResult<long?>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [Route("CreateOperationDirect")]
        public async Task<ActionResult> CreateOperationDirect(CreateOperationWithClientNoSimulation request)
        {
            OperationResult<long?> result = await _repository.CreateOperationWithClientInfoNoSimulation(request);
            return ReturnCode(result);
        }

        /// <summary>
        /// Get data and documents of operation by RUT customer
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener los datos y los documentos de la operación por RUT del cliente
        /// </summary>ccc
        /// <param name="rut">Customer RUT</param>
        /// <param name="request">Parámetros de filtrado y ordenamiento</param>
        /// <returns>Documents of operation</returns>
        [ProducesResponseType(typeof(OperationDocumentResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{rut}/RUTClient")]
        public async Task<ActionResult> GetOperationByRUT([FromRoute] string rut, [FromQuery] OperationByRutRequest request)
        {
            OperationResult<OperationDocumentResource> result = await _repository.GetOperationDocumentByRUTAsync(rut, request);
            return ReturnCode(result);
        }

        /// <summary>
        /// Get settlement form by operation number
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener los datos de la planilla de liquidación por número de operación
        /// </summary>ccc
        /// <param name="number">Operation number</param>
        /// <returns>Return settlement form data</returns>
        [ProducesResponseType(typeof(SettlementFormResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{number}/SettlementForm")]
        public async Task<ActionResult> GetSettlementFormByOperationID([FromRoute] long number)
        {
            OperationResult<SettlementFormResource> result = await _repository.GetSettlementFormByOperationNumberAsync(number);
            return ReturnCode(result);
        }

        /// <summary>
        /// Get documents form by operation number
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener los documentos por número de operación
        /// </summary>
        /// <param name="number">Operation number</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Return documents by operation number</returns>
        [ProducesResponseType(typeof(OperationDocumentByIDResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{number}/DocumentByOperationID")]
        public async Task<ActionResult> GetDocumentsByOperationID([FromRoute] long number, [FromQuery] int? page, int? pageSize)
        {
            OperationResult<OperationDocumentByIDResource> result = await _repository.GetOperationDocumentByIDAsync(number, page, pageSize);
            return ReturnCode(result);
        }


        /// <summary>
        /// Get operation status
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener el estado de una operación.
        /// </summary>
        /// <param name="number">Operation number</param>
        /// <returns>Operation status</returns>
        [ProducesResponseType(typeof(OperationStatusResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet]
        [Route("{number}/Status")]
        public async Task<ActionResult> GetStatus([FromRoute] long number)
        {
            OperationResult<OperationStatusResource> result = await _repository.GetStatusAsync(number);
            return ReturnCode(result);
        }

        /// <summary>
        /// Get operation data by user
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener la operación por usuario.
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <returns>Operation data by user</returns>
        [ProducesResponseType(typeof(OperationUserResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet]
        [Route("{userID}/user")]
        public async Task<ActionResult> GetOperationByUser([FromRoute] string userID)
        {
            OperationResult<OperationUserResource> result = await _repository.GetOperationByUserAsync(userID);
            return ReturnCode(result);
        }

        /// <summary>
        /// Get operation credit detail
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener el detalle del credito de la operación.
        /// </summary>
        /// <param name="number">Operation number</param>
        /// <returns>Operation credit detail</returns>
        [ProducesResponseType(typeof(CreditDetailResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{number}/CreditDetail")]
        public async Task<ActionResult> GetCreditDetailt([FromRoute] long number)
        {
            OperationResult<CreditDetailResource> result = await _repository.GetCreditDetailAsync(number);
            return ReturnCode(result);
        }

        /// <summary>
        /// Get operation by document data.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("byDocument")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<long?>> GetOperationByDocument([FromBody] OperationByDocumentRequest request)
        {
            OperationResult<long?> result = await _repository.GetOperationByDocumentAsync(request);
            return ReturnCode(result);
        }
    }
}