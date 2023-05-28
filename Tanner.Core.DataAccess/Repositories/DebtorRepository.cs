using Dapper;
using Microsoft.ApplicationInsights;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;
using Tanner.RelationalDataAccess;

namespace Tanner.Core.DataAccess.Repositories
{
    /// <inheritdoc cref="IDebtorRepository"/>
    public class DebtorRepository : CoreRepository, IDebtorRepository
    {
        private readonly TelemetryClient telemetry;
        private string guidId;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx">DB context</param>
        public DebtorRepository(CoreContext ctx, TelemetryClient _telemetry) : base(ctx)
        {
            telemetry = _telemetry;
        }

        public async Task<OperationResult<DebtorBaseResource>> AddDebtorAsync(DebtorBaseResource debtor)
        {
            var parameters = new
            {
                ss_rut_pagador = debtor.RUT,
                ss_nombre_pagador = debtor.Name
            };

            guidId = Guid.NewGuid().ToString();
            string payload = JsonConvert.SerializeObject(debtor);
            telemetry.TrackTrace($"Payload debtor [{debtor.RUT}]", new Dictionary<string, string> { { "guidId", guidId }, { "payload", payload } });

            OperationResult<DebtorBaseResource> execute = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    DebtorBaseResource debtorAdd =
                    await connection.QueryFirstOrDefaultAsync<DebtorBaseResource>("dbo.SP_TRX_PP_crea_pagador_deudor", p, commandType: CommandType.StoredProcedure);

                    if (debtorAdd == null)
                    {
                        telemetry.TrackTrace($"Existe un deudor con el RUT [{debtor.RUT}]");
                        return OperationResult<DebtorBaseResource>.ConflictResult(debtor.RUT, $"Existe un deudor con el RUT {debtor.RUT}");
                    }

                    var result = new OperationResult<DebtorBaseResource>(debtorAdd);
                    telemetry.TrackTrace($"Se ha agregado el deudor [{result}]");
                    return result;
                },
                null,
                parameters);

            return execute;
        }

        /// <inheritdoc cref="IDebtorRepository.UpdateElectronicReceiverAsync(UpdateElectronicReceiver)"/>
        public async Task<OperationBaseResult> UpdateElectronicReceiverAsync(UpdateElectronicReceiver request)
        {
            var parameters = new
            {
                rut = request.RUT,
                esReceptor = request.IsReceiver
            };

            guidId = Guid.NewGuid().ToString();
            string payload = JsonConvert.SerializeObject(request);
            telemetry.TrackTrace($"Payload Electronic receiver [{request.RUT}]", new Dictionary<string, string> { { "guidId", guidId }, { "payload", payload } });

            OperationBaseResult execute = await ExecuteAsync(
                    async (SqlConnection connection, string q, object p) =>
                    {

                        ElectronicReceiverResource electronicReceiverResource = await connection.QueryFirstAsync<ElectronicReceiverResource>("dba.sp_put_receptor_electronico", p, commandType: CommandType.StoredProcedure);
                        if (electronicReceiverResource.MensajeRespuesta == "Parametros de entrada incorrectos.")
                        {
                            telemetry.TrackTrace($"Parámetros de entrada [{request.RUT}] incorrectos");
                            return OperationBaseResult.BadRequestResult($"Parámetros de entrada {request.RUT} incorrectos");
                        }

                        if (electronicReceiverResource.MensajeRespuesta == "No se encontraron registros con el rut indicado.")
                        {
                            telemetry.TrackTrace($"No existen registros para el RUT:[{request.RUT}]");
                            return OperationBaseResult.NotFoundResult($"No existen registros para el RUT: {request.RUT}");
                        }

                        if (electronicReceiverResource.MensajeRespuesta == "Error al actualizar receptor electronico.")
                        {
                            telemetry.TrackTrace($"Error al actualizar el receptor electrónico:[{request.RUT}]");
                            return OperationBaseResult.BadRequestResult($"Error al actualizar el receptor electrónico: {request.RUT}");
                        }

                        return new OperationBaseResult();
                    },
                    null,
                    parameters);
            return execute;
        }

        /// <inheritdoc cref="IDebtorRepository.GetDebtorDetailAsync(string)"/>
        public async Task<OperationResult<DebtorDataResource>> GetDebtorDetailAsync(string rut)
        {
            (string query, object param) = DebtorDataResource.Query_DebtorDetailByRUT(rut);
            telemetry.TrackTrace($"RUT solicitado para consulta: [{rut}]");
            DebtorDataResource result = await ExecuteAsync(
                        async (SqlConnection connection, string q, object p) =>
                        {
                            DebtorDataResource resultSingle = await connection.QueryFirstOrDefaultAsync<DebtorDataResource>(q, p);
                            return resultSingle;
                        },
                        query,
                        param);

            if (result == null)
            {
                telemetry.TrackTrace($"No se encuentra el RUT: [{rut}]");
                return OperationResult<DebtorDataResource>.NotFoundResult(rut);
            }

            var execute = new OperationResult<DebtorDataResource>
            {
                Data = result
            };
            telemetry.TrackTrace($"El resultado obtenido es: [{result}]");
            return execute;
        }

        /// <inheritdoc cref="IDebtorRepository.GetDataDocumentsDebtorAsync"/>
        public async Task<OperationCollectionResult<DocumentDebtorResource>> GetDataDocumentsDebtorAsync()
        {
            (string query, object param) = DocumentDebtorResource.Query_DebtorDocuments();

            IEnumerable<DocumentDebtorResource> result = await ExecuteAsync(
                        async (SqlConnection connection, string q, object p) =>
                        {
                            IEnumerable<DocumentDebtorResource> elements = await connection.QueryAsync<DocumentDebtorResource>(q, p);
                            return elements;
                        },
                        query,
                        param);

            var execute = new OperationCollectionResult<DocumentDebtorResource>
            {
                DataCollection = result,
                Total = result.Count()
            };

            return execute;
        }
    }
}
