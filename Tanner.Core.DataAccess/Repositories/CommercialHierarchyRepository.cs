using Dapper;
using Microsoft.ApplicationInsights;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Repositories.Interfaces;

namespace Tanner.Core.DataAccess.Repositories
{
    /// <inheritdoc cref="ICommercialHierarchyRepository"/>
    public class CommercialHierarchyRepository : CoreRepository, ICommercialHierarchyRepository
    {
        private readonly TelemetryClient telemetry;
        private string guidId;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx">DB context</param>
        public CommercialHierarchyRepository(CoreContext ctx, TelemetryClient _telemetry) : base(ctx)
        {
            telemetry = _telemetry;
        }

        /// <inheritdoc cref="ICommercialHierarchyRepository.GetCommercialHierarchyByEmailAsync(string)"/>
        public async Task<BusinessHierarchyResource> GetCommercialHierarchyByEmailAsync(string email)
        {
            (string query, object param) = BusinessHierarchyResource.Query_CommercialHierarchy(email);
            telemetry.TrackTrace($"Email solicitado para consulta: [{email}]");
            BusinessHierarchyResource execute = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    BusinessHierarchyResource result = await connection.QueryFirstOrDefaultAsync<BusinessHierarchyResource>(q,p);
                    return result;
                },
                query,
                param);
            return execute;
        }
    }
}
