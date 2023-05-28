using Moq;
using System.Threading.Tasks;
using Tanner.Core.DataAccess.Commands;
using Tanner.Core.DataAccess.ModelResources;
using Tanner.Core.DataAccess.Models;
using Tanner.Core.DataAccess.Repositories.Interfaces;
using Tanner.Core.DataAccess.Results;
using Xunit;
using Xunit.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tanner.Core.DataAccess.Test.Unit.Commands
{
    public class AddAddressHandlerTest : BaseTest<AddAddressHandler>
    {
        private static readonly string RUT_NOTFOUND = "267300810";
        private static readonly string RUT_FOUND = "0";

        public AddAddressHandlerTest(ITestOutputHelper output) : base(output)
        {
        }

        protected override void ConfigServices()
        {
            var coreRepositoryMock = new Mock<ICoreRepository>();

            string rutNotFound = RUT_NOTFOUND.Replace("-", "");
            rutNotFound = rutNotFound.PadLeft(10, '0');

            string rut = RUT_FOUND.Replace("-", "");
            rut = RUT_FOUND.PadLeft(10, '0');

            var persons = new List<Person> { 
                new Person{ 
                    RUT = rut
                }
            };


            coreRepositoryMock
                .Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Person, bool>>>()))
                .Returns((Expression<Func<Person, bool>> arg, Expression<Func<Person, object>>[] includeProperties) => Task.FromResult(persons.FirstOrDefault(arg.Compile())));



            ICoreRepository coreRepository = coreRepositoryMock.Object;
            Services.AddScoped(t => coreRepository);
            base.ConfigServices();
        }

        [Fact]
        public async Task ActionAsync_PersonIsNull_ReturnPersonNotFound()
        {
            // arrange
            var command = new AddAddressCommand { 
                Address = "",
                CommuneID= 3305,
                Number= "",
                Phone= "",
                RUT= RUT_NOTFOUND
            };

            // action
            OperationResult<AddressResource> result = await Mediator.Send(command);

            //assert
            OperationResult<AddressResource> expect = OperationResult<AddressResource>.NotFoundResult(command.RUT, $"No existe una persona con el RUT:{command.RUT}");
            
            Assert.Equal(expect.ErrorStatus, result.ErrorStatus);
            Assert.Equal(expect.Resource, result.Resource);
        }
    }
}
