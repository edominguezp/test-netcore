using Dapper;
using Microsoft.ApplicationInsights;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;
using Tanner.RelationalDataAccess;

namespace Tanner.Core.DataAccess.Repositories
{
    public class IntelicomRepository : TannerRepository<IntelicomContext>, IIntelicomRepository
    {
        private readonly TelemetryClient telemetry;
        private string guidId;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx">DB context</param>
        public IntelicomRepository(IntelicomContext ctx, TelemetryClient _telemetry) : base(ctx)
        {
            telemetry = _telemetry;
        }
        /// <inheritdoc cref="IIntelicomRepository.GetDetailClientByRUTAsync(string)"/>
        public async Task<OperationResult<ClientDetailResource>> GetDetailClientByRUTAsync(string rut)
        {
            (string query, object param) = ClientDetailResource.Query_ClientDetailByRUT(rut);
            telemetry.TrackTrace($"Obteniendo datos para el rut [{rut}]");
            ClientDetailResource result = await ExecuteAsync(
                       async (SqlConnection connection, string q, object p) =>
                       {
                           ClientDetailResource resultSingle = await connection.QueryFirstOrDefaultAsync<ClientDetailResource>(q, p);
                           return resultSingle;
                       },
                       query,
                       param);
            var execute = new OperationResult<ClientDetailResource>
            {
                Data = result
            };
            telemetry.TrackTrace($"El resultado del detalle de cliente es: [{result}]");
            return execute;
        }

        /// <inheritdoc cref="IIntelicomRepository.GetDetailFactoringByRUTAsync(string)"/>
        public async Task<OperationResult<FactoringResource>> GetDetailFactoringByRUTAsync(string rut)
        {
            (string query, object param) = FactoringResource.Query_FactoringDetailByRUT(rut);
            telemetry.TrackTrace($"Obteniendo datos del factoring para el rut [{rut}]");
            FactoringResource result = await ExecuteAsync(
                       async (SqlConnection connection, string q, object p) =>
                       {
                           FactoringResource resultSingle = await connection.QueryFirstOrDefaultAsync<FactoringResource>(q, p);
                           return resultSingle;
                       },
                       query,
                       param);
            var execute = new OperationResult<FactoringResource>
            {
                Data = result
            };
            telemetry.TrackTrace($"El resultado del detalle de factoring es: [{result}]");
            return execute;
        }

        /// <inheritdoc cref="IIntelicomRepository.GetDetailDebtorByRUTAsync(string)"/>
        public async Task<OperationCollectionResult<FactoringDebtorResource>> GetDetailDebtorByRUTAsync(string rut)
        {
            (string query, object param) = FactoringDebtorResource.Query_FactoringDebtorByRUT(rut);
            telemetry.TrackTrace($"Obteniendo datos del deudor para el rut [{rut}]");
            IEnumerable<FactoringDebtorResource> result = await ExecuteAsync(
                       async (SqlConnection connection, string q, object p) =>
                       {
                           IEnumerable<FactoringDebtorResource> elements = await connection.QueryAsync<FactoringDebtorResource>(q, p);
                           return elements;
                       },
                       query,
                       param);
            var execute = new OperationCollectionResult<FactoringDebtorResource>
            {
                DataCollection = result,
                Total = result.Count()
            };
            telemetry.TrackTrace($"El resultado del detalle de deudor es: [{result}]");
            return execute;
        }
        ///<inheritdoc cref="IIntelicomRepository.GetDetailLastOperationsByRUTAsync(string)"/>
        public async Task<OperationResult<LastOperationsResource>> GetDetailLastOperationsByRUTAsync(string rut)

