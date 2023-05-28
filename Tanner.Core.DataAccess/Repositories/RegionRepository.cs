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
    public class RegionRepository : CoreRepository, IRegionRepository
    {
        private readonly TelemetryClient telemetry;
        private string guidId;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx">DB context</param>
        public RegionRepository(CoreContext ctx, TelemetryClient _telemetry) : base(ctx)
        {
            telemetry = _telemetry;
        }

        /// <inheritdoc cref="IRegionRepository.GetRegionsAsync()"/>
        public async Task<OperationCollectionResult<RegionResource>> GetRegionsAsync()
        {
            (string query, object param) = RegionResource.Query_GetRegion();
            IEnumerable<RegionResource> dataDocuments = await ExecuteAsync(
                        async (SqlConnection connection, string q, object p) =>
                        {
                            IEnumerable<RegionResource> resultMultiple = await connection.QueryAsync<RegionResource>(q, p);
                            return resultMultiple;
                        },
                        query,
                        param);

            var result = new OperationCollectionResult<RegionResource>
            {
                DataCollection = dataDocuments,
                Total = dataDocuments.Count()
            };
            telemetry.TrackTrace($"El resultado de las regiones es: [{dataDocuments}]");

            return result;
        }

        /// <inheritdoc cref="IRegionRepository.GetCommunesByRegion(int)"/>
        public async Task<OperationCollectionResult<CommuneResource>> GetCommunesByRegionAsync(int idRegion)
        {
            (string query, object param) = CommuneResource.Query_CommunesByRegion(idRegion);
            telemetry.TrackTrace($"Obteniendo las comunas según el id de la región: [{idRegion}]");
            IEnumerable<CommuneResource> dataDocuments = await ExecuteAsync(
                        async (SqlConnection connection, string q, object p) =>
                        {
                            IEnumerable<CommuneResource> resultMultiple = await connection.QueryAsync<CommuneResource>(q, p);
                            return resultMultiple;
                        },
                        query,
                        param);

            if (dataDocuments == null || !dataDocuments.Any())
            {
                telemetry.TrackTrace($"No existe la región con código: [{idRegion}]");
                return OperationCollectionResult<CommuneResource>.NotFoundResult($"No existe la región con código: {idRegion}");
            }

            var result = new OperationCollectionResult<CommuneResource>
            {
                DataCollection = dataDocuments,
                Total = dataDocuments.Count()
            };
            telemetry.TrackTrace($"El resultado de las comunas por región es: [{dataDocuments}]");
            return result;
        }
    }
}
