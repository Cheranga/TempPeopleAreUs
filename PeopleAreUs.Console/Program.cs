using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PeopleAreUs.Console.Business.Models;
using PeopleAreUs.Console.Core;
using PeopleAreUs.Console.Mappers;
using PeopleAreUs.Console.Specifications;
using PeopleAreUs.Console.Util;

namespace PeopleAreUs.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            var services = new ServiceCollection();
            var serviceProvider = Bootstrapper.GetServiceProvider(services);

            var client = serviceProvider.GetRequiredService<IPeopleAreUsHttpClient>();

            var operation = await client.GetPeopleAsync();

            if (operation.Status)
            {
                var mapper = new DtoPersonToBusinessPerson(new DtoPetToBusinessPet());

                var mappedPeople = operation.Data.Select(x => mapper.Map(x)).ToList();

                var specification = new PetTypeSpecification();

                var catsOnlyOwners = mappedPeople.SelectMany(x => x.Pets).Where(x => specification.IsSatisfiedBy(x, PetType.Cat)).ToList();
            }
        }
    }
}