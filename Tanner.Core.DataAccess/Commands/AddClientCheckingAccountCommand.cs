using System.Diagnostics.CodeAnalysis;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Results;
using Tanner.RelationalDataAccess;

namespace Tanner.Core.DataAccess.Commands
{
    [ExcludeFromCodeCoverage]
    public class AddClientCheckingAccountCommand : TannerCommand<OperationResult<CurrentAccountResource>>
    {
        /// <summary>
        /// Bank Code
        /// </summary>
        /// <summary xml:lang="es">
        /// Codigo de Banco
        /// </summary>
        public int BankCode { get; set; }
        /// <summary>
        /// Bank Checking Account
        /// </summary>
        /// <summary xml:lang="es">
        /// Cuenta Corriente Bancaria
        /// </summary>
        public string BankCheckingAccount { get; set; }

        /// <summary>
        /// Client Rut
        /// </summary>
        /// <summary xml:lang="es">
        /// Rut Cliente
        /// </summary>
        public string ClientRut{ get; set; }

    }
}
