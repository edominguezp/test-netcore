using Dapper;
using Microsoft.ApplicationInsights;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.DataAccess.Repositories
{
    /// <inheritdoc cref="IEmployeeRepository"/>
    public class EmployeeRepository : CoreRepository, IEmployeeRepository
    {
        private readonly TelemetryClient telemetry;
        private string guidId;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx">DB context</param>
        public EmployeeRepository(CoreContext ctx, TelemetryClient _telemetry) : base(ctx)
        {
            telemetry = _telemetry;
        }

        /// <inheritdoc cref="IEmployeeRepository.ExistEmployeeAsync(string)"/>
        public async Task<bool> ExistEmployeeAsync(string email)
        {
            //TODO
            return await Task.FromResult(true);
        }

        /// <inheritdoc cref="IEmployeeRepository.GetDataEmployeeByEmailAsync(string)"/>
        public async Task<OperationResult<EmployeeResource>> GetDataEmployeeByEmailAsync(string email)
        {
            (string query, object param) = EmployeeResource.Query_EmployeeData(email);
            telemetry.TrackTrace($"Obteniendo data según el correo: [{email}]");
            EmployeeResource data = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    EmployeeResource execute = await connection.QueryFirstOrDefaultAsync<EmployeeResource>(q, p);
                    return execute;
                },
                query,
                param);
            if (data == null)
            {
                telemetry.TrackTrace($"No existen resultados para el correo: [{email}]");
                return OperationResult<EmployeeResource>.NotFoundResult(email);
            }

            var result = new OperationResult<EmployeeResource>(data);
            telemetry.TrackTrace($"Resultados obtenidos, data empleados: [{result}]");
            return result;
        }
    }
}
