using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.DataAccess.Repositories
{
    public class BranchOfficeRepository : CoreRepository, IBranchOfficeRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx">DB context</param>
        public BranchOfficeRepository(CoreContext ctx) : base(ctx)
        {
        }
        
        /// <inheritdoc/>
        public async Task<OperationCollectionResult<BranchOfficeResource>> GetBranchOfficeAsync()
        {
            string query = BranchOfficeResource.Query_GetBranchOffice();
            IEnumerable<BranchOfficeResource> data = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    IEnumerable<BranchOfficeResource> resultMultiple = await connection.QueryAsync<BranchOfficeResource>(q, p);
                    return resultMultiple;
                },
            query,
            null);

            var result = new OperationCollectionResult<BranchOfficeResource>
            {
                DataCollection = data,
                Total = data.Count()
            };
            return result;
        }

        /// <inheritdoc cref="IBranchOfficeRepository.GetBranchOfficeByClientCodeAsync(int)"/>
        public async Task<OperationCollectionResult<BranchOfficeResource>> GetBranchOfficeByClientCodeAsync(int clientCode)
        {
            (string query, object param) = BranchOfficeResource.Query_GetBranchOfficeByClient(clientCode);
            IEnumerable<BranchOfficeResource> data = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    IEnumerable<BranchOfficeResource> resultMultiple = await connection.QueryAsync<BranchOfficeResource>(q, p);
                    return resultMultiple;
                }, query, param);

            var result = new OperationCollectionResult<BranchOfficeResource>
            {
                DataCollection = data,
                Total = data.Count()
            };
            return result;
        }
    }
}
