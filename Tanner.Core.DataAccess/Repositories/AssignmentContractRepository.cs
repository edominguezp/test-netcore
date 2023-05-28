using Microsoft.ApplicationInsights;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Models;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;
using static Dapper.SqlMapper;

namespace Tanner.Core.DataAccess.Repositories
{
    public class AssignmentContractRepository : CoreRepository, IAssignmentContractRepository
    {
        private readonly TelemetryClient telemetry;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx">DB context</param>
        public AssignmentContractRepository(CoreContext ctx, TelemetryClient _telemetry) : base(ctx)
        {
            telemetry = _telemetry;
        }

        public async Task<OperationResult<AssignmentContractResource>> GetAssignmentContractAsync(long operationNumber)
        {
            OperationResult<AssignmentContractResource> output = new OperationResult<AssignmentContractResource>();

            (string query, object param) = AssignmentContractResource.Query_AssignmentContract(operationNumber);
            telemetry.TrackTrace($"Obteniendo los datos del contrato de cesión {operationNumber}");

            output.Data = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    AssignmentContractResource result = new AssignmentContractResource();

                    GridReader resultMultiple = await connection.QueryMultipleAsync(q, p);
                    result = resultMultiple.ReadFirstOrDefault<AssignmentContractResource>();

                    if (result != null)
                        result.Documents = resultMultiple.Read<AssignmentContractDocuments>();
                    else
                        result = new AssignmentContractResource();

                    return result;
                }, query, param);

            return output;
        }
    }
}
