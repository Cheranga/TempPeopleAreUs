using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace PeopleAreUs.Infrastructure
{
    public static class Bootstrapper
    {
        public static void UseInfrastructure(this IServiceCollection services, PeopleAreUsApiConfig config)
        {
            if (services == null || config == null)
            {
                throw new Exception("Insufficient configuration data");
            }

            services.AddSingleton(config);
            
            services.AddHttpClient<IPeopleAreUsHttpClient, PeopleAreUsHttpClient>();
            services.AddSingleton<IPeopleDataConverter, JsonPeopleDataConverter>();
        }
    }
}
