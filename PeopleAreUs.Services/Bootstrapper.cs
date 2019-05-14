using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PeopleAreUs.Core;
using PeopleAreUs.DTO;
using PeopleAreUs.Infrastructure;
using PeopleAreUs.Services.Mappers;
using PeopleAreUs.Services.Specifications;

namespace PeopleAreUs.Services
{
    public static class Bootstrapper
    {
        public static void UseServices(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            RegisterMappers(services);
            RegisterSpecifications(services);
            RegisterServices(services);

            UseInfrastructure(services);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IPeopleService, PeopleService>();
        }

        private static void RegisterMappers(IServiceCollection services)
        {
            services.AddSingleton<IMapper<Person, Domain.Models.Person>, DtoPersonToBusinessPerson>();
            services.AddSingleton<IMapper<Pet, Domain.Models.Pet>, DtoPetToBusinessPet>();
        }

        private static void RegisterSpecifications(IServiceCollection services)
        {
            services.AddSingleton<IPetTypeSpecification, PetTypeSpecification>();
        }

        private static void UseInfrastructure(IServiceCollection services)
        {
            //
            // Get the configuration
            //
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("servicesettings.json")
                .Build();

            var config = new PeopleAreUsApiConfig();
            configuration.GetSection("PeopleAreUsApi").Bind(config);

            services.UseInfrastructure(config);
        }
    }
}
