﻿using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PeopleAreUs.Console.Application;
using PeopleAreUs.Console.Application.ViewModels;
using PeopleAreUs.Console.DTO.External;
using PeopleAreUs.Console.DTO.Internal;
using PeopleAreUs.Console.Mappers;
using PeopleAreUs.Console.Output;
using PeopleAreUs.Console.Services;
using PeopleAreUs.Console.Specifications;
using PeopleAreUs.Console.Util;

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
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            services.Configure<PeopleAreUsApiConfig>(configuration.GetSection("PeopleAreUsApi"));
            services.AddSingleton(provider =>
            {
                var config = provider.GetRequiredService<IOptions<PeopleAreUsApiConfig>>().Value;
                return config;
            });
            //
            // Services
            //
            services.AddSingleton<IPeopleService, PeopleService>();
            //
            // Managers
            //
            services.AddSingleton<IPeopleManager, PeopleManager>();
            //
            // Mappers
            //
            services.AddSingleton<IMapper<Person, Business.Models.Person>, DtoPersonToBusinessPerson>();
            services.AddSingleton<IMapper<Pet, Business.Models.Pet>, DtoPetToBusinessPet>();
            services.AddSingleton<IMapper<GetPetOwnersResponse, PetsByOwnerGenderViewModel>, GetPetOwnersResponseToPetsByOwnerGenderViewModelMapper>();
            //
            // Specifications
            //
            services.AddSingleton<IPetTypeSpecification, PetTypeSpecification>();
            //
            // Renderers
            //
            services.AddSingleton<IRenderer<PetsByOwnerGenderViewModel>, PetsByOwnerGenderRenderer>();
            //
            // Http Client
            //
            services.AddHttpClient<IPeopleAreUsHttpClient, PeopleAreUsHttpClient>();
            //
            // Data converters
            //
            services.AddSingleton<IPeopleDataConverter, JsonPeopleDataConverter>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}