        {
            (string query, object param) = LastOperationsResource.Query_LastOperationsByRUT(rut);
            telemetry.TrackTrace($"Obteniendo las últimas operaciones para el rut [{rut}]");
            OperationResult<LastOperationsResource> execute = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    SqlMapper.GridReader resultMultiple = await connection.QueryMultipleAsync(q, p);
                    var total = new LastOperationsResource();
                    {
                        //REVISAR
                        total.NormalRecency = await resultMultiple.ReadFirstOrDefaultAsync<int?>();
                        total.NormalOperation = await resultMultiple.ReadAsync<NormalOperationsResource>();
                        total.ReoperationRecency = await resultMultiple.ReadFirstOrDefaultAsync<int?>();
                        total.Reoperation = await resultMultiple.ReadAsync<ReoperationResource>();
                        total.CreditRecency = await resultMultiple.ReadFirstOrDefaultAsync<int?>();
                        total.LastCredit = await resultMultiple.ReadAsync<LastCreditResource>();
                    }
                    var result = new OperationResult<LastOperationsResource>(total);
                    telemetry.TrackTrace($"El resultado de las últimas operaciones es: [{result}]");
                    return result;

                },
                query,
                param);
            return execute;
        }

        /// <inheritdoc cref="IIntelicomRepository.GetHistoricOperationsByRUTAsync(string)"/>
        public async Task<OperationResult<HistoricOperationsResource>> GetHistoricOperationsByRUTAsync(string rut)
        {
            (string query, object param) = HistoricOperationsResource.Query_HistoricOperationsByRUT(rut);
            telemetry.TrackTrace($"Obteniendo las operaciones históricas para el rut [{rut}]");
            HistoricOperationsResource result = await ExecuteAsync(
                       async (SqlConnection connection, string q, object p) =>
                       {
                           HistoricOperationsResource resultSingle = await connection.QueryFirstOrDefaultAsync<HistoricOperationsResource>(q, p);
                           return resultSingle;
                       },
                       query,
                       param);
            var execute = new OperationResult<HistoricOperationsResource>
            {
                Data = result
            };
            telemetry.TrackTrace($"El resultado de las operaciones históricas es: [{result}]");
            return execute;
        }

        /// <inheritdoc cref="IIntelicomRepository.GetWeightedTermByRUTAsync(string)/>
        public async Task<OperationCollectionResult<WeightedTermResource>> GetWeightedTermByRUTAsync(string rut)
        {
            (string query, object param) = WeightedTermResource.Query_WeightedTermByRUT(rut);
            telemetry.TrackTrace($"Obteniendo los plazos ponderados para el rut [{rut}]");
            IEnumerable<WeightedTermResource> result = await ExecuteAsync(
                       async (SqlConnection connection, string q, object p) =>
                       {
                           IEnumerable<WeightedTermResource> elements = await connection.QueryAsync<WeightedTermResource>(q, p);
                           return elements;
                       },
                       query,
                       param);
            var execute = new OperationCollectionResult<WeightedTermResource>
            {
                DataCollection = result,
                Total = result.Count()
            };
            telemetry.TrackTrace($"El resultado de los plazos ponderados es: [{result}]");
            return execute;
        }

        /// <inheritdoc cref="IIntelicomRepository.GetHistoricCreditsByRUTAsync(string)"/>
        public async Task<OperationResult<CreditResource>> GetHistoricCreditsByRUTAsync(string rut)
        {
            (string query, object param) = CreditResource.Query_CreditDetailByRUT(rut);
            telemetry.TrackTrace($"Obteniendo los créditos históricos para el rut [{rut}]");
            CreditResource result = await ExecuteAsync(
                       async (SqlConnection connection, string q, object p) =>
                       {
                           CreditResource resultSingle = await connection.QueryFirstOrDefaultAsync<CreditResource>(q, p);
                           return resultSingle;
                       },
                       query,
                       param);
            var execute = new OperationResult<CreditResource>
            {
                Data = result
            };
            telemetry.TrackTrace($"El resultado de los créditos históricos es: [{result}]");
            return execute;
        }

        ///<inheritdoc cref= "IIntelicomRepository.GetTotalPaymentsByRUTAsync(string)"/>
        public async Task<OperationResult<TotalResource>> GetTotalPaymentsByRUTAsync(string rut)

        {
            (string query, object param) = TotalResource.Query_TotalPaymentsByRUT(rut);
            telemetry.TrackTrace($"Obteniendo los pagos totales para el rut [{rut}]");
            OperationResult<TotalResource> execute = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    SqlMapper.GridReader resultMultiple = await connection.QueryMultipleAsync(q, p);
                    var total = new TotalResource();
                    {
                        total.Rate = await resultMultiple.ReadFirstOrDefaultAsync<RateResource>();
                        total.Amount = await resultMultiple.ReadFirstAsync<AmountResource>();
                        total.Percentage = await resultMultiple.ReadFirstAsync<PercentageResource>();
                        total.Collection = await resultMultiple.ReadAsync<CollectionResource>();
                        total.Payment = await resultMultiple.ReadAsync<PaymentResource>();
                    }
                    var result = new OperationResult<TotalResource>(total);
                    telemetry.TrackTrace($"El resultado de los pagos totales es: [{result}]");
                    return result;
                },
                query,
                param);
            return execute;
        }

        ///<inheritdoc cref= "IIntelicomRepository.GetPercentagesBalanceSluggishAsync(string)"/>
        public async Task<OperationResult<PercentageBalanceSluggishResource>> GetPercentagesBalanceSluggishAsync(string rut)
        {
            (string query, object param) = PercentageBalanceSluggishResource.Query_PercentageDetailByRUT(rut);
            telemetry.TrackTrace($"Obteniendo los porcentajes para el rut [{rut}]");
            PercentageBalanceSluggishResource result = await ExecuteAsync(
                   async (SqlConnection connection, string q, object p) =>
                   {
                       PercentageBalanceSluggishResource resultSingle = await connection.QueryFirstOrDefaultAsync<PercentageBalanceSluggishResource>(q, p);
                       return resultSingle;
                   },
                   query,
                   param);
            var execute = new OperationResult<PercentageBalanceSluggishResource>
            {
                Data = result
            };
            telemetry.TrackTrace($"El resultado de los porcentajes totales es: [{result}]");
            return execute;
        }
    }
}