using System.Threading.Tasks;
using Tanner.Core.API.Model;

namespace Tanner.Core.API.Interfaces
{
    /// <summary>
    /// API Manager
    /// </summary>
    public interface IAPIManagerClient
    {
        /// <summary>
        /// Envía un correo electrónico
        /// </summary>
        /// <param name="emailRequest">Datos para el envio del correo electrónico</param>
        /// <returns>Retorna verdadero si se envia el correo electrónico</returns>
        Task<bool> SendEmailAsync(EmailRequest emailRequest);
    }
}
