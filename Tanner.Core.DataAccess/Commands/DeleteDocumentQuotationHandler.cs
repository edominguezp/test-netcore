using Microsoft.ApplicationInsights;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.Models;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;
using Tanner.RelationalDataAccess;

namespace Tanner.Core.DataAccess.Commands
{
    public class DeleteDocumentQuotationHandler : TannerCommandHandler<OperationBaseResult, DeleteDocumentQuotationCommand>
    {
        private readonly TelemetryClient telemetry;
        private string guidId;

        public DeleteDocumentQuotationHandler(ICoreRepository repository, TelemetryClient _telemetry) : base(repository)
        {
            telemetry = _telemetry;
        }

        public override async Task<OperationBaseResult> ActionAsync(DeleteDocumentQuotationCommand command)
        {
            guidId = Guid.NewGuid().ToString();
            string payload = JsonConvert.SerializeObject(command);
            telemetry.TrackTrace($"Payload Quotation [{command}]", new Dictionary<string, string> { { "guidId", guidId }, { "payload", payload } });

            Operation operation = await Repository.FirstOrDefaultAsync<Operation>(t => t.QuotationNumber == command.QuotationNumber);
            if (operation == null)
            {
                DocumentQuotation document = await Repository.FirstOrDefaultAsync<DocumentQuotation>(t => t.DocumentNumber == command.DocumentNumber && t.OperationNumber == command.QuotationNumber);

                if (document == null)
                {
                    var msg = $"Documento: {command.DocumentNumber} No existe en cotización: {command.QuotationNumber}";
                    telemetry.TrackTrace($"No existe cotización [{command.QuotationNumber}]");

                    return OperationBaseResult.NotFoundResult(command, msg);
                }
                else if (document.DocumentStatus != 0) // Validar que el estado no sea en análisis estado <> 0
                {
                    var msg = $"Documento: {command.DocumentNumber} no está en análisis, no se puede eliminar";
                    telemetry.TrackTrace($"Documento no está en análisis, no se puede eliminar [{command.DocumentNumber}]");

                    return OperationBaseResult.NotFoundResult(command.QuotationNumber, msg);
                }
                else
                {
                    //cumple todas las condiciones y porfin lo podemos actualizar.
                    document.DocumentStatus = 9;
                    Repository.Update(document);
                    await Repository.SaveChangesAsync();
                    telemetry.TrackTrace($"Se ha eliminado el registro [{guidId}]");
                    return new OperationBaseResult();


                }
            }
            else
            {
                var msg = $"No se pueden eliminar el documento, cotización: {command.QuotationNumber} ya se curso en operación: {operation.ID}";
                telemetry.TrackTrace($"No se puede eliminar el registro, ya se cursó la operación [{command.QuotationNumber}]");

                return OperationBaseResult.BadRequestResult(msg);
            }
        }
    }
}
