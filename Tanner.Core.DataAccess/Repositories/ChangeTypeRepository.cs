using Dapper;
using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.Enums;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.DataAccess.Repositories
{
    public class ChangeTypeRepository : CoreRepository, IChangeTypeRepository
    {
        public ChangeTypeRepository(CoreContext ctx) : base(ctx)
        {
         
        }

        /// <inheritdoc cref="IChangeTypeRepository.GetChangeTypeAndDefaultRateAsync(int, DateTime)"/>
        public async Task<OperationResult<ChangeTypeAndDefaultRateResource>> GetChangeTypeAndDefaultRateAsync(ChangeTypeEnum changeType, DateTime dateToConsult)
        {
            (string query, object param) = ChangeTypeAndDefaultRateResource.Query_GetChangeTypeAndDefaultRateByCurrentTypeAndDate(changeType, dateToConsult);

            ChangeTypeAndDefaultRateResource data = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    ChangeTypeAndDefaultRateResource resultMultiple = await connection.QueryFirstOrDefaultAsync<ChangeTypeAndDefaultRateResource>(q, p);
                    return resultMultiple;
                }, query, param);

            var result = new OperationResult<ChangeTypeAndDefaultRateResource>(data);

            return result;
        }
    }
}
