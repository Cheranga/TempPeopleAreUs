using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PeopleAreUs.Console.Application;
using PeopleAreUs.Console.Application.ViewModels;
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

            System.Console.ReadLine();
        }

        private static async Task MainAsync()
        {
            //var services = new ServiceCollection();
            //var serviceProvider = Bootstrapper.GetServiceProvider(services);

            //var client = serviceProvider.GetRequiredService<IPeopleAreUsHttpClient>();

            //var operation = await client.GetPeopleAsync();

            //if (operation.Status)
            //{
            //    var mapper = new DtoPersonToBusinessPerson(new DtoPetToBusinessPet());

            //    var mappedPeople = operation.Data.Select(x => mapper.Map(x)).ToList();

            //    var specification = new PetTypeSpecification();

            //    var catsOnlyOwners = mappedPeople.SelectMany(x => x.Pets).Where(x => specification.IsSatisfiedBy(x, PetType.Cat)).ToList();
            //}

            //var renderer = new PetsByOwnerGenderRenderer();
            //await renderer.RenderAsync(new PetsByOwnerGenderViewModel
            //{
            //    PetsMappedByOwnersGender = new Dictionary<Gender, List<string>>
            //    {
            //        {Gender.Male, new List<string> {"a", "b", "c"}},
            //        {Gender.Female, new List<string> {"p", "q", "r"}},
            //    }
            //});

            
        }
    }
}