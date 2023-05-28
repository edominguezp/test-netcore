using Dapper;
using Microsoft.ApplicationInsights;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;
using Tanner.RelationalDataAccess;

namespace Tanner.Core.DataAccess.Repositories
{
    public class FileRepository : TannerRepository<CoreFileContext>,  IFileRepository
    {
        private readonly TelemetryClient telemetry;
        private string guidId;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx">DB context</param>
        public FileRepository(CoreFileContext ctx, TelemetryClient _telemetry) : base(ctx)
        {
            telemetry = _telemetry;
        }

        public async Task<OperationResult<FileResource>> GetFileByID(decimal number, int id)
        {
            (string query, object param) = FileResource.Query_GetFileByID(number,id);
            telemetry.TrackTrace($"Obteniendo File para el número: [{number}] y el id: [{id}]");
            FileResource data = await ExecuteAsync(
                    async (SqlConnection connection, string q, object p) =>
                    {
                        FileResource execute = await connection.QueryFirstOrDefaultAsync<FileResource>(q, p);
                        return execute;
                    },
                    query,
                    param);
                if (data == null)
                {
                telemetry.TrackTrace($"No se encuentra un archivo con el id: [{id}]");
                return OperationResult<FileResource>.NotFoundResult(id);
                }

                var result = new OperationResult<FileResource>(data);
                telemetry.TrackTrace($"Se obtiene le archivo: [{result}]");
            return result;
        }


    }
}
