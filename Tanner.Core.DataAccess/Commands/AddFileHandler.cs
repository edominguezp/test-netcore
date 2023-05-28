using Microsoft.ApplicationInsights;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Models;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;
using Tanner.RelationalDataAccess;

namespace Tanner.Core.DataAccess.Commands
{
    public class AddFileHandler : TannerCommandHandler<OperationResult<FileResource>, AddFileCommand>
    {
        private readonly IClientRepository _clientRepository;
        private readonly TelemetryClient telemetry;

        public AddFileHandler(IFileRepository repository, IClientRepository clientRepository, TelemetryClient _telemetry) : base(repository)
        {
            telemetry = _telemetry;
            _clientRepository = clientRepository;
        }

        public override async Task<OperationResult<FileResource>> ActionAsync(AddFileCommand command)
        {
            OperationResult<FileResource> output = new OperationResult<FileResource> { Data = null };


            bool lineExist = await _clientRepository.GetCreditLineById(command.LineId);

            if (!lineExist)
            {
                return OperationResult<FileResource>.BadRequestResult($"La linea {command.LineId} no existe.");
            }

            File file = new File
            {
                Name = command.FileName,
                URL = command.URL,
                Directory = command.Directory
            };

            Repository.Add(file);
            await Repository.SaveChangesAsync();

            FileLine fileLine = new FileLine
            {
                ID = file.ID,
                LineID = command.LineId,
                FileType = command.FileType
            };

            Repository.Add(fileLine);

            await Repository.SaveChangesAsync();
            telemetry.TrackTrace($"Se ha agregado el registro a la base de datos");

            return output;
        }
    }
}
