using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tanner.Core.API.Infrastructure.Configurations;

namespace Tanner.Core.API.Middleware
{
    /// <summary>
    /// Class that represent the swagger basic auth
    /// </summary>
    public class SwaggerBasicAuthMiddleware
    {
        private readonly RequestDelegate next;
        private readonly SwaggerBasicAuth _swaggerBasicAuth;
        /// <summary>
        /// Basic auth
        /// </summary>
        /// <param name="next"></param>
        /// <param name="swaggerBasicAuth">parameters of basic auth</param>
        public SwaggerBasicAuthMiddleware(RequestDelegate next, IOptions<SwaggerBasicAuth> swaggerBasicAuth)
        {
            this.next = next;
            _swaggerBasicAuth = swaggerBasicAuth.Value;
        }
        /// <summary>
        /// if have swagger request authorization
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger") && !this.IsLocalRequest(context))
            {
                string authHeader = context.Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Basic "))
                {
                    // Get the encoded username and password
                    var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
                    // Decode from Base64 to string
                    var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));
                    // Split username and password
                    var username = decodedUsernamePassword.Split(':', 2)[0];
                    var password = decodedUsernamePassword.Split(':', 2)[1];
                    // Check if login is correct
                    if (IsAuthorized(username, password))
                    {
                        await next.Invoke(context);
                        return;
                    }
                }
                // Return authentication type (causes browser to show login dialog)
                context.Response.Headers["WWW-Authenticate"] = "Basic";
                // Return unauthorized
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            /*
            //esto lo descomentas cuando implementen el token
            else if (context.Request.Path.StartsWithSegments("/api"))
            {
                var tokenValue = ConfigurationSetting.instance.GetValue("ApiToken");
                var authHeader = context.Request.Headers["Authorization"];
                if (context.Request.Path.Value.ToLower().Contains("/status") 
                    || context.Request.Path.Value.ToLower().Contains("/tasas"))
                    await next.Invoke(context);
                if (string.IsNullOrEmpty(authHeader) || !authHeader.Equals(tokenValue))
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                else
                    await next.Invoke(context);             
            }*/
            else
            {
                /*
                 *   //esto lo descomentas cuando implementen el token
                if (this.IsLocalRequest(context))
                    await next.Invoke(context);
                else
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    */
                await next.Invoke(context);
            }
        }
        /// <summary>
        /// Get bool if user is authorized
        /// </summary>
        /// <param name="username">Nombre de usuario</param>
        /// <param name="password">Contraseña</param>
        /// <returns>True if is authorized</returns>
        public bool IsAuthorized(string username, string password)
        {
            var user = _swaggerBasicAuth.User;
            var pwd = _swaggerBasicAuth.Password;
            return username.Equals(user, StringComparison.InvariantCultureIgnoreCase)
                    && password.Equals(pwd);
        }
        /// <summary>
        /// If is a local start does not request authorization
        /// </summary>
        /// <param name="context"></param>
        /// <returns>bool if is local return true</returns>
        public bool IsLocalRequest(HttpContext context)
        {
           
            //Handle running using the Microsoft.AspNetCore.TestHost and the site being run entirely locally in memory without an actual TCP/IP connection
            if (context.Connection.RemoteIpAddress == null && context.Connection.LocalIpAddress == null)
            {
                return true;
            }
            if (context.Connection.RemoteIpAddress.Equals(context.Connection.LocalIpAddress))
            {
                return true;
            }
            if (IPAddress.IsLoopback(context.Connection.RemoteIpAddress))
            {
                return true;
            }
            return false;
        }
    }
}
