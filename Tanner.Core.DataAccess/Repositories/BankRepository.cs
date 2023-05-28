using Dapper;
using Microsoft.ApplicationInsights;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.DataAccess.Repositories
{
    public class BankRepository : CoreRepository, IBankRepository
    {
        private readonly TelemetryClient telemetry;
        private string guidId;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx">DB context</param>
        public BankRepository(CoreContext ctx, TelemetryClient _telemetry) : base(ctx)
        {
            telemetry = _telemetry;

        }

        /// <inheritdoc cref="IBankRepository.GetBanksAsync()"/>
        public async Task<OperationCollectionResult<BankResource>> GetBanksAsync()
        {
            telemetry.TrackTrace("Obteniendo banco");

            string query = BankResource.Query_GetBank();
            IEnumerable<BankResource> dataDocuments = await ExecuteAsync(
                        async (SqlConnection connection, string q, object p) =>
                        {
                            IEnumerable<BankResource> banks = await connection.QueryAsync<BankResource>(q);
                            return banks;
                        },
                        query);

            var result = new OperationCollectionResult<BankResource>
            {
                DataCollection = dataDocuments,
                Total = dataDocuments.Count()
            };

            guidId = Guid.NewGuid().ToString();
            string payload = JsonConvert.SerializeObject(result);
            telemetry.TrackTrace($"Payload Banks [{result.DataCollection}]", new Dictionary<string, string> { { "guidId", guidId }, { "payload", payload } });
        
            return result;
        }
    }
}
