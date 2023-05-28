using Microsoft.ApplicationInsights;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.Enums;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Models;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;
using Tanner.RelationalDataAccess;

namespace Tanner.Core.DataAccess.Commands
{
    public class AddAddressHandler : TannerCommandHandler<OperationResult<AddressResource>, AddAddressCommand>
    {
        private readonly TelemetryClient telemetry;
        private string guidId;


        public AddAddressHandler(ICoreRepository repository, TelemetryClient _telemetry) : base(repository)
        {
            telemetry = _telemetry;
        }

        public override async Task<OperationResult<AddressResource>> ActionAsync(AddAddressCommand command)
        {
            string rut = command.RUT.Replace("-", "");
            char zero = '0';
            rut = rut.PadLeft(10, zero);

            Person person = await Repository.FirstOrDefaultAsync<Person>(t => t.RUT.Equals(rut));
            if (person == null)
            {
                telemetry.TrackTrace($"No existe una persona con el RUT [{command.RUT}]");
                return OperationResult<AddressResource>.NotFoundResult(command.RUT , $"No existe una persona con el RUT:{command.RUT}");
            }
            if (command.Phone != null && ((command.Phone.Length < 12 && command.Phone.Contains("+")) || (command.Phone.Length == 12 && !command.Phone.Contains("+")) || !long.TryParse(command.Phone, out long result)))
            {
                telemetry.TrackTrace($"El teléfono no contiene el formato correcto: [{command.Phone}]");
                return OperationResult<AddressResource>.BadRequestResult($"El teléfono no contiene el formato correcto: { command.Phone}");
            }

            if (command.Phone != null && !(command.Phone.Length == 9 || command.Phone.Length == 11 || command.Phone.Length == 12))
            {
                telemetry.TrackTrace($"El teléfono no contiene el tamaño correcto: [{command.Phone}]");
                return OperationResult<AddressResource>.BadRequestResult($"El teléfono no contiene el tamaño correcto: { command.Phone}");
            }

            bool existComune = Repository.Where<Commune>(t => t.ID == command.CommuneID).Any();

            if(!existComune)
            {
                telemetry.TrackTrace($"No existe la comuna: [{command.CommuneID}]");
                return OperationResult<AddressResource>.NotFoundResult($"No existe la comuna: { command.CommuneID}");
            }

            var address = new Address
            {
                PersonID = person.ID,
                CommuneID = command.CommuneID,
                AddressClient = command.Address,
                Number = command.Number,
                Phone = command.Phone,
                CountryCode = (int)command.Country

            };

            guidId = Guid.NewGuid().ToString();
            string payload = JsonConvert.SerializeObject(command);
            telemetry.TrackTrace($"Payload Address [{command.Address}]", new Dictionary<string, string> { { "guidId", guidId }, { "payload", payload } });

            //inserto registro en la tabla
            Repository.Add(address);
            await Repository.SaveChangesAsync();
            telemetry.TrackTrace($"Se ha agregado el registro a la base de datos [{guidId}]");

            var data = (AddressResource)address;
            telemetry.TrackTrace($"Se agregó el siguiente registro [{data}]");
            return new OperationResult<AddressResource>(data);
        }
    }
}