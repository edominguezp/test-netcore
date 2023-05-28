using Dapper;
using Microsoft.ApplicationInsights;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Models;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;
using Tanner.RelationalDataAccess.Exceptions;

namespace Tanner.Core.DataAccess.Repositories
{
    public class LogRepository : CoreRepository, ILogRepository
    {
        private readonly TelemetryClient telemetry;
        private string guidId;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx">DB context</param>
        public LogRepository(CoreContext ctx, TelemetryClient _telemetry) : base(ctx)
        {
            telemetry = _telemetry;
        }

        public async Task<OperationResult<bool>> AddLogAsync(LogRequest logRequest)
        {
            if (logRequest.OperationNumber <= 0)
                return OperationResult<bool>.BadRequestResult("Operación no existe.");

            if (logRequest.ActionCode <= 0)
                return OperationResult<bool>.BadRequestResult("Código de acción no existe.");

            if (logRequest.ActionDescription.Length > 255)
                return OperationResult<bool>.BadRequestResult("En ancho máximo permitido para la propiedad ActionDescription es de 255 caracteres");

            OperationResult<bool> result = new OperationResult<bool>();
            try
            {
                (string query, object param) = LogResource.Query_AddLog(logRequest);
                result.Data = await ExecuteAsync(
                            async (SqlConnection connection, string q, object p) =>
                            {
                                int resultLogs = await connection.ExecuteAsync(q, p);
                                return (resultLogs > 0);
                            },
                            query, param);

                guidId = Guid.NewGuid().ToString();
                telemetry.TrackTrace($"Logs registrado", new Dictionary<string, string> { { "guidId", guidId }, { "payload", JsonConvert.SerializeObject(logRequest) } });

                return result;
            }
            catch(TannerDBException ex)
            {
                if (ex.InnerException != null)
                    return OperationResult<bool>.BadRequestResult(ex.InnerException.Message);

                throw;
            }
        }
    }
}
