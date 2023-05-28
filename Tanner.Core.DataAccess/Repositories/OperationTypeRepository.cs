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
    /// <summary>
    /// Repository to operation types
    /// </summary>
    /// <summary xml:lang="es">
    /// Repositorio para los tipos de producto
    /// </summary>
    public class OperationTypeRepository : CoreRepository, IOperationTypeRepository
    {
        public OperationTypeRepository(CoreContext ctx) : base(ctx)
        {
        }

        /// <inheritdoc/>
        public async Task<OperationCollectionResult<OperationTypeResource>> GetOperationTypeAsync()
        {
            string query = OperationTypeResource.Query_GetOperationType();
            IEnumerable<OperationTypeResource> data = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    IEnumerable<OperationTypeResource> resultMultiple = await connection.QueryAsync<OperationTypeResource>(q, p);
                    return resultMultiple;
                },
            query,
            null);

            var result = new OperationCollectionResult<OperationTypeResource>
            {
                DataCollection = data,
                Total = data.Count()
            };
            return result;
        }
    }
}
