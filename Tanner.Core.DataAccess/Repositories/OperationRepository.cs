using Dapper;
using Microsoft.ApplicationInsights;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.Enums;
using Tanner.Core.DataAccess.Helper;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Models;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;
using Tanner.RelationalDataAccess.Exceptions;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using static Tanner.Core.DataAccess.ModelResources.OperationByDocumentResource;

namespace Tanner.Core.DataAccess.Repositories
{
    /// <inheritdoc cref="IOperationRepository"/>
    public class OperationRepository : CoreRepository, IOperationRepository
    {
        private readonly TelemetryClient telemetry;
        private string guidId;
        private readonly ISimulationRepository _simulation;
        private readonly IClientRepository _client;
        private readonly IDebtorRepository _debtor;
        private readonly IBranchOfficeRepository _branchOffice;
        private readonly IDocumentRepository _document;
        private readonly IChangeTypeRepository _changeTypeRepository;
        private StringBuilder builder;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx">DB context</param>
        public OperationRepository(CoreContext ctx, TelemetryClient _telemetry, ISimulationRepository simulation, 
                                IClientRepository client, IDebtorRepository debtor, IBranchOfficeRepository branchOffice,
                                IDocumentRepository document, IChangeTypeRepository changeTypeRepository) : base(ctx)
        {
            telemetry = _telemetry;
            _simulation = simulation;
            _client = client;
            _debtor = debtor;
            _changeTypeRepository = changeTypeRepository;
            _branchOffice = branchOffice;
            _document = document;
            builder = new StringBuilder();
        }

        /// <inheritdoc cref="IOperationRepository.GetOperationsByNumberAsync(int)"/>
        public async Task<OperationDetailResource> GetOperationsByNumberAsync(int number)
        {
            (string query, object param) = OperationDetailResource.Query_GetOperationDataByNumber(number);
            telemetry.TrackTrace($"Consultando operaciones para el rut: [{number}]");

            OperationDetailResource result = await ExecuteAsync(
                        async (SqlConnection connection, string q, object p) =>
                        {
                            OperationDetailResource resultSingle = await connection.QueryFirstOrDefaultAsync<OperationDetailResource>(q, p);
                            return resultSingle;
                        },
                        query,
                        param);
            telemetry.TrackTrace($"Las operaciones obtenidas son: [{result}]");
            return result;
        }

        /// <inheritdoc cref="IOperationRepository.GetOperationsByExecutiveOrAgentAsync(OperationByExecutiveOrAgent)"/>
        public async Task<OperationCollectionResult<OperationResource>> GetOperationsByExecutiveOrAgentAsync(OperationByExecutiveOrAgent request)
        {
            (string query, object param) = OperationResource.Query_GetOperationsByEmployeeEmail(request);
            telemetry.TrackTrace($"Obteniendo las operaciones del email: [{request.Email}] en los últimos {request.Days} días");

            if (request.Paginate != null)
            {
                query = ApplyPaginate(query, request.Paginate);
            }

            (long total, IEnumerable<OperationResource> elements) data = await ExecuteAsync(
            async (SqlConnection connection, string q, object p) =>
            {
                SqlMapper.GridReader resultMultiple = await connection.QueryMultipleAsync(q, p);
                long total = resultMultiple.ReadSingle<long>();
                IEnumerable<OperationResource> elements = resultMultiple.Read<OperationResource>();
                return (total, elements);
            },
            query,
            param);

            var result = new OperationCollectionResult<OperationResource>
            {
                DataCollection = data.elements,
                Total = data.total
            };
            telemetry.TrackTrace($"Las operaciones obtenidas son: [{data.elements}]");
            return result;
        }

        /// <inheritdoc cref="IDataRepository.GetDocumentsByOperationAsync(int)"/>
        public async Task<OperationResult<OperationSummaryResource>> GetDocumentsByOperationAsync(int operationNumber)
        {
            (string query, object param) = OperationDataResource.Query_DataDocumentByOperation(operationNumber);
            telemetry.TrackTrace($"Obteniendo documentos por operación para el número de operación: [{operationNumber}]");
            OperationSummaryResource data = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    SqlMapper.GridReader resultMultiple = await connection.QueryMultipleAsync(q, p);
                    OperationDataResource operation = resultMultiple.ReadFirstOrDefault<OperationDataResource>();
                    OperationSummaryResource result = null;
                    if (operation != null)
                    {
                        operation.Documents = resultMultiple.Read<DocumentDataResource>();
                        result = new OperationSummaryResource
                        {
                            Operation = operation,
                            TotalOperations = resultMultiple.ReadSingle<int>(),
                            TotalOperation6Months = resultMultiple.ReadSingle<int>(),
                            TotalOperation9Months = resultMultiple.ReadSingle<int>(),
                            MaximunOperationAmount = resultMultiple.ReadSingle<long>(),
                            PaymentPercentage = resultMultiple.ReadSingle<decimal>()
                        };
                    }
                    return result;
                },
                query,
                param);

            if (data == null)
            {
                telemetry.TrackTrace($"No se encuentran operaciones para el número de operación: [{operationNumber}]");
                return OperationResult<OperationSummaryResource>.NotFoundResult(operationNumber);
            }

