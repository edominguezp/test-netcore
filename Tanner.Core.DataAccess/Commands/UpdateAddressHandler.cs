using Microsoft.ApplicationInsights;
using System.Linq;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Models;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;
using Tanner.RelationalDataAccess;

namespace Tanner.Core.DataAccess.Commands
{
    public class UpdateAddressHandler : TannerCommandHandler<OperationBaseResult, UpdateAddressCommand>
    {
        private readonly TelemetryClient telemetry;
        private string guidId;

        public UpdateAddressHandler(ICoreRepository repository, TelemetryClient _telemetry) : base(repository)
        {
            telemetry = _telemetry;
        }

        public override async Task<OperationBaseResult> ActionAsync(UpdateAddressCommand command)
        {
            if (command.Phone != null && ((command.Phone.Length < 12 && command.Phone.Contains("+")) || (command.Phone.Length == 12 && !command.Phone.Contains("+")) || !long.TryParse(command.Phone, out long result)))
            {
                telemetry.TrackTrace($"El teléfono no contiene el formato correcto: [{command.Phone}]");
                return OperationBaseResult.BadRequestResult($"El teléfono no contiene el formato correcto: { command.Phone}");
            }

            if (command.Phone != null && !(command.Phone.Length == 9 || command.Phone.Length == 11 || command.Phone.Length == 12))
            {
                telemetry.TrackTrace($"El teléfono no contiene el tamaño correcto: [{command.Phone}]");
                return OperationBaseResult.BadRequestResult($"El teléfono no contiene el tamaño correcto: { command.Phone}");
            }

            Address address = await Repository.FirstOrDefaultAsync<Address>(t => t.ID == command.ID);

            if (address == null)
            {
                telemetry.TrackTrace($"No se encontró una dirección con el id: [{command.ID}]");
                return OperationBaseResult.NotFoundResult(command.ID, $"No se encontró una dirección con el id : {command.ID}");
            }

            bool existComune = Repository.Where<Commune>(t => t.ID == command.CommuneID).Any();

            if (!existComune)
            {
                telemetry.TrackTrace($"No existe la comuna con ID: [{command.ID}]");
                return OperationResult<AddressResource>.NotFoundResult($"No existe la comuna con ID: {command.CommuneID}");
            }

            address.CommuneID = command.CommuneID;
            address.AddressClient = command.Address;
            address.Number = command.Number;
            address.Phone = command.Phone;


            await Repository.SaveChangesAsync();
            telemetry.TrackTrace($"Se ha actualizado el registro en la base de datos [{guidId}]");

            return new OperationBaseResult();

        }
    }
}