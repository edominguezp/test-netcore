using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tanner.Core.API.Infrastructure.ActionResults;
using Tanner.Core.DataAccess.Results;
using Tanner.RelationalDataAccess.Controllers;

namespace Tanner.Core.API.Controllers
{
    /// <summary>
    /// Base controller
    /// </summary>
    /// <summary xml:lang="es">
    /// Controlador base
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T> : DAControllerBase
    {
        /// <summary>
        /// Logger
        /// </summary>
        protected readonly ILogger<T> Logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="mediator"></param>
        public BaseController(ILogger<T> logger, IMediator mediator) : base(mediator)
        {
            Logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        [NonAction]
        protected virtual ActionResult ReturnCode(OperationBaseResult result)
        {
            switch (result.ErrorStatus)
            {
                case OperationErrorStatus.BadRequest:
                    return BadRequest(result.Message);
                case OperationErrorStatus.InternalError:
                    return InternalError(result.Message);
                case OperationErrorStatus.NotFound:
                    return NotFound(result.Resource);
                case OperationErrorStatus.Conflict:
                    return Conflict(result.Resource);
            }
            return NoContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResource"></typeparam>
        /// <param name="result"></param>
        /// <param name="controller"></param>
        /// <param name="actionName"></param>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        [NonAction]
        protected virtual ActionResult ReturnCode<TResource>(OperationResult<TResource> result, string controller = null, string actionName = null, object routeValues = null)
        {
            if (result.ErrorStatus != null)
            {
                return ReturnCode((OperationBaseResult)result);
            }
            string method = Request.Method;
            if (method == "POST" && !string.IsNullOrEmpty(controller) && !string.IsNullOrEmpty(actionName))
            {
                controller = controller.GetControllerName();
                return CreatedAtAction(actionName, controller, routeValues, result.Data);
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        [NonAction]
        protected virtual ActionResult ReturnCode<TResult>(OperationCollectionResult<TResult> result)
        {
            if (result.ErrorStatus != null)
            {
                return ReturnCode((OperationBaseResult)result);
            }
            return Ok(new CollectionResult<TResult>
            {
                DataCollection = result.DataCollection,
                Total = result.Total
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        [NonAction]
        protected virtual InternalServerErrorObjectResult InternalError(object error)
        {
            var result = new InternalServerErrorObjectResult(error);
            return result;
        }
    }
}