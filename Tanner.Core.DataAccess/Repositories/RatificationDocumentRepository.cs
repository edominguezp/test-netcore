using Dapper;
using Microsoft.ApplicationInsights;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.DataAccess.Repositories
{
    /// <inheritdoc cref="IRatificationDocumentRepository"/>
    public class RatificationDocumentRepository : CoreRepository, IRatificationDocumentRepository
    {
        private readonly TelemetryClient telemetry;
        private string guidId;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx">DB context</param>
        public RatificationDocumentRepository(CoreContext ctx, TelemetryClient _telemetry) : base(ctx)
        {
            telemetry = _telemetry;
        }

        /// <inheritdoc cref="IRatificationDocumentRepository.UpdateRatificationStatus(UpdateRatificationStatus)"/>
        public async Task<OperationBaseResult> UpdateRatificationStatus(UpdateRatificationStatus request)
        {
            var parameters = new
            {
                id_documento = request.DocumentID,
                estado_confirmacion = request.ConfirmationStatus,
                observacion = request.Observation,
                av_login = request.Login,
                esta_cedido = request.IsGranted

            };

            var execute = new OperationBaseResult();

            guidId = Guid.NewGuid().ToString();
            string payload = JsonConvert.SerializeObject(request);
            telemetry.TrackTrace($"Payload Ratification [{request.DocumentID}]", new Dictionary<string, string> { { "guidId", guidId }, { "payload", payload } });

            string query = DocumentStatusResource.Query_UpdateDocumentStatus(request.DocumentID);

            int confirmationStatus = await GetElementAsync<int>(query, null);

            if (confirmationStatus != 2 || (confirmationStatus == 2 && request.ConfirmationStatus != 5))
            {
                execute = await ExecuteAsync(
                       async (SqlConnection connection, string q, object p) =>
                       {
                           await connection.QueryAsync("dba.pr_fin0197", p, commandType: CommandType.StoredProcedure);
                           var result = new OperationBaseResult();
                           telemetry.TrackTrace($"El resultado del sp es: [{result}]");
                           return result;

                       },
                       null,
                       parameters);
            }

            return execute;
        }
    }
}