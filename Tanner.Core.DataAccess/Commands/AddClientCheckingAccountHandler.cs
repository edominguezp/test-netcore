using Microsoft.ApplicationInsights;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Models;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;
using Tanner.RelationalDataAccess;

namespace Tanner.Core.DataAccess.Commands
{
    public class AddCustomerCheckingAccountHandler : TannerCommandHandler<OperationResult<CurrentAccountResource>, AddClientCheckingAccountCommand>
    {
        private readonly TelemetryClient telemetry;
        private string guidId;

        public AddCustomerCheckingAccountHandler(ICoreRepository repository, TelemetryClient _telemetry) : base(repository)
        {
            telemetry = _telemetry;
        }

        public override async Task<OperationResult<CurrentAccountResource>> ActionAsync(AddClientCheckingAccountCommand command)
        {

            string Rut = command.ClientRut.Replace("-", "");
            char zero = '0';

            Rut = Rut.PadLeft(10, zero);


            Person person = await Repository.FirstOrDefaultAsync<Person>(t => t.RUT.Equals(Rut));
            if (person == null)
            {
                return OperationResult<CurrentAccountResource>.NotFoundResult(command.ClientRut, $"No se encontró un cliente con el RUT: {command.ClientRut}");
            }

            Account account = await Repository.FirstOrDefaultAsync<Account>(t => t.ID == command.BankCode && t.CurrentAcount == command.BankCheckingAccount && t.CodePerson == person.ID);

            CurrentAccount currentAccount;

            if (account != null)
            {
                return OperationResult<CurrentAccountResource>.ConflictResult(command, $"Ya existe la cuenta número { command.BankCheckingAccount} para el cliente RUT: {command.ClientRut}");
            }
            
            currentAccount = await Repository.FirstOrDefaultAsync<CurrentAccount>(t => t.ID == command.BankCode && t.CurrentAcount == command.BankCheckingAccount);
            if (currentAccount == null)
            {
                currentAccount = new CurrentAccount
                {
                    ID = command.BankCode,
                    CurrentAcount = command.BankCheckingAccount,

                };

                guidId = Guid.NewGuid().ToString();
                string payload = JsonConvert.SerializeObject(command);
                telemetry.TrackTrace($"Payload Cuenta corriente [{currentAccount}]", new Dictionary<string, string> { { "guidId", guidId }, { "payload", payload } });

                //inserto registro en la tabla fin51
                Repository.Add(currentAccount);
                await Repository.SaveChangesAsync();
                telemetry.TrackTrace($"Se ha agregado el registro a la base de datos  fin 51[{guidId}]");

            }

            ////inserto registro en la tabla cuenta_entidad
            account = new Account
            {
                ID = command.BankCode,
                CodePerson = person.ID,
                CurrentAcount = command.BankCheckingAccount
            };
            string payload2 = JsonConvert.SerializeObject(command);
            telemetry.TrackTrace($"Payload Account [{account}]", new Dictionary<string, string> { { "guidId", guidId }, { "payload", payload2 } });

            Repository.Add(account);
            await Repository.SaveChangesAsync();
            telemetry.TrackTrace($"Se ha agregado el registro a la base de datos cuenta entidad [{guidId}]");


            var data = (CurrentAccountResource)currentAccount;
            telemetry.TrackTrace($"Se agregó el siguiente registro [{data}]");
            return new OperationResult<CurrentAccountResource>(data);
        }
    }
}
