using Microsoft.ApplicationInsights;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Models;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;
using Tanner.RelationalDataAccess;

namespace Tanner.Core.DataAccess.Commands
{

    public class InsertFileOperationHandler : TannerCommandHandler<OperationResult<FileResource>, InsertFileOperationCommand>
    {
        private readonly TelemetryClient telemetry;
        private string guidId;

        public InsertFileOperationHandler(IFileRepository repository, TelemetryClient _telemetry) : base(repository)
        {
            telemetry = _telemetry;
        }

        public override async Task<OperationResult<FileResource>> ActionAsync(InsertFileOperationCommand command)
        {
            var fileInsert = new File
            {
                Directory = command.Directory,
                Name = command.Name,
                URL = command.URL
            };

            guidId = Guid.NewGuid().ToString();
            string payload = JsonConvert.SerializeObject(command);
            telemetry.TrackTrace($"Payload FileInsert [{command}]", new Dictionary<string, string> { { "guidId", guidId }, { "payload", payload } });

            Repository.Add(fileInsert);
            await Repository.SaveChangesAsync();
            telemetry.TrackTrace($"Se ha agregado el archivo a la base de datos [{guidId}]");

            var operationFileInsert = new OperationFile
            {
                FileID = fileInsert.ID,
                OperationID = command.OperationNumber
            };
            guidId = Guid.NewGuid().ToString();
            string payload2 = JsonConvert.SerializeObject(command);
            telemetry.TrackTrace($"Payload File [{command.OperationNumber}]", new Dictionary<string, string> { { "guidId", guidId }, { "payload", payload2 } });

            Repository.Add(operationFileInsert);
            await Repository.SaveChangesAsync();
            telemetry.TrackTrace($"Se ha agregado el archivo a la base de datos [{guidId}]");


            var fileResource = new FileResource
            {
                OperationNumber = Convert.ToInt64(operationFileInsert.OperationID),
                ID = fileInsert.ID,
                URL = fileInsert.URL,
                Directory = fileInsert.Directory,
                Name = fileInsert.Name
            };
                
            var result = new OperationResult<FileResource>(fileResource);
            return result;
        }
    }
}