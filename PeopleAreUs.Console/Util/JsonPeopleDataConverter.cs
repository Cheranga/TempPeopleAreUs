using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PeopleAreUs.Console.Core;
using PeopleAreUs.Console.DTO.External;

namespace PeopleAreUs.Console.Util
{
    public class JsonPeopleDataConverter : IPeopleDataConverter
    {
        private readonly ILogger<JsonPeopleDataConverter> _logger;

        public JsonPeopleDataConverter(ILogger<JsonPeopleDataConverter> logger)
        {
            _logger = logger;
        }

        public ResultStatus<List<Person>> Convert(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                _logger.LogError("Empty content. Cannot deserialize data");
                return ResultStatus<List<Person>>.Failure("Empty content. Cannot deserialize data");
            }

            try
            {
                var people = JsonConvert.DeserializeObject<List<Person>>(content);

                return ResultStatus<List<Person>>.Success(people);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Cannot deserialize data: {exception}");
                return ResultStatus<List<Person>>.Failure($"Invalid content data: {content}");
            }
        }
    }
}