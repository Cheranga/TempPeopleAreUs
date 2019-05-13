﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
            // Http Client
            //
            services.AddHttpClient<IPeopleAreUsHttpClient, PeopleAreUsHttpClient>();

            services.AddSingleton<IPeopleDataConverter, JsonPeopleDataConverter>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
