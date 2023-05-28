using Dapper;
using Microsoft.ApplicationInsights;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Models;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;

namespace Tanner.Core.DataAccess.Repositories
{
    /// <inheritdoc cref="IOperationRepository"/>
    public class DocumentRepository : CoreRepository, IDocumentRepository
    {
        private readonly TelemetryClient telemetry; 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx">DB context</param>
        public DocumentRepository(CoreContext ctx, TelemetryClient _telemetry) : base(ctx)
        {
            telemetry = _telemetry;
        }

        ///<inheritdoc cref = "IDocumentRepository.GetOperationsbyDocumentNumberAsync(string, string)" />
        public async Task<OperationCollectionResult<DataOperationResource>> GetOperationsbyDocumentNumberAsync(string number, string rut)
        {
            (string query, object param) = DataOperationResource.Query_GetOperationbyDocumentNumber(rut, number);
            telemetry.TrackTrace($"Parámetros a consultar RUT: [{rut}] y número de documento[{number}]");
            IEnumerable<DataOperationResource> result = await ExecuteAsync(
                        async (SqlConnection connection, string q, object p) =>
                        {
                            IEnumerable<DataOperationResource> elements = await connection.QueryAsync<DataOperationResource>(q, p);
                            return elements;
                        },
                        query,
                        param);
            var execute = new OperationCollectionResult<DataOperationResource>
            {
                DataCollection = result,
                Total = result.Count()
            };
            telemetry.TrackTrace($"Se han obtenido las operaciones [{result}]");
            return execute;
        }

        public async Task<OperationBaseResult> UpdateGrantDocumentAsync(DocumentGrantRequest documentGrantRequest)
        {
            (string query, object param) = DocumentGrantResource.Query_UpdateDocumentGrant(documentGrantRequest);
            telemetry.TrackTrace($"Actualizando estado de cesión del documento: {JsonConvert.SerializeObject(documentGrantRequest)}");
            
            var result = await ExecuteAsync(
                        async (SqlConnection connection, string q, object p) =>
                        {
                            var resultSQL = await connection.ExecuteAsync(q, p);
                            return resultSQL;
                        },
                        query,
                        param);
            telemetry.TrackTrace($"El estado de cesión del documento se actualizo correctamente. [{JsonConvert.SerializeObject(documentGrantRequest)}]");
            return new OperationBaseResult();
        }
         
        public async Task<OperationResult<int>> GetDayDocumentAsync(DateTime expiryDate, string product)
        {
            if (string.IsNullOrEmpty(product))
                return OperationResult<int>.BadRequestResult("El producto no puede ser vacio ó null");

            (string query, object param) = DayResource.Query_DocumentDays(expiryDate, product);

            int result = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    var resultSQL = await connection.QueryFirstOrDefaultAsync<int>(q, p);
                    return resultSQL;
                },
                query,
                param);

            OperationResult<int> output = new OperationResult<int> { Data = result };
            return output;
        }

        public async Task<OperationBaseResult> CreateDocumentAsync(CreateDocumentRequest request)
        {
            try
            {
                object param = DocumentCreateResource.Query_CreateDocument(request);
                telemetry.TrackTrace($"Creando documento {JsonConvert.SerializeObject(request)} en la operación: {request.OperationNumber}");
                var result = await ExecuteAsync(
                    async (SqlConnection connection, string q, object p) =>
                    {
                        var resultSQL = await connection.ExecuteAsync("dba.pr_fin0086", p, commandType: System.Data.CommandType.StoredProcedure);
                        return resultSQL;
                    }, null, param);
                telemetry.TrackTrace($"Fin de la creación del documento {JsonConvert.SerializeObject(request)} en la operación: {request.OperationNumber}");
                return new OperationBaseResult();
            }
            catch (Exception e)
            {
                telemetry.TrackException(e);
                return OperationBaseResult.BadRequestResult(e.Message);
            }
        }

    }
}