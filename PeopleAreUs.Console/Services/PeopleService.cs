using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PeopleAreUs.Console.Core;
using PeopleAreUs.Console.DTO.External;
using PeopleAreUs.Console.DTO.Internal;
using PeopleAreUs.Console.Mappers;
using PeopleAreUs.Console.Specifications;
using PeopleAreUs.Console.Util;

namespace PeopleAreUs.Console.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly IPeopleAreUsHttpClient _client;
        private readonly IPetTypeSpecification _petTypeSpecification;
        private readonly IMapper<Person, Business.Models.Person> _mapper;
        private readonly ILogger<PeopleService> _logger;

        public PeopleService(IPeopleAreUsHttpClient client, IPetTypeSpecification petTypeSpecification, IMapper<Person, Business.Models.Person> mapper,
            ILogger<PeopleService> logger)
        {
            _client = client;
            _petTypeSpecification = petTypeSpecification;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResultStatus<GetPetOwnersResponse>> GetPetOwnersAsync(GetPetOwnersRequest request)
        {
            try
            {
                var getPeopleOperation = await _client.GetPeopleAsync().ConfigureAwait(false);
                if (!getPeopleOperation.Status)
                {
                    _logger.LogError("Cannot retrieve people from the API");
                    return ResultStatus<GetPetOwnersResponse>.Failure("Cannot retrieve people");
                }

                var people = getPeopleOperation.Data.Select(x=>_mapper.Map(x)).ToList();

                var selectedPeople = new List<Business.Models.Person>();

                foreach (var person in people)
                {
                    var pets = person.Pets;
                    if (pets == null || !pets.Any())
                    {
                        continue;
                    }

                    var selectedPets = person.Pets.Where(x => _petTypeSpecification.IsSatisfiedBy(x, request.Type)).ToList();
                    if (!selectedPets.Any())
                    {
                        continue;
                    }

                    selectedPeople.Add(new Business.Models.Person
                    {
                        Name = person.Name,
                        Age = person.Age,
                        Gender = person.Gender,
                        Pets = selectedPets
                    });
                }

                return ResultStatus<GetPetOwnersResponse>.Success(new GetPetOwnersResponse(selectedPeople));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error when retrieving people: {exception}");
            }

            return ResultStatus<GetPetOwnersResponse>.Failure("Cannot retrieve people");
        }
    }
}
