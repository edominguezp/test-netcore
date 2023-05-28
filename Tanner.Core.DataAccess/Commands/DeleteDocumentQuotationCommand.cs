using System.Diagnostics.CodeAnalysis;
using Tanner.Core.DataAccess.Results;
using Tanner.RelationalDataAccess;

namespace Tanner.Core.DataAccess.Commands
{
    [ExcludeFromCodeCoverage]
    public class DeleteDocumentQuotationCommand : TannerCommand<OperationBaseResult>
    {

        /// <summary>
        /// Quotation Number
        /// </summary>
        /// <summary xml:lang="es">
        /// Numero de Cotización
        /// </summary>
        public int QuotationNumber { get; set; }

        /// <summary>
        /// Document Number
        /// </summary>
        /// <summary xml:lang="es">
        /// Numero de Documento
        /// </summary>
        public int DocumentNumber { get; set; }

    }
}