            var execute = new OperationResult<OperationSummaryResource>
            {
                Data = data
            };
            telemetry.TrackTrace($"Las operaciones son: [{data}]");
            return execute;
        }

        /// <inheritdoc cref="IOperationRepository.GetOperationsByStateAsync(OperationByStatus)"/>
        public async Task<OperationCollectionResult<OperationsStatesResource>> GetOperationsByStateAsync(OperationByStatus request)
        {
            (string query, object parameters) = OperationsStatesResource.Query_OperationInAnalysis(request);

            telemetry.TrackTrace($"Obteniendo las operaciones para los estados: [{string.Join(", ", request.Status)}], la zona: [{request.ZoneCode}], y la sucursal: [{request.BranchOfficeCode}]");

            if (request.Paginate != null)
            {
                if (request.Paginate.OrderBy != null && request.Paginate.OrderBy.Any())
                {
                    //TODO: Hacerlo más genérico
                    for (int i = 0; i < request.Paginate.OrderBy.Length; i++)
                    {
                        if (request.Paginate.OrderBy[i].ToLower() == nameof(OperationsStatesResource.Number).ToLower())
                        {
                            request.Paginate.OrderBy[i] = "ope.numero_operacion";
                        }
                        else if (request.Paginate.OrderBy[i].ToLower() == nameof(OperationsStatesResource.Zone).ToLower())
                        {
                            request.Paginate.OrderBy[i] = "zo.nombre_zona";
                        }
                        else if (request.Paginate.OrderBy[i].ToLower() == nameof(OperationsStatesResource.BranchOffice).ToLower())
                        {
                            request.Paginate.OrderBy[i] = "suc.descripcion_sucursal";
                        }
                        else
                        {
                            request.Paginate.OrderBy[i] = null;
                        }
                    }
                    request.Paginate.OrderBy = request.Paginate.OrderBy.Where(t => !string.IsNullOrEmpty(t)).ToArray();
                }

                query = ApplyPaginate(query, request.Paginate);
            }

            (IEnumerable<OperationsStatesResource> dataCollection, int total) = await ExecuteAsync(
            async (SqlConnection connection, string q, object p) =>
            {
                SqlMapper.GridReader multipleResult = await connection.QueryMultipleAsync(q, p);
                int totalElements = await multipleResult.ReadSingleAsync<int>();
                IEnumerable<OperationsStatesResource> elements = await multipleResult.ReadAsync<OperationsStatesResource>();
                var data = (elements, totalElements);
                return data;
            },
            query, parameters);

            var result = new OperationCollectionResult<OperationsStatesResource>
            {
                DataCollection = dataCollection,
                Total = total
            };
            telemetry.TrackTrace($"Las operaciones son: [{dataCollection}]");

            return result;
        }

        /// <inheritdoc cref="IOperationRepository.GetProposedPaymentAsync(int, CommercialTermsResource)"/>
        public async Task<OperationResult<ProposedPaymentResource>> GetProposedPaymentAsync(int number, CommercialTermsResource request)
        {
            telemetry.TrackTrace($"Obteniendo el pago propuesto para el número de operación: [{number}]");

            (string query, object param) = OperationDocumentsAmountResource.Query_GetProposedAndActualPayment(number, request);
            
            ProposedPaymentResource response = await ExecuteAsync(
                        async (SqlConnection connection, string q, object p) =>
                        {
                            ProposedPaymentResource responsePayment = 
                                await connection.QueryFirstOrDefaultAsync<ProposedPaymentResource>(q, p);
                            return responsePayment;
                        },
                        query,
                        param);
            
            var result = new OperationResult<ProposedPaymentResource>(response);

            var msg = $"El resultado de los calculos es: [{JsonConvert.SerializeObject(result)}]";
            telemetry.TrackTrace(msg);

            return result;
        }

        /// <inheritdoc cref="IOperationRepository.GetOperationSummaryAsync(bool, int)"/>
        public async Task<OperationResult<OperationDetailSummaryResource>> GetOperationSummaryAsync(bool isQuotation, int number)
        {
            (string query, object param) = OperationDetailSummaryResource.Query_GetOperationSummary(number, isQuotation);
            telemetry.TrackTrace($"Obteniendo total de operaciones o cotización para el número [{number}]");

            OperationDetailSummaryResource result = await ExecuteAsync(
                        async (SqlConnection connection, string q, object p) =>
                        {
                            OperationDetailSummaryResource resultSingle = await connection.QueryFirstOrDefaultAsync<OperationDetailSummaryResource>(q, p);
                            return resultSingle;
                        },
                        query,
                        param);
            var execute = new OperationResult<OperationDetailSummaryResource>
            {
                Data = result
            };
            telemetry.TrackTrace($"El total de operaciones es: [{result}]");
            return execute;
        }

        /// <inheritdoc cref="IOperationRepository.GetDataDocumentsByOperationAsync(int)"/>
        public async Task<OperationCollectionResult<SummaryDocumentResource>> GetDataDocumentsByOperationAsync(int number)
        {
            (string query, object param) = SummaryDocumentResource.Query_GetDataDocumentsByOperation(number);
            telemetry.TrackTrace($"Obteniendo la data de los documentos para la operación [{number}]");

            IEnumerable<SummaryDocumentResource> result = await ExecuteAsync(
                        async (SqlConnection connection, string q, object p) =>
                        {
                            IEnumerable<SummaryDocumentResource> elements = await connection.QueryAsync<SummaryDocumentResource>(q, p);
                            return elements;
                        },
                        query,
                        param);

            var execute = new OperationCollectionResult<SummaryDocumentResource>
            {
                DataCollection = result,
                Total = result.Count()
            };
            telemetry.TrackTrace($"Los documentos obtenidos según el número de operación son: [{result}]");

            return execute;
        }

        /// <inheritdoc cref="IOperationRepository.GetSummaryDocumentsByOperationAsync(int)"/>
        public async Task<OperationCollectionResult<DocumentSummaryResource>> GetSummaryDocumentsByOperationAsync(int number)
        {
            (string query, object param) = DocumentSummaryResource.Query_SummaryDocuments(number);
            telemetry.TrackTrace($"Obteniendo los documentos para la operación [{number}]");

            IEnumerable<DocumentSummaryResource> result = await ExecuteAsync(
                        async (SqlConnection connection, string q, object p) =>
                        {
                            IEnumerable<DocumentSummaryResource> elements = await connection.QueryAsync<DocumentSummaryResource>(q, p);
                            return elements;
                        },
                        query,
                        param);

            var execute = new OperationCollectionResult<DocumentSummaryResource>
            {
                DataCollection = result,
                Total = result.Count()
            };
            telemetry.TrackTrace($"El resultado de los documentos es: [{result}]");
            return execute;
        }

        /// <inheritdoc cref="IOperationRepository.UpdateCommercialTermOperationAsync(UpdateCommercialTermOperation)"/>
        public async Task<OperationBaseResult> UpdateCommercialTermOperationAsync(UpdateCommercialTermOperation request)
        {
            var parameters = new
            {
                numero_operacion = request.OperationNumber,
                tasa_operacion = request.Rate,
                monto_comi_cob = request.Commission,
                an_gasto_fijo_oper = request.Expenses,
                av_usuario = request.User,
                tipo = request.OperationTypeCode
            };

            guidId = Guid.NewGuid().ToString();
            string payload = JsonConvert.SerializeObject(request);
            telemetry.TrackTrace($"Payload Terminos comerciales [{request.OperationNumber}]", new Dictionary<string, string> { { "guidId", guidId }, { "payload", payload } });

            OperationBaseResult execute = await ExecuteAsync(
                    async (SqlConnection connection, string q, object p) =>
                    {
                        (string queryExist, object param) = OperationDataResource.Query_ExistOperation(parameters.numero_operacion);
                        bool exist = await connection.QueryFirstAsync<bool>(queryExist, param);
                        if (!exist)
                        {
                            telemetry.TrackTrace($"No existe el número de operación [{parameters.numero_operacion}]");
                            return OperationBaseResult.NotFoundResult($"No existe el número de operación: " + parameters.numero_operacion);
                        }
                        try
                        {
                            await connection.QueryAsync("dba.pr_act_condiciones_comerciales", p, commandType: CommandType.StoredProcedure);
                        }
                        catch (Exception ex)
                        {
                            if (ex.InnerException == null)
                                return OperationBaseResult.BadRequestResult(ex.Message);

                            throw;
                        }

                        var result = new OperationBaseResult();
                        telemetry.TrackTrace($"El resultado de la actualización es: [{result}]");

                        return result;
                    },
                    null,
                    parameters);

            return execute;
        }

        /// <inheritdoc cref="IOperationRepository.ValidateDocument(OperationDocumentParameter)"/>
        public async Task<OperationCollectionResult<ValidateDocumentResource>> ValidateDocument(OperationDocumentParameter search)
        {
            guidId = Guid.NewGuid().ToString();
            string payloadD = JsonConvert.SerializeObject(search);
            telemetry.TrackTrace($"Payload Validate documents [{search.ClientRUT}]", new Dictionary<string, string> { { "guidId", guidId }, { "payload", payloadD } });
            
            (string query, object param) = ValidateDocumentResource.Query_DocumentValidate(search.ClientRUT, search.DebtorRUT, search.Documents, search.DocumentType);

            OperationCollectionResult<ValidateDocumentResource> execute = await ExecuteAsync(
                    async (SqlConnection connection, string q, object p) =>
                    {
                        IEnumerable<ValidateDocumentResource> data = await connection.QueryAsync<ValidateDocumentResource>(q, p);
                        var result = new OperationCollectionResult<ValidateDocumentResource>
                        {
                            DataCollection = data,
                            Total = data.Count()
                        };
                        telemetry.TrackTrace($"El resultado de la validación es: [{data}]");
                        return result;
                    },
                    query,
                    param);
            return execute;
        }

        /// <inheritdoc cref="IOperationRepository.GetAmountQuotationByNumberQuotationAsync(int)"/>
        public async Task<OperationResult<QuoteAmountResource>> GetAmountQuotationByNumberQuotationAsync(int number)
        {
            (string query, object param) = QuoteAmountResource.Query_GetAmountQuotation(number);
            telemetry.TrackTrace($"Obteniendo el monto de cotización para el número de cotización: [{number}]");

            QuoteAmountResource result = await ExecuteAsync(
                        async (SqlConnection connection, string q, object p) =>
                        {
                            QuoteAmountResource resultSingle = await connection.QueryFirstOrDefaultAsync<QuoteAmountResource>(q, p);
                            return resultSingle;
                        },
                        query,
                        param);
            var execute = new OperationResult<QuoteAmountResource>
            {
                Data = result
            };
            telemetry.TrackTrace($"El monto de la cotización es: [{result}]");

            return execute;
        }

        /// <inheritdoc cref="IOperationRepository.UpdateDatesOperationAsync(UpdateDatesOperation)"/>
        public async Task<OperationBaseResult> UpdateDatesOperationAsync(UpdateDatesOperation request)
        {
            var parameters = new
            {
                ss_rut_cliente = request.ClientRUT,
                ll_num_documento = request.DocumentNumber,
                ld_fec_emi = request.IssueDate,
                ld_fec_ces = request.GrantedDate,
                ld_fec_rec = request.ReceptionDate
            };
            guidId = Guid.NewGuid().ToString();
            string payloadDates = JsonConvert.SerializeObject(request);
            telemetry.TrackTrace($"Payload Dates [{request.ClientRUT}]", new Dictionary<string, string> { { "guidId", guidId }, { "payload", payloadDates } });
            request.ClientRUT = JsonConvert.SerializeObject(request);

            OperationBaseResult execute = await ExecuteAsync(
                    async (SqlConnection connection, string q, object p) =>
                    {
                        CodeSPResponse responseData = await connection.QueryFirstAsync<CodeSPResponse>("dba.Actualizar_Fechas_Documentos", p, commandType: CommandType.StoredProcedure);
                        if (responseData.Answer == "Documento no encontrado.")
                        {
                            telemetry.TrackTrace($"No existe el documento: [{request.DocumentNumber}]");
                            return OperationBaseResult.NotFoundResult(request.DocumentNumber, $"El documento con número: {request.DocumentNumber} no existe");
                        }
                        if (responseData.Answer == "Cliente no encontrado.")
                        {
                            telemetry.TrackTrace($"No se encuenta el cliente: [{request.ClientRUT}]");
                            return OperationBaseResult.NotFoundResult(request.ClientRUT, $"El cliente con RUT: {request.ClientRUT} no existe");
                        }
                        return new OperationBaseResult();
                    },
                    null,
                    parameters);
            return execute;
        }

        /// <inheritdoc cref="IOperationRepository.RegisterApproved(RegisterApprovedParameters)"/>
        public async Task<OperationBaseResult> RegisterApproved(RegisterApprovedParameters request)
        {
            var parameters = new
            {
                an_numero_operacion = (int)request.OperationNumber,
                av_login = request.Login,
                an_estado = request.ApprovedState
            };
            guidId = Guid.NewGuid().ToString();
            string payloadRegister = JsonConvert.SerializeObject(request);
            telemetry.TrackTrace($"Payload Register approved [{request.OperationNumber}]", new Dictionary<string, string> { { "guidId", guidId }, { "payload", payloadRegister } });

            OperationBaseResult execute = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    (string queryExist, object param) = OperationDataResource.Query_ExistOperation(parameters.an_numero_operacion);
                    bool exist = await connection.QueryFirstAsync<bool>(queryExist, param);
                    if (!exist)
                    {
                        telemetry.TrackTrace($"No existe el número de operación[{parameters.an_numero_operacion}]");
                        return OperationBaseResult.NotFoundResult($"No existe el número de operación: " + parameters.an_numero_operacion);
                    }
                    await connection.QueryAsync("dbo.spi_visto_bueno_condiciones_comerciales", p, commandType: CommandType.StoredProcedure);
                    var result = new OperationBaseResult();
                    telemetry.TrackTrace($"El resultado de registrar la aprobación es [{result}]");
                    return result;
                },
                null,
                parameters);

            return execute;
        }

        /// <inheritdoc cref="IOperationRepository.MarkRejectedDocument(UpdateDocument)"/>
        public async Task<OperationBaseResult> MarkRejectedDocument(UpdateDocument request)
        {
            var parameters = new
            {
                num_operacion = request.OperationNumber,
                folio = request.DocumentNumber,
                factura_fecha_rechazada = request.RejectionDate,
                codigo_fact_rechazada = (int)request.InvoiceCode
            };
            guidId = Guid.NewGuid().ToString();
            string payloadReject = JsonConvert.SerializeObject(request);
            telemetry.TrackTrace($"Payload reject [{request.OperationNumber}]", new Dictionary<string, string> { { "guidId", guidId }, { "payload", payloadReject } });

            OperationBaseResult execute = await ExecuteAsync(
                    async (SqlConnection connection, string q, object p) =>
                    {
                        await connection.QueryAsync("dbo.pget_Cambia_estado_documento", p, commandType: CommandType.StoredProcedure);
                        var result = new OperationBaseResult();
                        telemetry.TrackTrace($"El resultado del rechazo en el sp es: [{result}]");
                        return result;
                    },
                    null,
                    parameters);
            return execute;
        }

        /// <inheritdoc cref="IOperationRepository.InsertOperation(InsertOperation)"/>
        public async Task<OperationResult<long?>> InsertOperation(InsertOperation request)
        {
            var parameters = new
            {
                id_cotizacion = request.QuotationID,
                banco_SBIF = request.BankCode,
                ctacte_giro = request.CurrentAccount
            };

            guidId = Guid.NewGuid().ToString();
            string payloadInsertOperation = JsonConvert.SerializeObject(request);
            telemetry.TrackTrace($"Payload Insert Operation [{request.QuotationID}]", new Dictionary<string, string> { { "guidId", guidId }, { "payload", payloadInsertOperation } });
            string message = null;
            OperationResult<long?> execute = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    long? operationNumber = null;

                    connection.InfoMessage += OnInfoMessageEvent;
                    decimal resultado = await connection.QueryFirstOrDefaultAsync<decimal>("dba.Sp_CursarOperaciones", p, commandType: CommandType.StoredProcedure);
                    connection.InfoMessage -= OnInfoMessageEvent;

                    message = builder.ToString();

                    operationNumber = Convert.ToInt64(resultado);
                    telemetry.TrackTrace($"Se ejecuta el sp con el resultado: [{operationNumber}]");

                    var result = new OperationResult<long?>
                    {
                        Data = operationNumber
                    };
                    telemetry.TrackTrace($"El resultado de insertar la operación es: [{JsonConvert.SerializeObject(result)}]");
                    return result;
                },
                null,
                parameters);

            if (execute.Data == null)
            {
                telemetry.TrackTrace($"No se encuentra la cotización: [{request.QuotationID}]");
                execute = OperationResult<long?>.NotFoundResult(request.QuotationID);
            }
            if(execute.Data == 0)
            {
                telemetry.TrackTrace($"No fue posible ingresar como operación la cotización [{request.QuotationID}] por los siguientes motivos");
                string[] messageArray = message.Split("\r\n");
                foreach (string messa in messageArray)
                {
                    if (!string.IsNullOrEmpty(messa))
                    {
                        telemetry.TrackTrace(messa);
                    }                    
                }
                execute = OperationResult<long?>.BadRequestResult(message);
            }
            return execute;
        }

        public async Task<OperationResult<long?>> CreateOperationWithClientInfoNoSimulation(CreateOperationWithClientNoSimulation request)
        {   
            #region listado de deudores
            IEnumerable<string> debtorRUTs = request.Documents.Select(x => x.DebtorRUT);
            #endregion

            #region obtiene deudores como deudor
            var debtorDebtor = new List<OperationResult<DebtorDataResource>>();
            foreach (string item in debtorRUTs)
            {
                telemetry.TrackTrace($"Se consulta la existencia del deudor {item}");
                OperationResult<DebtorDataResource> debtorDetail = await _debtor.GetDebtorDetailAsync(item);
                debtorDebtor.Add(debtorDetail);
            }
            if (debtorDebtor.Any(x => x.ErrorStatus != null))
            {
                telemetry.TrackTrace($"Al menos uno de los ruts de los documentos no se encuentra como deudor en CORE");
                return OperationResult<long?>.BadRequestResult("Al menos uno de los ruts de los documentos no se encuentra como deudor en CORE");
            }
            #endregion

            #region obtiene deudores como cliente
            decimal debtorClient = 0;
            foreach (string item in debtorRUTs)
            {
                telemetry.TrackTrace($"Se consulta la existencia del deudor {item} como cliente");
                OperationResult<ClientResource> data = await _client.GetClientByRUTAsync(item);
                if (data.ErrorStatus == null)
                {
                    debtorClient++;
                }
            }
            if (debtorClient != debtorRUTs.Count())
            {
                telemetry.TrackTrace($"Al menos uno de los ruts en los documentos no se encuentra como cliente en CORE");
                return OperationResult<long?>.BadRequestResult("Al menos uno de los ruts en los documentos no se encuentra como cliente en CORE");
            }
            #endregion

            #region obtiene cliente como cliente
            telemetry.TrackTrace($"Se consulta la existencia del cliente {JsonConvert.SerializeObject(request.DataClient.RUT)}");
            OperationResult<ClientResource> client = await _client.GetClientByRUTAsync(request.DataClient.RUT);

            if (client.ErrorStatus != null)
            {
                //Crear cliente
                telemetry.TrackTrace($"Se crea el cliente con los siguientes párametros: {JsonConvert.SerializeObject(request.DataClient)}");
                OperationResult<ClientBaseResource> newClient = await _client.AddClientAsync(request.DataClient);
                if (newClient.Data == null)
                {
                    telemetry.TrackTrace($"No fue posible crear el usuario como nuevo cliente");
                    return OperationResult<long?>.BadRequestResult("No fue posible crear el nuevo cliente");
                }
            }
            #endregion

            #region obtiene el codigo de cliente
            int? clientCode = await _client.GetClientCodeByRut(request.DataClient.RUT);
            #endregion

            #region codigo de la sucursal
            OperationCollectionResult<BranchOfficeResource> codeBranchOfficeResource = await _branchOffice.GetBranchOfficeByClientCodeAsync(clientCode.Value);
            if(!codeBranchOfficeResource.DataCollection.Any())
            {
                string situation = $"No fue posible obtener el código de la sucursal para el cliente {request.DataClient.RUT}";
                telemetry.TrackTrace(situation);
                return OperationResult<long?>.BadRequestResult(situation);
            }
            int codeBranchOffice = codeBranchOfficeResource.DataCollection.First().Code;
            #endregion

            #region averigua si es primera operacion
            var consultDocs = new OperationByRutRequest
            {
                Page = 1,
                PageSize = 1
            };
            OperationResult<OperationDocumentResource> docs = await GetOperationDocumentByRUTAsync(request.DataClient.RUT, consultDocs);

            string xOperation = "N";

            if (docs.Data?.Total == 0)
            {
                xOperation = "PO";
            }
            #endregion

            #region creación de listado de documentos
            var docList = new List<CreateDocumentRequest>();
            foreach (CreateDocumentNoSimulation item in request.Documents)
            {
                var newDoc = new CreateDocumentRequest
                {
                    ClientCode = clientCode.Value,
                    DocumentNumber = Convert.ToInt64(item.DocumentNumber),
                    DocumentValue = item.NominalValue,
                    ExpirationDate = item.ExpiredDate,
                    IsSimulation = request.IsSimulation ? "S":"N",
                    IssueDate = item.IssueDate,
                    ThirdCode = debtorDebtor.FirstOrDefault().Data.ThirdCode,
                    User = request.User,
                    DocumentTypeSii = item.DocumentTypeSii
                };
                docList.Add(newDoc);
            }
            #endregion

            #region Creación de objeto para crear la operación
            var createOperation = new CreateOperationDirect
            {
                BranchOfficeCode = codeBranchOffice,
                ClientCode = clientCode.Value,
                CurrencyCode = request.CurrencyType,
                Documents = docList,
                OperationDate = request.OperationDate,
                OperationValue = request.OperationValue,
                Origin = request.Origin,
                ProductCode = request.ProductCode,
                Rate = request.OperationRate,
                OperationType = xOperation,
                User = request.User,
                IsSimulation = request.IsSimulation ? "S": "N",
                ReturnInterest = request.ReturnInterest ? "S":"N",
                PaidType = request.PaidType,
                AffectedCharges = request.AffectedCharges,
                AppliedAgainst = request.AppliedAgainst,
                AppliedInFavor = request.AppliedInFavor,
                CollectionType = request.CollectionType,
                ComiCobType = request.ComiCobType,
                DiscountRate = request.DiscountRate,
                PromissoryNoteType = request.PromissoryNoteType,
                SectionCode = 1,
                WithCustody = 1,
                WithGuarantee = false,
                WithNotification = 1,
                WithResponsability = 1
            };
            #endregion

            #region obtiene el tipo de cambio y la tasa mora
            OperationResult<ChangeTypeAndDefaultRateResource> changeTypeAndDefaultRate = await _changeTypeRepository.GetChangeTypeAndDefaultRateAsync(request.CurrencyType, request.OperationDate);
            if (changeTypeAndDefaultRate.Data.ChangeType == null)
            {
                return OperationResult<long?>.BadRequestResult($"No se recuperaron los tipos de cambio ni la tasa mora para el tipo de moneda {request.CurrencyType} y la fecha {request.OperationDate:d}");
            }
            createOperation.ChangeType = changeTypeAndDefaultRate.Data.ChangeType.Value;
            createOperation.DefaultRate = changeTypeAndDefaultRate.Data.DefaultRate ?? 0;
            #endregion

            #region Crea la operación
            try
            {
                OperationResult<long?> result = await CreateOperationDirect(createOperation);

                return result;
            }
            catch (Exception e)
            {
                return OperationResult<long?>.BadRequestResult(e.Message);
            }
            #endregion
        }

        public async Task<OperationResult<long?>> CreateOperationDirect(CreateOperationDirect request)
        {
            string message = null;


            var parameters = new
            {
                fecha_operacion = request.OperationDate,
                tasa_operacion = request.Rate,
                porcentaje_descuento = request.DiscountRate,
                valor_nominal_operacion = request.FaceValueOperation,
                valor_futuro_operacion = request.FutureValueOperation,
                valor_presente_operacion = request.PresentValueOperation,
                interes_operacion = request.OperationInterest,
                aplicado_favor = request.AppliedInFavor,
                aplicado_contra = request.AppliedAgainst,
                saldo_deudor = request.DebitBalance,
                saldo_acreedor = request.CreditBalance,
                codigo_cliente = request.ClientCode,
                codigo_sucursal = request.BranchOfficeCode,
                estado_operacion = request.OperationState,
                tipo_cambio = request.ChangeType,
                codigo_moneda = request.CurrencyCode,
                tipo_pagare = request.PromissoryNoteType,
                cargos_afectos = request.AffectedCharges,
                cargos_exentos = request.ExemptCharges,
                codigo_producto = request.ProductCode,
                tipo_comi_cob = request.ComiCobType,
                monto_comi_cob = request.ComiCobAmount,
                factor_comi_cob = request.ComiCobFactor,
                minimo_comi_cob = request.ComiCobMin,
                maximo_comi_cob = request.ComiCobMax,
                con_garantia = request.WithGuarantee ? "S":"N",
                con_responsabilidad = request.WithResponsability,
                tipo_cobranza = request.CollectionType,
                con_notificacion = request.WithNotification,
                con_custodia = request.WithCustody,
                an_codigo_tramo = request.SectionCode,
                an_tasa_mora = request.DefaultRate,
                an_sucursal_custodia = request.BranchOfficeCode,
                an_ind_fijo_oper = request.FixedIndicatorOperation,
                an_ind_prim_oper = request.FirstIndicatorOperation,
                an_ind_notifica = request.IndicatorNotification,
                an_ind_cobranza = request.CollectionIndicator,
                an_tipo_pago = request.PaidType,
                av_usuario = request.User,
                av_origen = request.Origin,
                an_valor_total_mx = request.OperationValue,
                av_devolucion_intereses = request.ReturnInterest,
                av_tipo_operacion = request.OperationType,
                av_es_simulacion = request.IsSimulation
            };
            try
            {
                OperationResult<long?> execute = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    long? operationNumber = null;

                    connection.InfoMessage += OnInfoMessageEvent;
                    message = builder.ToString();
                    decimal resultado = await connection.QueryFirstOrDefaultAsync<decimal>("dba.pr_fin0116", p, commandType: CommandType.StoredProcedure);
                    connection.InfoMessage -= OnInfoMessageEvent;

                    message = builder.ToString();

                    if (resultado > 0)
                    {
                        foreach (CreateDocumentRequest item in request.Documents)
                        {
                            item.OperationNumber = resultado;
                            OperationBaseResult resultDoc = await _document.CreateDocumentAsync(item);

                            if (resultDoc.ErrorStatus != null)
                            {
                                return OperationResult<long?>.BadRequestResult(resultDoc.Message.ToString());
                            }
                        }
                    }

                    operationNumber = Convert.ToInt64(resultado);
                    telemetry.TrackTrace($"Se ejecuta el sp con el resultado: [{operationNumber}]");

                    var result = new OperationResult<long?>
                    {
                        Data = operationNumber
                    };
                    telemetry.TrackTrace($"El resultado de insertar la operación es: [{JsonConvert.SerializeObject(result)}]");
                    return result;
                },
                null,
                parameters);

                return execute;
            }
            catch (Exception e)
            {
                return OperationResult<long?>.BadRequestResult(e.Message);
            }
        }


        /// <inheritdoc cref="IOperationRepository.SimulationAndCreateOperation(SimulationAndCreateOperationResource)"/> 
        public async Task<OperationResult<long?>> SimulationAndCreateOperation(SimulationAndCreateOperationResource request)
        {
            //listado de deudores
            var debtorRUTs = request.Simulation.Documents.Select(x => x.DebtorRUT).Distinct();

            //obtiene deudores como deudor
            var debtorDebtor = new List<OperationResult<DebtorDataResource>>();
            foreach (var item in debtorRUTs)
            {
                telemetry.TrackTrace($"Se consulta la existencia del deudor {item}");
                var debtorDetail = await _debtor.GetDebtorDetailAsync(item);
                debtorDebtor.Add(debtorDetail);
            }
            if (debtorDebtor.Any(x => x.ErrorStatus != null))
            {
                telemetry.TrackTrace($"Al menos uno de los ruts de los documentos no se encuentra como deudor en CORE");
                return OperationResult<long?>.BadRequestResult("Al menos uno de los ruts de los documentos no se encuentra como deudor en CORE");
            }

            //obtiene deudores como cliente
            decimal debtorClient = 0;
            foreach (var item in debtorRUTs)
            {
                telemetry.TrackTrace($"Se consulta la existencia del deudor {item} como cliente");
                var data = await _client.GetClientByRUTAsync(item);
                if (data.ErrorStatus == null)
                {
                    debtorClient++;
                }
            }
            if (debtorClient != debtorRUTs.Count())
            {
                telemetry.TrackTrace($"Al menos uno de los ruts en los documentos no se encuentra como cliente en CORE");
                return OperationResult<long?>.BadRequestResult("Al menos uno de los ruts en los documentos no se encuentra como cliente en CORE");
            }

            //obtiene proveedor como cliente
            telemetry.TrackTrace($"Se consulta la existencia del cliente {JsonConvert.SerializeObject(request.Simulation.ClientRUT)}");
            var client = await _client.GetClientByRUTAsync(request.Simulation.ClientRUT);

            if(client.ErrorStatus != null)
            {
                //Crear cliente
                telemetry.TrackTrace($"Se crea el cliente con los siguientes parametros: {JsonConvert.SerializeObject(request.DataClient)}");
                var newClient = await _client.AddClientAsync(request.DataClient);
                if (newClient.Data == null)
                {
                    telemetry.TrackTrace($"No fue posible crear el usuario como nuevo cliente");
                    return OperationResult<long?>.BadRequestResult("No fue posible crear el nuevo cliente");
                }
            }



            //crea la simulación
            telemetry.TrackTrace($"Se crea la cotización con los siguientes parametros: {JsonConvert.SerializeObject(request.Simulation)}");
            OperationResult<long> simulationId = await _simulation.AddSimulation(request.Simulation);
            
            var insertOperation = new InsertOperation
            {
                BankCode = request.DataClient.BankCode,
                CurrentAccount = request.DataClient.BankAccount,
                QuotationID = Convert.ToInt32(simulationId.Data)
            };

            //Crea la operación
            telemetry.TrackTrace($"Se inserta la operación utilizando parametros: {JsonConvert.SerializeObject(insertOperation)}");
            OperationResult<long?> operation = await InsertOperation(insertOperation);

            telemetry.TrackTrace($"El resultado de insertar la operación es: [{JsonConvert.SerializeObject(operation)}]");

            return operation;
        }

        public async Task<OperationResult<OperationDocumentResource>> GetOperationDocumentByRUTAsync(string rut, OperationByRutRequest request)
        {
            var page = (request.Page == null) ? 0 : request.Page - 1;
            var pageSize = (request.PageSize == null) ? 10 : request.PageSize;
            var order = (string.IsNullOrEmpty(request.Order)) ? "ASC" : request.Order;
            var orderBy = (string.IsNullOrEmpty(request.OrderBy)) ? "numero_operacion" : request.OrderBy;
            OperationResult<OperationDocumentResource> output = new OperationResult<OperationDocumentResource>();

            (string query, object param) = OperationDocumentResource.Query_OperationDocumentByRUT(rut, page, pageSize, order, orderBy, request.OperationNumber, request.StartDate, request.EndDate);
            telemetry.TrackTrace($"Obteniendo los documentos con su operación: [{rut}]");

            output.Data = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    OperationDocumentResource result = new OperationDocumentResource();

                    SqlMapper.GridReader resultMultiple = await connection.QueryMultipleAsync(q, p);
                    result.Total = resultMultiple.ReadFirstOrDefault<int>();
                   
                    if (result.Total > 0)
                        result.Documents = resultMultiple.Read<OperationDocument>();

                    return result;
                }, query, param);

            return output;
        }

        public async Task<OperationResult<SettlementFormResource>> GetSettlementFormByOperationNumberAsync(long operationNumber)
        {
            OperationResult<SettlementFormResource> output = new OperationResult<SettlementFormResource>();

            telemetry.TrackTrace($"Obteniendo la planilla de liquidación con número de operación {operationNumber}");

            output.Data = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    SettlementFormResource result = new SettlementFormResource();

                    var reader = await connection.QueryMultipleAsync("dba.sp_c2_planilla_liquidacion", param: new { numero_operacion = operationNumber }, commandType: CommandType.StoredProcedure);
                    result = reader.ReadFirstOrDefault<SettlementFormResource>();

                    if (result != null)
                        result.Documents = reader.Read<SettlementFormDocument>();
                    else
                        result = new SettlementFormResource();

                    return result;
                }, default, default);

            return output;
        }

        public async Task<OperationResult<OperationDocumentByIDResource>> GetOperationDocumentByIDAsync(long operationNumber, int? page, int? pageSize)
        {
            page = (page == null) ? 0 : page - 1;
            pageSize = (pageSize == null) ? 10 : pageSize;
            OperationResult<OperationDocumentByIDResource> output = new OperationResult<OperationDocumentByIDResource>();

            (string query, object param) = OperationDocumentByIDResource.Query_OperationDocumentByID(operationNumber, page, pageSize);
            telemetry.TrackTrace($"Obteniendo los documentos por número de operación {operationNumber}");

            output.Data = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    OperationDocumentByIDResource result = new OperationDocumentByIDResource();

                    SqlMapper.GridReader resultMultiple = await connection.QueryMultipleAsync(q, p);
                    result.Total = resultMultiple.ReadFirstOrDefault<int>();

                    if (result.Total > 0)
                        result.Documents = resultMultiple.Read<OperationDocumentByID>();

                    return result;
                }, query, param);

            return output;
        }

        /// <inheritdoc/>
        public async Task<OperationResult<OperationStatusResource>> GetStatusAsync(long number)
        {
            (string query, object param) = OperationDetailResource.Query_GetOperationStatus(number);

            OperationStatusResource data = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    OperationStatusResource resultSingle = 
                        await connection.QueryFirstOrDefaultAsync<OperationStatusResource>(q, p);
                    return resultSingle;
                },
                query,
                param
            );

            if(data == null)
            {
                return OperationResult<OperationStatusResource>.NotFoundResult(number, $"No existe una operación con número {number}");
            }

            var result = new OperationResult<OperationStatusResource>(data);
            return result;
        }

        public async Task<OperationResult<OperationUserResource>> GetOperationByUserAsync(string userID)
        {
            OperationResult<OperationUserResource> output = new OperationResult<OperationUserResource>();
            
            (string query, object param) = OperationUserResource.Query_OperationByUser(userID);
            telemetry.TrackTrace($"Obteniendo las operaciones del usuario {userID}");

            output.Data = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    OperationUserResource result = new OperationUserResource();

                    SqlMapper.GridReader resultMultiple = await connection.QueryMultipleAsync(q, p);
                    result.Operations = resultMultiple.Read<OperationUser>();

                    if (result.Operations != null)
                    {
                        result.SummaryStates = resultMultiple.Read<SummaryStates>();

                        result.Total = resultMultiple.ReadFirstOrDefault<int>();
                    }

                    return result;
                }, query, param);

            return output;
        }

        public async Task<OperationResult<CreditDetailResource>> GetCreditDetailAsync(long number)
        {
            (string query, object param) = CreditDetailResource.Query_GetCreditDetail(number);

            CreditDetailResource data = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    CreditDetailResource resultSingle =
                        await connection.QueryFirstOrDefaultAsync<CreditDetailResource>(q, p);
                    return resultSingle;
                },
                query,
                param
            );

            var result = new OperationResult<CreditDetailResource>(data);
            return result;
        }

        void OnInfoMessageEvent(object obj, SqlInfoMessageEventArgs args)
        {
            builder.AppendLine(args.Message);
        }

        /// <inheritdoc cref="Interfaces.GetOperationByDocumentAsync(OperationByDocumentRequest)"/>
        public async Task<OperationResult<long?>> GetOperationByDocumentAsync(OperationByDocumentRequest request)
        {
            (string query, Query_OperationByDocumentModel parameters) = Query_OperationByDocument(request.DocumentNumber, RutHelper.FormatRut(request.ClientRut), RutHelper.FormatRut(request.DebtorRut), request.ProductCode);

            OperationByDocumentResource data = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    OperationByDocumentResource resultSingle =
                        await connection.QueryFirstOrDefaultAsync<OperationByDocumentResource>(q, p);
                    return resultSingle;
                },
                query,
                parameters
            );

            if(data == null)
            {
                return OperationResult<long?>.NotFoundResult(request, "Unable to find an operation for the document.");
            }

            return new OperationResult<long?>(data.OperationNumber);
        }
    }
}