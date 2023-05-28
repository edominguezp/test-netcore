
using Dapper;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;
using Tanner.RelationalDataAccess;
using Microsoft.ApplicationInsights;
using System;
using Newtonsoft.Json;
using Tanner.Core.DataAccess.Models;

namespace Tanner.Core.DataAccess.Repositories
{
    /// <inheritdoc cref="IClientRepository"/>
    public class ClientRepository : CoreRepository, IClientRepository
    {
        private readonly TelemetryClient telemetry;
        private string guidId;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx">DB context</param>
        public ClientRepository(CoreContext ctx, TelemetryClient _telemetry) : base(ctx)
        {
            telemetry = _telemetry;
        }

        /// <inheritdoc cref="IClientRepository.GetClientByRUTAsync(string)"/>
        public async Task<OperationResult<ClientResource>> GetClientByRUTAsync(string rut)
        {
            (string query, object param) = ClientResource.Query_ClientByRUT(rut);
            OperationResult<ClientResource>  execute = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    SqlMapper.GridReader resultMultiple = await connection.QueryMultipleAsync(q, p);
                    ClientResource client = resultMultiple.ReadFirstOrDefault<ClientResource>();

                    if (client == null)
                    {
                        telemetry.TrackTrace($"No existe una persona con el RUT [{rut}]");
                        return OperationResult<ClientResource>.NotFoundResult(rut);
                    }

                    client.Address = resultMultiple.Read<ClientAddressResource>();
                    client.Contact = resultMultiple.Read<ClientContactResource>();

                    var result = new OperationResult<ClientResource>(client);
                    telemetry.TrackTrace($"Resultado:  [{result}]");
                    return result;
                },
                query,
                param);

