using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PeopleAreUs.Console.Core;
using PeopleAreUs.Console.Util;

namespace PeopleAreUs.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var services = new ServiceCollection();
            var serviceProvider = Bootstrapper.GetServiceProvider(services);

            var client = serviceProvider.GetRequiredService<IPeopleAreUsHttpClient>();

            var peopleData = await client.GetPeopleAsync();
        }
    }
}
