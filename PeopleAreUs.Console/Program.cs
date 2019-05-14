using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PeopleAreUs.Console.Application;
using PeopleAreUs.Console.Application.Requests;
using PeopleAreUs.Console.Core;
using PeopleAreUs.Domain.Models;

namespace PeopleAreUs.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();

            System.Console.WriteLine("PRESS [ENTER] KEY TO EXIT THE CONSOLE");
            System.Console.ReadLine();
        }

        private static async Task MainAsync()
        {
            var serviceProvider = Bootstrapper.GetServiceProvider(new ServiceCollection());
            var manager = serviceProvider.GetRequiredService<IPeopleManager>();
            await manager.ShowPetsAsync(new ShowPetsRequest(PetType.Cat));

        }
    }
}