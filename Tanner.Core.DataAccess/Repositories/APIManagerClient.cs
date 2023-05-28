using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tanner.Core.API.Interfaces;
using Tanner.Core.API.Model;
using Tanner.RestClient.Services;

namespace Tanner.Core.API.Infrastructure.Configurations
{
    /// <summary>
    ///
    /// </summary>
    public class APIManagerClient : TannerRestClient, IAPIManagerClient
    {
        private readonly ILogger<TannerRestClient> _logger;
        private readonly EmailConfiguration email;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="emailConfiguration"></param>
        /// <param name="telemetryClient"></param>
        /// <param name="logger"></param>
        public APIManagerClient(HttpClient httpClient, IOptions<EmailConfiguration> emailConfiguration, ILogger<TannerRestClient> logger) : base(httpClient)
        {
            _logger = logger;
            email = emailConfiguration.Value;
        }

        /// <summary>
        /// Envía un correo electrónico
        /// </summary>
        /// <param name="emailRequest">Datos del correo electrónico</param>
        /// <returns>Retorna verdadero si se envia el correo electrónico</returns>
        public async Task<bool> SendEmailAsync(EmailRequest emailRequest)
        {
            string json = JsonConvert.SerializeObject(emailRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, DefaultRequestMediaType);

            httpContent.Headers.Add("ContentType", DefaultRequestMediaType);
            httpContent.Headers.Add("Ocp-Apim-Subscription-Key", email.SubscriptionKey);

            HttpResponseMessage responseMessage = await HttpClient.PostAsync("tanner-email/email", httpContent);

            bool response = true;
            if (!responseMessage.IsSuccessStatusCode)
            {
                response = false;
                _logger.LogWarning("Ha ocurrido un error enviando correo.");
            }
            return response;
        }
    }
}