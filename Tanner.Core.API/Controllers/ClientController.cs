using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.Commands;
using Tanner.Core.DataAccess.Enums;
using Tanner.Core.DataAccess.Extensions;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Models;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.API.Controllers
{
    /// <summary>
    /// Client controller
    /// </summary>
    public class ClientController : BaseController<ClientController>
    {
        private readonly IClientRepository _repository;
        private readonly IIntelicomRepository _intelicomRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="mediator"></param>
        /// <param name="repository"></param>
        /// <param name="intelicomRepository"></param>
        public ClientController(ILogger<ClientController> logger, IMediator mediator, IClientRepository repository, IIntelicomRepository intelicomRepository) : base(logger, mediator)
        {
            _repository = repository;
            _intelicomRepository = intelicomRepository;
        }


        /// <summary>
        /// Get data Client
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener los datos del cliente de acuerdo a su rut
        /// </summary>
        /// <param name="rut"></param>
        /// <returns>Data associated a Client </returns>
        [ProducesResponseType(typeof(ClientResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{rut}")]
        public async Task<ActionResult<ClientResource>> Get([FromRoute]string rut)
        {
            //TODO: Validar rut
            OperationResult<ClientResource> client = await _repository.GetClientByRUTAsync(rut);
            return ReturnCode(client);
        }

        /// <summary>
        /// Add Customer Checking Account
        /// </summary>
        /// <summary xml:lang="es">
        /// Agregar Cuenta Corriente Cliente
        /// </summary>       
        /// <returns></returns>
        [ProducesResponseType(typeof(CurrentAccountResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [Route("{rut}/AddAccount")]
        public async Task<ActionResult<CurrentAccountResource>> AddCustomerCheckingAccount([FromRoute]string rut, AddClientCheckingAccountCommand command)
        {
            //TODO Validar AddClientCheckingAccountCommand
            OperationResult<CurrentAccountResource> result = await Mediator.Send(command);
            return ReturnCode(result);
        }

        /// <summary>
        /// Get financial detail by rut
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener el detalle financiero del cliente de acuerdo a su rut
        /// </summary>
        /// <param name="rut"></param>
        /// <returns>Financial detail associated a Client </returns>
        [ProducesResponseType(typeof(FinancialDetailResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{rut}/FinancialDetail")]
        public async Task<ActionResult> GetFinancialDetail([FromRoute]string rut)
        {
            //Validar rut
            OperationResult<FinancialDetailResource> result;
            rut = rut.FillRUT();
            bool existClient = await _repository.AnyAsync<Person>(t => t.RUT == rut);
            if (!existClient)
            {
                var msg = $"Cliente: {rut} no existe";
                result = OperationResult<FinancialDetailResource>.NotFoundResult(rut, msg);
            }
            else
            {
                result = await _repository.GetFinancialDetailRUTAsync(rut);
            }

            return ReturnCode(result);
        }

        /// <summary>
        /// Get client detail by rut
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener el detalle del cliente de acuerdo a su rut
        /// </summary>
        /// <param name="rut"></param>
        /// <returns>Detail associated a Client </returns>
        [ProducesResponseType(typeof(ClientDetailResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{rut}/ClientDetail")]
        public async Task<ActionResult> GetDetail([FromRoute]string rut)
        {
            //Validar rut
            OperationResult<ClientDetailResource> result;
            rut = rut.FillRUT();
            bool existClient = await _repository.AnyAsync<Person>(t => t.RUT == rut);
            if (!existClient)
            {
                var msg = $"Cliente: {rut} no existe";
                result = OperationResult<ClientDetailResource>.NotFoundResult(rut, msg);
            }
            else
            {
                result = await _intelicomRepository.GetDetailClientByRUTAsync(rut);
            }

            return ReturnCode(result);
        }

        /// <summary>
        /// Get factoring detail by rut
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener el detalle del factoring de acuerdo a su rut
        /// </summary>
        /// <param name="rut"></param>
        /// <returns>Detail associated a Client </returns>
        [ProducesResponseType(typeof(FactoringResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{rut}/FactoringDetail")]
        public async Task<ActionResult> GetFactoringDetail([FromRoute]string rut)
        {
            //Validar rut
            OperationResult<FactoringResource> result;
            rut = rut.FillRUT();
            bool existClient = await _repository.AnyAsync<Person>(t => t.RUT == rut);
            if (!existClient)
            {
                var msg = $"Cliente: {rut} no existe";
                result = OperationResult<FactoringResource>.NotFoundResult(rut, msg);
            }
            else
            {
                result = await _intelicomRepository.GetDetailFactoringByRUTAsync(rut);
            }

            return ReturnCode(result);
        }

        /// <summary>
        /// Get debtor detail by rut
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener el detalle del deudor de acuerdo a su rut
        /// </summary>
        /// <param name="rut"></param>
        /// <returns>Detail debtor associated a Client </returns>
        [ProducesResponseType(typeof(IEnumerable<FactoringDebtorResource>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{rut}/DebtorDetail")]
        public async Task<ActionResult> GetDebtorDetail([FromRoute]string rut)
        {
            //Validar rut
            OperationCollectionResult<FactoringDebtorResource> result;
            rut = rut.FillRUT();
            bool existClient = await _repository.AnyAsync<Person>(t => t.RUT == rut);
            if (!existClient)
            {
                var msg = $"Cliente: {rut} no existe";
                result = OperationCollectionResult<FactoringDebtorResource>.NotFoundResult(rut, msg);
            }
            else
            {
                result = await _intelicomRepository.GetDetailDebtorByRUTAsync(rut);
            }

            return ReturnCode(result);
        }
        /// <summary>
        /// Get las operations by rut
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener las últimas operaciones del cliente de acuerdo a su rut
        /// </summary>
        /// <param name="rut"></param>
        /// <returns> Last operations associated a Client </returns>
        [ProducesResponseType(typeof(LastOperationsResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{rut}/LastOperations")]
        public async Task<ActionResult> GetLastOperations([FromRoute]string rut)
        {
            OperationResult<LastOperationsResource> result;
            rut = rut.FillRUT();
            bool existClient = await _repository.AnyAsync<Person>(t => t.RUT == rut);
            if (!existClient)
            {
                var msg = $"Cliente: {rut} no existe";
                result = OperationResult<LastOperationsResource>.NotFoundResult(rut, msg);
            }
            else
            {
                result = await _intelicomRepository.GetDetailLastOperationsByRUTAsync(rut);

            }

            return ReturnCode(result);
        }

        /// <summary>
        /// Get historic operations detail by rut
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener las operaciones historicas de acuerdo a su rut
        /// </summary>
        /// <param name="rut"></param>
        /// <returns>Detail historic operations associated a Client </returns>
        [ProducesResponseType(typeof(HistoricOperationsResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{rut}/HistoricOperations")]
        public async Task<ActionResult> GeHistoricOperationsDetail([FromRoute]string rut)
        {
            //Validar rut
            OperationResult<HistoricOperationsResource> result;
            rut = rut.FillRUT();
            bool existClient = await _repository.AnyAsync<Person>(t => t.RUT == rut);
            if (!existClient)
            {
                var msg = $"Cliente: {rut} no existe";
                result = OperationResult<HistoricOperationsResource>.NotFoundResult(rut, msg);
            }
            else
            {
                result = await _intelicomRepository.GetHistoricOperationsByRUTAsync(rut);
            }

            return ReturnCode(result);
        }
        /// <summary>
        /// Get Weighted term by rut
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener el plazo ponderado acuerdo a su rut
        /// </summary>
        /// <param name="rut"></param>
        /// <returns>Detail weighted term associated a Client </returns>
        [ProducesResponseType(typeof(IEnumerable<WeightedTermResource>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{rut}/WeightedTerm")]
        public async Task<ActionResult> GetWeightedTermDetail([FromRoute]string rut)
        {
            //Validar rut
            OperationCollectionResult<WeightedTermResource> result;
            rut = rut.FillRUT();
            bool existClient = await _repository.AnyAsync<Person>(t => t.RUT == rut);
            if (!existClient)
            {
                var msg = $"Cliente: {rut} no existe";
                result = OperationCollectionResult<WeightedTermResource>.NotFoundResult(rut, msg);
            }
            else
            {
                result = await _intelicomRepository.GetWeightedTermByRUTAsync(rut);
            }

            return ReturnCode(result);
        }

        /// <summary>
        /// Get historic credits detail by rut
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener los créditos historicos de acuerdo a su rut
        /// </summary>
        /// <param name="rut"></param>
        /// <returns>Detail historic credits associated a Client </returns>
        [ProducesResponseType(typeof(CreditResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{rut}/HistoricCredits")]
        public async Task<ActionResult> GeHistoricCreditsDetail([FromRoute]string rut)
        {
            //Validar rut
            OperationResult<CreditResource> result;
            rut = rut.FillRUT();
            bool existClient = await _repository.AnyAsync<Person>(t => t.RUT == rut);
            if (!existClient)
            {
                var msg = $"Cliente: {rut} no existe";
                result = OperationResult<CreditResource>.NotFoundResult(rut, msg);
            }
            else
            {
                result = await _intelicomRepository.GetHistoricCreditsByRUTAsync(rut);
            }

            return ReturnCode(result);
        }

        /// <summary>
        /// Get total payments detail by rut
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener el total de pagos de acuerdo a su rut
        /// </summary>
        /// <param name="rut"></param>
        /// <returns> Total payments associated a Client </returns>
        [ProducesResponseType(typeof(TotalResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{rut}/TotalPayments")]
        public async Task<ActionResult> GetTotalPayments([FromRoute]string rut)
        {
            //Validar rut
            OperationResult<TotalResource> result;
            rut = rut.FillRUT();
            bool existClient = await _repository.AnyAsync<Person>(t => t.RUT == rut);
            if (!existClient)
            {
                var msg = $"Cliente: {rut} no existe";
                result = OperationResult<TotalResource>.NotFoundResult(rut, msg);
            }
            else
            {
                result = await _intelicomRepository.GetTotalPaymentsByRUTAsync(rut);
            }

            return ReturnCode(result);
        }

        /// <summary>
        /// Add Client 
        /// </summary>
        /// <summary xml:lang="es">
        /// Agregar Cliente
        /// </summary>       
        /// <returns></returns> 
        [ProducesResponseType(typeof(ClientBaseResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<ClientBaseResource>> AddClient(AddClientResource client)
        {
            OperationResult<ClientBaseResource> result = await _repository.AddClientAsync(client);
            return ReturnCode(result);
        }

        /// <summary>
        /// Get percentages balance associated a Client
        /// </summary> 
        /// <summary xml:lang="es">
        /// Obtiene los porcentajes del saldo asociado al cliente
        /// </summary>
        /// <param name="rut"></param>
        /// <returns> Percentages associated a Client </returns>
        [ProducesResponseType(typeof(PercentageBalanceSluggishResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{rut}/PercentageBalance")]
        public async Task<ActionResult> GetPercentagesBalanceSluggish([FromRoute]string rut)
        {
            OperationResult<PercentageBalanceSluggishResource> result;
            rut = rut.FillRUT();
            bool existClient = await _repository.AnyAsync<Person>(t => t.RUT == rut);
            if (!existClient)
            {
                var msg = $"Cliente: {rut} no existe";
                result = OperationResult<PercentageBalanceSluggishResource>.NotFoundResult(rut, msg);
            }
            else
            {
                result = await _intelicomRepository.GetPercentagesBalanceSluggishAsync(rut);
            }

            return ReturnCode(result);
        }

        /// <summary>
        /// Get address by client
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener las direcciones por cliente
        /// </summary>
        /// <param name="rut"></param>
        /// <returns>Detail of address associated a Client </returns>
        [ProducesResponseType(typeof(IEnumerable<AddressResource>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{rut}/Address")]
        public async Task<ActionResult> GetAddressDetailByClient([FromRoute]string rut)
        {
            OperationCollectionResult<AddressResource> result;
            rut = rut.FillRUT();
            bool existClient = await _repository.AnyAsync<Person>(t => t.RUT == rut);
            if (!existClient)
            {
                var msg = $"Cliente: {rut} no existe";
                result = OperationCollectionResult<AddressResource>.NotFoundResult(rut, msg);
            }
            else
            {
                result = await _repository.GetAddressDetailByClient(rut);
            }
            return ReturnCode(result);
        }

        /// <summary>
        /// Add Address Client
        /// </summary>
        /// <summary xml:lang="es">
        /// Agregar dirección del cliente
        /// </summary>       
        /// <returns>Address Client</returns>
        [ProducesResponseType(typeof(AddressResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [Route("/AddAddress")]
        public async Task<ActionResult<AddressResource>> AddAddressClient(AddAddressCommand command)
        {
            if(!Enum.IsDefined(typeof(Country), command.Country))
            {
                return BadRequest($"País no válido {command.Country}");
            }
            OperationResult<AddressResource> result = await Mediator.Send(command);
            return ReturnCode(result);
        }

        /// <summary>
        /// Update address with new parameters
        /// </summary>
        /// <summary xml:lang="es">
        /// Actualizar la dirección con los nuevos parámetros
        /// </summary>
        /// <param name="command"></param>
        /// <returns> if created return 204 or if not found return 404 </returns>
        [ProducesResponseType(typeof(AddressResource), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPatch]
        [Route("/UpdateAddress")]
        public async Task<ActionResult<AddressResource>> UpdateAddressClient(UpdateAddressCommand command)
        {
            OperationBaseResult result = await Mediator.Send(command);
            return ReturnCode(result);
        }

        /// <summary>
        /// Get bank account by client
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener cuenta bancaria por cliente
        /// </summary>
        /// <param name="rut">RUT del cliente</param>
        /// <returns>Detail of the bank account associated with a Client</returns>
        [ProducesResponseType(typeof(BankAccountClientResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{rut}/BankAccountClient")]
        public async Task<ActionResult> GetBankAccountDetailsByClient([FromRoute]string rut)
        {
            OperationCollectionResult<BankAccountClientResource> result;
            rut = rut.FillRUT();
            bool existClient = await _repository.AnyAsync<Person>(t => t.RUT == rut);
            if (!existClient)
            {
                var msg = $"Cliente: {rut} no existe";
                result = OperationCollectionResult<BankAccountClientResource>.NotFoundResult(rut, msg);
            }
            else
            {
                result = await _repository.GetBankAccountDetailsByClient(rut);
            }
            return ReturnCode(result);
        }

        /// <summary>
        /// Get main contact by client
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener contacto principal por cliente
        /// </summary>
        /// <param name="rut">RUT del cliente</param>
        /// <returns>main contact data</returns>
        [ProducesResponseType(typeof(MainContactResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{rut}/MainContact")]
        public async Task<ActionResult> GetMainContactByClient([FromRoute] string rut)
        {
            OperationResult<MainContactResource> result = await _repository.GetMainContactByClient(rut);

            return ReturnCode(result);
        }

        /// <summary>
        /// Get the ID of the credit line granted by the client
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener el Id de la linea de credito otorgada por cliente
        /// </summary>
        /// <param name="rut">RUT del cliente</param>
        /// <returns>main contact data</returns>
        [ProducesResponseType(typeof(ClientCreditLine), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{rut}/CreditLine")]
        public async Task<ActionResult> GetCreditLineByClient([FromRoute] string rut)
        {
            OperationResult<ClientCreditLine> result = await _repository.GetClientCreditLine(rut);

            return ReturnCode(result);
        }
    }
}