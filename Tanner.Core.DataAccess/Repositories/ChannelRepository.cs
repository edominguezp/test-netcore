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
    public class ChannelRepository : CoreRepository, IChannelRepository
    {
        public ChannelRepository(CoreContext ctx) : base(ctx)
        {
        }

        /// <inheritdoc/>
        public async Task<OperationCollectionResult<ChannelResource>> GetChannelAsync()
        {
            string query = ChannelResource.Query_GetChannel();
            IEnumerable<ChannelResource> data = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    IEnumerable<ChannelResource> resultMultiple = await connection.QueryAsync<ChannelResource>(q, p);
                    return resultMultiple;
                },
            query,
            null);

            var result = new OperationCollectionResult<ChannelResource>
            {
                DataCollection = data,
                Total = data.Count()
            };
            return result;
        }
    }
}
