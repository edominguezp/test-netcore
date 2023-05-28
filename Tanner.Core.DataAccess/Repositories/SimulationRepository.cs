using Dapper;
using Microsoft.ApplicationInsights;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Models;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;
using static Dapper.SqlMapper;

namespace Tanner.Core.DataAccess.Repositories
{
    ///<inheritdoc cref="ISimulationRepository"/>
    public class SimulationRepository : CoreRepository, ISimulationRepository
    {
        private readonly TelemetryClient telemetry;
        private string guidId;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx">DB context</param>
        public SimulationRepository(CoreContext ctx, TelemetryClient _telemetry) : base(ctx)
        {
            telemetry = _telemetry;
        }

        public async Task<OperationResult<long>> AddSimulation(AddSimulation request)
        {
            var parameters = new
            {
                ss_rut_cliente = request.ClientRUT,
                numero_cotizacion = (decimal?)null,
                fecha_operacion = request.OperationDate.Date,
                tasa_operacion = request.OperationRate,
                porcentaje_descuento = request.PercentageDiscount,
                codigo_moneda = request.CurrencyCode,
                codigo_producto = request.ProductCode,
                tipo_comi_cob = request.TypeCommisionCob,
                monto_comision = request.CommissionAmount,
                factor_comi_cob = request.CommissionFactor,
                minimo_comi_cob = request.MinimumCollectionCommission,
                maximo_comi_cob = request.MaximumCollectionCommission,
                an_gasto_fijo_oper = request.FixedExpense,
                av_tipo_operacion = request.OperationType,
                numero_simulacion = request.SimulationNumber,
                av_origen = request.Av_Origin
            };

            guidId = Guid.NewGuid().ToString();
            string payload = JsonConvert.SerializeObject(request);
            telemetry.TrackTrace($"Payload para simulación rut: [{request.ClientRUT}]", new Dictionary<string, string> { { "guidId", guidId }, { "payload", payload } });

            OperationResult<long> execute = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    GridReader multipleResponse = await connection.QueryMultipleAsync("dba.pr_OPE_fin0116", p, commandType: CommandType.StoredProcedure);
                    var obj = new SimulationResource
                    {
                        QuotationNumber = await multipleResponse.ReadSingleAsync<long>(),
                        AnswerResource = await multipleResponse.ReadSingleAsync<AnswerResource>()
                    };

                    if (obj.AnswerResource.ResponseCode == 0)
                    {
                        List<Task<IEnumerable<dynamic>>> taskDocument = new List<Task<IEnumerable<dynamic>>>();
                            
                        foreach (AddDocumentToSimulation doc in request.Documents)
                        {
                            var parametersDoc = new
                            {
                                numero_documento = doc.DocumentNumber,
                                valor_nominal_documento = doc.NominalValue,
                                fecha_vencimiento_documento = doc.ExpiredDate,
                                numero_cotizacion = obj.QuotationNumber,
                                ss_rut_deudor = doc.DebtorRUT,
                                ss_rut_cliente = request.ClientRUT,
                                ad_fecha_emision = doc.IssueDate,
                                tipo_dcto_sii = doc.DteType,
                                iddoc_simulacion = doc.IdDocSimulation
                            };

                            guidId = Guid.NewGuid().ToString();
                            string payloadDoc = JsonConvert.SerializeObject(parametersDoc);
                            telemetry.TrackTrace($"Payload para simulación : [{request.SimulationNumber}]", new Dictionary<string, string> { { "guidId", guidId }, { "payload", payloadDoc } });

                            taskDocument.Add(connection.QueryAsync("dba.pr_DOC_fin0086", parametersDoc, commandType: CommandType.StoredProcedure));
                        }

                        if(taskDocument.Count > 0)
                        {
                            await Task.WhenAll(taskDocument);
                        }

                        var parameters3 = new
                        {
                            ss_rut_cliente = request.ClientRUT,
                            numero_cotizacion = obj.QuotationNumber,
                            fecha_operacion = request.OperationDate.Date,
                            tasa_operacion = request.OperationRate,
                            porcentaje_descuento = request.PercentageDiscount,
                            codigo_moneda = request.CurrencyCode,
                            codigo_producto = request.ProductCode,
                            tipo_comi_cob = request.TypeCommisionCob,
                            monto_comision = request.CommissionAmount,
                            factor_comi_cob = request.CommissionFactor,
                            minimo_comi_cob = request.MinimumCollectionCommission,
                            maximo_comi_cob = request.MaximumCollectionCommission,
                            an_gasto_fijo_oper = request.FixedExpense,
                            av_tipo_operacion = request.OperationType,
                            numero_simulacion = request.SimulationNumber,
                            av_origen = request.Av_Origin
                        };

                        guidId = Guid.NewGuid().ToString();
                        string payload3 = JsonConvert.SerializeObject(parameters3);
                        telemetry.TrackTrace($"Payload para simulación : [{request.SimulationNumber}]", new Dictionary<string, string> { { "guidId", guidId }, { "payload", payload3 } });

                        GridReader multipleResponse1 = await connection.QueryMultipleAsync("dba.pr_OPE_fin0116", parameters3, commandType: CommandType.StoredProcedure);
                        obj = new SimulationResource
                        {
                            QuotationNumber = await multipleResponse1.ReadSingleAsync<long>(),
                            AnswerResource = await multipleResponse1.ReadSingleAsync<AnswerResource>()
                        };
                    }

                    if (obj.AnswerResource.ResponseCode != 0)
                    {
                        telemetry.TrackTrace($"Ha ocurrido un error en la simulación para el cliente: [{request.ClientRUT}]");
                        return OperationResult<long>.InternalErrorResult(null, obj.AnswerResource.Response);
                    }
                    var result = new OperationResult<long>(obj.QuotationNumber);
                    telemetry.TrackTrace($"Número de cotización: [{result}]");
                    return result;
                },
                null,
                parameters);

            return execute;
        }

        public async Task<OperationResult<SimulationResults>> GetSimulationResults(long simulationId)
        {            
            var parameters = new
            {
                nro_cotizacion = simulationId
            };
            OperationResult<SimulationResults> simulationresults = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    telemetry.TrackTrace($"Obteniendo resume planilla con el id: [{simulationId}]");
                    GridReader multipleResponse = await connection.QueryMultipleAsync("dba.SP_resumen_planilla_cotizacion", p, commandType: CommandType.StoredProcedure);
                    SimulationResults simulationResults = await multipleResponse.ReadFirstOrDefaultAsync<SimulationResults>();

                    OperationResult<SimulationResults> result = new OperationResult<SimulationResults>
                    {
                        Data = null
                    };

                    if (simulationResults != null)
                    {
                        simulationResults.Documents = await multipleResponse.ReadAsync<SimulationDocuments>();
                        result.Data = simulationResults;
                    }
                    return result;
                },
            null, parameters);

            if (simulationresults.Data == null)
            {
                telemetry.TrackTrace($"No se encuentra la simulación: [{simulationId}]");
                return OperationResult<SimulationResults>.NotFoundResult(simulationId);
            }
            telemetry.TrackTrace($"El resultado de la simulación es: [{simulationresults}]");

            return simulationresults;
        }
    }
}
