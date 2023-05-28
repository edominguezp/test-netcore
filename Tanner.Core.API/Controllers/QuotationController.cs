using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tanner.Core.DataAccess.Results;
using Tanner.Core.DataAccess.Commands;

namespace Tanner.Core.API.Controllers
{
    /// <summary>
    /// Quotation controller
    /// </summary>
    public class QuotationController : BaseController<QuotationController>
    {
        /// <summary>
        /// Constructor Quotes
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="mediator"></param>
        public QuotationController(ILogger<QuotationController> logger, IMediator mediator) : base(logger, mediator)
        {     
            
        }


        /// <summary>
        /// Delete documents quotes
        /// </summary>
        /// <summary xml:lang="es">
        /// Eliminar documentos de cotización
        /// </summary>       
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpDelete]
        public async Task<ActionResult> DeleteDocument(DeleteDocumentQuotationCommand command)
        {
            OperationBaseResult result = await Mediator.Send(command);
            return ReturnCode(result);
        }

    }
}