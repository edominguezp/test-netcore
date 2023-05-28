using MediatR;

using System.Collections.Generic;
using Tanner.Core.DataAccess.Commands;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Results;
using Tanner.RelationalDataAccess;
using Tanner.UnitTest.Base;
using Xunit.Abstractions;

namespace Tanner.Core.DataAccess.Test.Unit
{
    /// <summary>
    /// Unit test base
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseTest<T> : UnitTestBase<T>
    {
        private static string CORE_CONNECTION_STRING = 
            "Server=192.168.118.189;Initial Catalog=CORE2;Persist Security Info=False;User ID=core2_api;Password=Sqlc0r324p1;MultipleActiveResultSets=False;Connection Timeout=180;";
        private static string CORE_KEY = nameof(CORE_KEY);
            
        private static string FILE_CONNECTION_STRING = 
            "Server=192.168.118.189;Initial Catalog=CORE2_ARCHIVOS;Persist Security Info=False;User ID=core2_api;Password=Sqlc0r324p1;MultipleActiveResultSets=False;Connection Timeout=180;";
        private static string FILE_KEY = nameof(FILE_KEY);

        private static string INTELICOM_CONNECTION_STRING = 
            "Server=192.168.118.189;Initial Catalog=DM_Intelicom;Persist Security Info=False;User ID=core2_api;Password=Sqlc0r324p1;MultipleActiveResultSets=False;Connection Timeout=180;";
        private static string INTELICOM_KEY = nameof(INTELICOM_KEY);

        protected readonly IMediator Mediator;

        public BaseTest(ITestOutputHelper output) : base(output)
        {
            Mediator = (IMediator)Provider.GetService(typeof(IMediator));
        }

        protected override void ConfigServices()
        {
            Services.AddTannerDataAccess(opt => {
                opt.DataBases = new Dictionary<string, string> {
                        { CORE_KEY, CORE_CONNECTION_STRING },
                        { FILE_KEY, FILE_CONNECTION_STRING },
                        { INTELICOM_KEY, INTELICOM_CONNECTION_STRING }
                    };
            })
                .UseContext<CoreContext>(CORE_KEY)
                .UseContext<CoreFileContext>(FILE_KEY)
                .UseContext<IntelicomContext>(INTELICOM_KEY)
                .AddCommand<AddAddressCommand, OperationResult<AddressResource>>()
                .AddCommand<AddClientCheckingAccountCommand, OperationResult<CurrentAccountResource>>()
                .AddCommand<DeleteDocumentQuotationCommand, OperationBaseResult>()
                .AddCommand<InsertFileOperationCommand, OperationResult<FileResource>>()
                .AddCommand<UpdateAddressCommand, OperationBaseResult>()
            ;
        }

        protected override void Configure()
        {
        }
    }
}
