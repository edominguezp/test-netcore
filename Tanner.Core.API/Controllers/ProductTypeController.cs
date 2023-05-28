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
    /// Product Type controller
    /// </summary>
    public class ProductTypeController : BaseController<ProductTypeController>
    {        
        private readonly IProductTypeRepository _productTypeRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="mediator"></param>
        /// <param name="productTypeRepository"></param>
        public ProductTypeController(ILogger<ProductTypeController> logger, IMediator mediator, IProductTypeRepository productTypeRepository) : base(logger, mediator)
        {
            _productTypeRepository = productTypeRepository;
        }

        /// <summary>
        /// Get all product types
        /// </summary>
        /// <summary xml:lang="es">
        /// Obtener todos los tipos de productos
        /// </summary>
        /// <returns>All data of product types</returns>
        [ProducesResponseType(typeof(CollectionResult<ProductTypeResource>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<CollectionResult<ProductTypeResource>>> Get()
        {
            OperationCollectionResult<ProductTypeResource> result = await _productTypeRepository.GetProductTypeAsync();
            return ReturnCode(result);
        }
    }
}
