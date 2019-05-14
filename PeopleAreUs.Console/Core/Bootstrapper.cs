using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PeopleAreUs.Console.Application;
using PeopleAreUs.Console.Application.ViewModels;
using PeopleAreUs.Console.Mappers;
using PeopleAreUs.Console.Output;
using PeopleAreUs.Core;
using PeopleAreUs.DTO;
using PeopleAreUs.Infrastructure;
using PeopleAreUs.Services;
using PeopleAreUs.Services.Mappers;
using PeopleAreUs.Services.Responses;
using PeopleAreUs.Services.Specifications;

namespace PeopleAreUs.Console.Core
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
            // Enable logging
            //
            services.AddLogging();
            //
            // Configuration
            //
            //var configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            //services.Configure<PeopleAreUsApiConfig>(configuration.GetSection("PeopleAreUsApi"));
            //services.AddSingleton(provider =>
            //{
            //    var config = provider.GetRequiredService<IOptions<PeopleAreUsApiConfig>>().Value;
            //    return config;
            //});
            //
            // Services
            //
            services.UseServices();
            //
            // Managers
            //
            services.AddSingleton<IPeopleManager, PeopleManager>();
            //
            // Mappers
            //
            services.AddSingleton<IMapper<GetPetOwnersResponse, PetsByOwnerGenderViewModel>, GetPetOwnersResponseToPetsByOwnerGenderViewModelMapper>();
            //
            // Renderers
            //
            services.AddSingleton<IRenderer<PetsByOwnerGenderViewModel>, PetsByOwnerGenderRenderer>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}