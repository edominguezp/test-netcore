using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.API.Controllers
{
    /// <summary>
    /// Client controller
    /// </summary>
    public class DebtorController : BaseController<DebtorController>
    {
        private readonly IDebtorRepository _repository;
        private readonly IIntelicomRepository _intelicomRepository;
        private readonly IDocumentsDebtorRepository _documentsDebtor;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="mediator"></param>
        /// <param name="repository"></param>
        /// <param name="intelicomRepository"></param>
        /// <param name="documentsDebtor"></param>
        public DebtorController(ILogger<DebtorController> logger, IMediator mediator, IDebtorRepository repository, IIntelicomRepository intelicomRepository, IDocumentsDebtorRepository documentsDebtor) : base(logger, mediator)
        {
            _repository = repository;
            _intelicomRepository = intelicomRepository;
            _documentsDebtor = documentsDebtor;
        }
    
        /// <summary>
        /// Add Debtor
        /// </summary>
        /// <summary xml:lang="es">
        /// Agregar Deudor-Tercero
        /// </summary>       
        /// <returns></returns> 
        [ProducesResponseType(typeof(DebtorBaseResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<DebtorBaseResource>> AddDebtor(DebtorBaseResource debtor)
        {
            OperationResult<DebtorBaseResource> result = await _repository.AddDebtorAsync(debtor);
            return ReturnCode(result);
        }

        /// <summary>
        /// Update state of electronic receiver
        /// </summary>
        /// <summary xml:lang="es">
        /// Actualiza el estado del receptor electrónico
        /// </summary>
        /// <param name="request"></param>
        /// <returns>  </returns>
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpPatch]
        [Route("{rut}/UpdateElectronicReceiver")]
        public async Task<ActionResult> UpdateElectronicReceiver(UpdateElectronicReceiver request)
        {
            OperationBaseResult result = await _repository.UpdateElectronicReceiverAsync(request);
            return ReturnCode(result);
        }

        /// <summary>
        /// Get data by debtor
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener los datos del deudor
        /// </summary>
        /// <param name="rut"></param>
        /// <returns>Debtor data </returns>
        [ProducesResponseType(typeof(DebtorDataResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet]
        [Route("{rut}/DebtorDetail")]
        public async Task<ActionResult> GetData([FromRoute]string rut)
        {
            OperationResult<DebtorDataResource> result = await _repository.GetDebtorDetailAsync(rut);
            return ReturnCode(result);
        }

        /// <summary>
        /// Get data documents for debtors
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener los datos de los documentos de los deudores
        /// </summary>
        /// <returns>Collection of debtors</returns>
        [ProducesResponseType(typeof(CollectionResult<DocumentDebtorResource>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        [Route("/ProcessDocuments")]

        public async Task<ActionResult> GetDocuments()
        {

            OperationCollectionResult<DocumentDebtorResource> result = await _documentsDebtor.GetDebtors();
            return ReturnCode(result);
        }
    }
}
        