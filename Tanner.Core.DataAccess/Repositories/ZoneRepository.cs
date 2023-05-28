using Dapper;
using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.Enums;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.DataAccess.Repositories
{
    public class ZoneRepository : CoreRepository, IZoneRepository
    {
        private readonly TelemetryClient telemetry;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx">DB context</param>
        public ZoneRepository(CoreContext ctx, TelemetryClient _telemetry) : base(ctx)
        {
            telemetry = _telemetry;
        }

        /// <inheritdoc cref="IOperationRepository.GetBranchOfficeByState(IEnumerable{OperationState}, int, int)"/>
        public async Task<OperationCollectionResult<BranchOfficeResource>> GetBranchOfficeByZoneAndState(IEnumerable<OperationState> statesOperation, int daysOperations, int zone)
        {
            (string query, object param) = BranchOfficeResource.Query_GetBranchOfficeByState(statesOperation, daysOperations, zone);
            telemetry.TrackTrace($"Buscando sucursales para la zona: [{zone}]");
            IEnumerable<BranchOfficeResource> dataDocuments = await ExecuteAsync(
                        async (SqlConnection connection, string q, object p) =>
                        {
                            IEnumerable<BranchOfficeResource> resultMultiple = await connection.QueryAsync<BranchOfficeResource>(q, p);
                            return resultMultiple;
                        },
                        query,
                        param);
            var result = new OperationCollectionResult<BranchOfficeResource>
            {
                DataCollection = dataDocuments,
                Total = dataDocuments.Count()
            };
            telemetry.TrackTrace($"Las sucursales encontradas son: [{dataDocuments}]");
            return result;
        }

        /// <inheritdoc/>
        public async Task<OperationCollectionResult<ZoneResource>> GetZonesAsync()
        {
            string query = ZoneResource.Query_GetZones();
            IEnumerable<ZoneResource> data = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    IEnumerable<ZoneResource> resultMultiple = await connection.QueryAsync<ZoneResource>(q, p);
                    return resultMultiple;
                },
            query,
            null);

            var result = new OperationCollectionResult<ZoneResource>
            {
                DataCollection = data,
                Total = data.Count()
            };
            return result;
        }

        /// <inheritdoc cref="IOperationRepository.GetZonesByState(ZoneByOperationStatus)"/>
        public async Task<OperationCollectionResult<ZoneResource>> GetZonesByState(ZoneByOperationStatus request)
        {
            (string query, object param) = ZoneResource.Query_GetZonesByState(request);
            telemetry.TrackTrace($"Obteniendo zonas con días de operaciones en: [{request.OperationDays}]");
            IEnumerable<ZoneResource> dataDocuments = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    IEnumerable<ZoneResource> resultMultiple = await connection.QueryAsync<ZoneResource>(q, p);
                    return resultMultiple;
                },
            query,
            param);
            var result = new OperationCollectionResult<ZoneResource>
            {
                DataCollection = dataDocuments,
                Total = dataDocuments.Count()
            };
            telemetry.TrackTrace($"Las zonas encontradas son: [{dataDocuments}]");

            return result;
        }
    }
}
