using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PeopleAreUs.Console.Mappers;
using PeopleAreUs.Console.Output;
using PeopleAreUs.Console.ViewModels;
using PeopleAreUs.Core;
using PeopleAreUs.Services;
using PeopleAreUs.Services.Responses;

namespace PeopleAreUs.Console
{
    public class Bootstrapper
    {
        public static IServiceProvider GetServiceProvider(IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            //
            // Enable logging and configure console logging
            //
            services.AddLogging(builder => builder.AddConsole());
            //
            // Managers
            //
            services.AddSingleton<IPeopleMediator, PeopleMediator>();
            //
            // Mappers
            //
            services.AddSingleton<IMapper<GetPetOwnersResponse, PetsByOwnerGenderViewModel>, GetPetOwnersResponseToPetsByOwnerGenderViewModelMapper>();
            //
            // Renderers
            //
            services.AddSingleton<IRenderer<PetsByOwnerGenderViewModel>, PetsByOwnerGenderRenderer>();
            //
            // Register services
            //
            services.UseServices();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}