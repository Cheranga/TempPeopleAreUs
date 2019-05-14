using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PeopleAreUs.Console.Application;
using PeopleAreUs.Console.Application.Requests;
using PeopleAreUs.Domain.Models;
using PeopleAreUs.Services;
using PeopleAreUs.Services.Requests;
using Xunit;
using Bootstrapper = PeopleAreUs.Console.Core.Bootstrapper;

namespace PeopleAreUs.Tests.Integration
{
    public class PeopleServiceTests
    {
        [Fact]
        public async Task Given_A_PetType_All_People_Who_Owns_That_PetType_Must_Be_Returned()
        {
            var serviceProvider = Bootstrapper.GetServiceProvider(new ServiceCollection());
            var peopleService = serviceProvider.GetRequiredService<IPeopleService>();

            var operation = await peopleService.GetPetOwnersAsync(new GetPetOwnersRequest(PetType.Cat));

            Assert.True(operation.Status);

            Assert.NotNull(operation.Data?.People);
            Assert.NotEmpty(operation.Data.People);
        }
    }

    public class PeopleManagerTests
    {
        [Fact]
        public async Task Test()
        {
            var serviceProvider = Bootstrapper.GetServiceProvider(new ServiceCollection());
            var manager = serviceProvider.GetRequiredService<IPeopleManager>();

            await manager.ShowPetsAsync(new ShowPetsRequest(PetType.Cat));
        }
    }
}
