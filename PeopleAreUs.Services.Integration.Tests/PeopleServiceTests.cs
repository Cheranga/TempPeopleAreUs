using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PeopleAreUs.Console;
using PeopleAreUs.Console.Requests;
using PeopleAreUs.Domain.Models;
using PeopleAreUs.Services.Requests;
using Xunit;

namespace PeopleAreUs.Services.Integration.Tests
{
    public class PeopleServiceTests
    {
        [Fact]
        public async Task Given_A_PetType_All_People_Who_Owns_That_PetType_Must_Be_Returned()
        {
            
            var serviceProvider = Console.Bootstrapper.GetServiceProvider(new ServiceCollection());
            var peopleService = serviceProvider.GetRequiredService<IPeopleService>();

            var operation = await peopleService.GetPetOwnersAsync(new GetPetOwnersRequest(PetType.Cat));

            Assert.True(operation.Status);

            Assert.NotNull(operation.Data?.People);
            Assert.NotEmpty(operation.Data.People);
        }
    }
}
