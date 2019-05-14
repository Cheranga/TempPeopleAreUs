using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PeopleAreUs.Core;
using PeopleAreUs.DTO;

namespace PeopleAreUs.Infrastructure
{
    internal class PeopleAreUsHttpClient : IPeopleAreUsHttpClient
    {
        private readonly HttpClient _client;
        private readonly PeopleAreUsApiConfig _config;
        private readonly IPeopleDataConverter _converter;
        private readonly ILogger<PeopleAreUsHttpClient> _logger;

        public PeopleAreUsHttpClient(HttpClient client, PeopleAreUsApiConfig config, IPeopleDataConverter converter, ILogger<PeopleAreUsHttpClient> logger)
        {
            _client = client;
            _config = config;
            _converter = converter;
            _logger = logger;
        }

        public async Task<OperationResult<List<Person>>> GetPeopleAsync()
        {
            var httpResponse = await _client.GetAsync(_config.Url).ConfigureAwait(false);
            if (!httpResponse.IsSuccessStatusCode)
            {
                _logger.LogError($"Cannot retrieve people data from the external API: {httpResponse.ReasonPhrase}");
                return OperationResult<List<Person>>.Failure("Cannot retrieve people data from the API");
            }

            var content = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (string.IsNullOrEmpty(content))
            {
                _logger.LogError("The API returned empty content");
                return OperationResult<List<Person>>.Failure("No people data to process");
            }

            var operation = _converter.Convert(content);
            if (operation.Status)
            {
                return OperationResult<List<Person>>.Success(operation.Data);
            }

            return OperationResult<List<Person>>.Failure(operation.Message);
        }
    }
}