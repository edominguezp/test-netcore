using FluentValidation;
using Tanner.Core.DataAccess.ModelResources;

namespace Tanner.Core.API.Validations
{
    /// <summary>
    /// Validate request to search operation by executive or agent
    /// </summary>
    /// <summary lang="es">
    /// Validar los datos de la solicitud de búsqueda de las operaciones por ejecutivo o agente
    /// </summary>
    public class OperationByExecutiveOrAgentValidator : AbstractValidator<OperationByExecutiveOrAgent>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public OperationByExecutiveOrAgentValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage($"La propiedad no es un correo electrónico válido");
        }
    }
}
