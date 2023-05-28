using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Threading.Tasks;
using Tanner.Core.API.Helpers;
using Tanner.Core.API.Model;

namespace Tanner.Core.API.Middleware
{
    /// <summary>
    /// Class that represent the execption of middleware
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// method that invoke the exeption
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }



        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            string errorId = TraceHelper.TraceException(ex);

            var errorResponse = new ErrorResponse
            {
                Id = errorId,
                Message = "Ha ocurrido un error interno"
            };

            var serializerSetting = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var result = JsonConvert.SerializeObject(errorResponse, serializerSetting);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(result);
        }
    }
}
