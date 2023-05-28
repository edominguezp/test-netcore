using FluentValidation;
using Tanner.Core.DataAccess.Commands;
using Tanner.Utils.Extensions;

namespace Tanner.Core.API.Validations
{
    /// <summary>
    /// Validate add client checking account
    /// </summary>
    /// <summary lang="es">
    /// Validar los datos de inserción de una cuenta corriente a un cliente
    /// </summary>
    public class AddClientCheckingAccountCommandValidator : AbstractValidator<AddClientCheckingAccountCommand>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AddClientCheckingAccountCommandValidator()
        {
            RuleFor(x => x.ClientRut)
                .Must(rut => rut.IsValidRUT())
                .WithMessage($"La propiedad no es un RUT válido");
        }
    }
}
