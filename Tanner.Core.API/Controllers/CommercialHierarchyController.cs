using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Repositories.Interfaces;

namespace Tanner.Core.API.Controllers
{
    /// <summary>
    /// Commercial Hierarchy controller
    /// </summary>
    public class CommercialHierarchyController : BaseController<CommercialHierarchyController>
    {
        private readonly ICommercialHierarchyRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="mediator"></param>
        /// <param name="repository"></param>
        public CommercialHierarchyController(ILogger<CommercialHierarchyController> logger, IMediator mediator, ICommercialHierarchyRepository repository) : base(logger, mediator)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get commercial Hierarchy of executive
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener la jerarquía comercial de acuerdo a parámetros de entrada
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Commercial Hierarchy associated a executive </returns>
        [ProducesResponseType(typeof(BusinessHierarchyResource), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery]string email)
        {
            BusinessHierarchyResource commercialHierarchy = await _repository.GetCommercialHierarchyByEmailAsync(email);
            if (commercialHierarchy == null)
            {
                return NotFound(email);
            }

            return Ok(commercialHierarchy);
        }
    }  

}