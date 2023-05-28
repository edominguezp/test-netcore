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
    /// Repository to channels
    /// </summary>
    /// <summary xml:lang="es">
    /// Repositorio para los canales
    /// </summary>
    public class ProductTypeRepository : CoreRepository, IProductTypeRepository
    {
        public ProductTypeRepository(CoreContext ctx) : base(ctx)
        {
        }

        /// <inheritdoc/>
        public async Task<OperationCollectionResult<ProductTypeResource>> GetProductTypeAsync()
        {
            string query = ProductTypeResource.Query_GetProductTypeResource();
            IEnumerable<ProductTypeResource> data = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    IEnumerable<ProductTypeResource> resultMultiple = await connection.QueryAsync<ProductTypeResource>(q, p);
                    return resultMultiple;
                },
            query,
            null);

            var result = new OperationCollectionResult<ProductTypeResource>
            {
                DataCollection = data,
                Total = data.Count()
            };
            return result;
        }
    }
}