            return execute;
        }


        /// <inheritdoc cref="IClientRepository.GetFinancialDetailRUTAsync(string)"/>
        public async Task<OperationResult<FinancialDetailResource>> GetFinancialDetailRUTAsync(string rutClient)
        {
            (string query, object param) = FinancialDetailResource.Query_FinancialDetailByRUT(rutClient);
            OperationResult<FinancialDetailResource> execute = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    SqlMapper.GridReader resultMultiple = await connection.QueryMultipleAsync(q, p);
                    FinancialDetailResource result = resultMultiple.ReadFirstOrDefault<FinancialDetailResource>();
                    if(result != null)
                    {
                        result.Indicators = resultMultiple.Read<IndicatorsResource>();
                        result.IndicatorsPercentageResource = resultMultiple.ReadFirstOrDefault<IndicatorsPercentageResource>();
                        result.DataDebtorResource = resultMultiple.Read<DebtorResource>();
                    }
                    telemetry.TrackTrace($"Datos financieros: [{result}]");
                    return new OperationResult<FinancialDetailResource>(result);
                },
                query,
                param);
            return execute;
        }

        public async Task<OperationResult<ClientBaseResource>> AddClientAsync(AddClientResource client)
        {
            if (client.ClientStatus != 0 && client.ClientStatus != 1 && client.ClientStatus != 2)
            {
                telemetry.TrackTrace($"El estado del cliente no existe: [{client.ClientStatus}]");
                return OperationResult<ClientBaseResource>.BadRequestResult($"El estado del cliente no existe: { client.ClientStatus}");
            }

            var parameters = new
            {
                ss_rut_cliente = client.RUT,
                ss_nombre_cliente = client.Name,
                ss_email = client.Email,
                ll_cod_banco = client.BankCode,
                ss_cuenta_banco = client.BankAccount,
                ss_direccion = client.Address,
                ll_tipo_persona_arg = client.PersonType,
                ss_paterno = client.LastName,
                ss_materno = client.MotherLastName,
                ss_primer_nombre = client.FirstName,
                ss_segundo_nombre = client.SecondName,
                ss_razon_social = client.BusinessName,
                ll_actividad_economica_arg = client.EconomicActivity,
                ll_estado_cliente_arg = client.ClientStatus,
                ll_sucursal_arg = client.BranchOfficeID,
                ll_ejecutivo_arg = client.ExecutiveID,
                ll_clasificacion_clie_arg = client.ClientClassificationID,
                banksbif = client.IsBankSbif
            };
            guidId = Guid.NewGuid().ToString();
            string payload = JsonConvert.SerializeObject(parameters);
            telemetry.TrackTrace($"Payload Client [{client}]", new Dictionary<string, string> { { "guidId", guidId }, { "payload", payload } });

            OperationResult<ClientBaseResource> execute = await ExecuteAsync(
                    async (SqlConnection connection, string q, object p) =>
                    {
                        ClientBaseResource clientAdd =
                        await connection.QueryFirstOrDefaultAsync<ClientBaseResource>("dbo.SP_TRX_RP_crea_clientes", p, commandType: CommandType.StoredProcedure);

                        if (clientAdd == null)
                        {
                            telemetry.TrackTrace($"Existe un cliente con el RUT: [{client.RUT}]");
                            return OperationResult<ClientBaseResource>.ConflictResult(client.RUT, $"Existe un cliente con el RUT {client.RUT}");
                        }

                        var result = new OperationResult<ClientBaseResource>(clientAdd);
                        telemetry.TrackTrace($"Se agregó el cliente: [{result}]");
                        return result;
                    },
                    null,
                    parameters);

            return execute;
        }

        /// <inheritdoc cref="IClientRepository.GetAddressDetailByClient(string)"/>
        public async Task<OperationCollectionResult<AddressResource>> GetAddressDetailByClient(string rut)
        {
            (string query, object param) = AddressResource.Query_AddressDetailByRUT(rut);

            IEnumerable<AddressResource> result = await ExecuteAsync(
                        async (SqlConnection connection, string q, object p) =>
                        {
                            IEnumerable<AddressResource> elements = await connection.QueryAsync<AddressResource>(q, p);
                            return elements;
                        },
                        query,
                        param);

            var execute = new OperationCollectionResult<AddressResource>
            {
                DataCollection = result,
                Total = result.Count()
            };

            guidId = Guid.NewGuid().ToString();
            string payloadAddress = JsonConvert.SerializeObject(execute);
            telemetry.TrackTrace($"Payload Address [{execute.DataCollection}]", new Dictionary<string, string> { { "guidId", guidId }, { "payload", payloadAddress } });
            
            return execute;
        }

        /// <inheritdoc cref="IClientRepository.GetBankAccountDetailsByClient(string)"/>
        public async Task<OperationCollectionResult<BankAccountClientResource>> GetBankAccountDetailsByClient(string rut)
        {
            (string query, object param) = BankAccountClientResource.Query_BankAccountDetailByRUT(rut);

            IEnumerable<BankAccountClientResource> result = await ExecuteAsync(
                        async (SqlConnection connection, string q, object p) =>
                        {
                            IEnumerable<BankAccountClientResource> bankAccounts = await connection.QueryAsync<BankAccountClientResource>(q, p);
                            return bankAccounts;
                        },
                        query,
                        param);

            var execute = new OperationCollectionResult<BankAccountClientResource>
            {
                DataCollection = result,
                Total = result.Count()
            };

            guidId = Guid.NewGuid().ToString();
            string payloadBank = JsonConvert.SerializeObject(execute);
            telemetry.TrackTrace($"Payload Bank [{execute.DataCollection}]", new Dictionary<string, string> { { "guidId", guidId }, { "payload", payloadBank } });

            return execute;
        }

        /// <inheritdoc cref="IClientRepository.GetMainContactByClient(string)"/>
        public async Task<OperationResult<MainContactResource>> GetMainContactByClient(string rut)
        {
            OperationResult<MainContactResource> result = new OperationResult<MainContactResource>();

            (string query, object param) = MainContactResource.Query_MainContactByRUT(rut);

            result.Data = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    MainContactResource mainContact = await connection.QueryFirstOrDefaultAsync<MainContactResource>(q, p);
                    return mainContact;
                }, query, param);

            if (result.Data == null)
                result.Data = new MainContactResource();

            return result;
        }

        /// <inheritdoc cref="IClientRepository.GetClientCreditLine(string)"/>
        public async Task<OperationResult<ClientCreditLine>> GetClientCreditLine(string rut)
        {
            OperationResult<ClientCreditLine> result = new OperationResult<ClientCreditLine>();

            (string query, object param) = ClientCreditLine.Query_ClientCreditLineByRUT(rut);

            result.Data = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    ClientCreditLine mainContact = await connection.QueryFirstOrDefaultAsync<ClientCreditLine>(q, p);
                    return mainContact;
                }, query, param);

            return result;
        }

        /// <inheritdoc cref="IClientRepository.GetCreditLineById(string)"/>
        public async Task<bool> GetCreditLineById(int lineId)
        {
            bool output = false;

            (string query, object param) = ClientCreditLine.Query_CreditLineByID(lineId);

            int resultLine = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    int result = await connection.QueryFirstOrDefaultAsync<int>(q, p);
                    return result;
                }, query, param);

            if (resultLine > 0)
                output = true;

            return output;
        }

        /// <inheritdoc cref="IClientRepository.GetClientCodeByRut(string)"/>
        public async Task<int?> GetClientCodeByRut(string clientRut)
        {

            (string query, object param) = ClientCodeResource.Query_GetClientCodeByRut(clientRut);

            int? resultCode = await ExecuteAsync(
                async (SqlConnection connection, string q, object p) =>
                {
                    int? result = await connection.QueryFirstOrDefaultAsync<int?>(q, p);
                    return result;
                }, query, param);

            return resultCode;
        }
    }
}
