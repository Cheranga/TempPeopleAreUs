using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PeopleAreUs.Core;
using PeopleAreUs.DTO;
using PeopleAreUs.Infrastructure;
using PeopleAreUs.Services.Requests;
using PeopleAreUs.Services.Responses;
using PeopleAreUs.Services.Specifications;

namespace PeopleAreUs.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly IPeopleAreUsHttpClient _client;
        private readonly IPetTypeSpecification _petTypeSpecification;
        private readonly IMapper<Person, Domain.Models.Person> _mapper;
        private readonly ILogger<PeopleService> _logger;

        public PeopleService(IPeopleAreUsHttpClient client, IPetTypeSpecification petTypeSpecification, IMapper<Person, Domain.Models.Person> mapper,
            ILogger<PeopleService> logger)
        {
            _client = client;
            _petTypeSpecification = petTypeSpecification;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OperationResult<GetPetOwnersResponse>> GetPetOwnersAsync(GetPetOwnersRequest request)
        {
            try
            {
                var getPeopleOperation = await _client.GetPeopleAsync().ConfigureAwait(false);
                if (!getPeopleOperation.Status)
                {
                    _logger.LogError("Cannot retrieve people from the API");
                    return OperationResult<GetPetOwnersResponse>.Failure("Cannot retrieve people");
                }

                var people = getPeopleOperation.Data.Select(x=>_mapper.Map(x)).ToList();

                var selectedPeople = new List<Domain.Models.Person>();

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

                    selectedPeople.Add(new Domain.Models.Person
                    {
                        Name = person.Name,
                        Age = person.Age,
                        Gender = person.Gender,
                        Pets = selectedPets
                    });
                }

                return OperationResult<GetPetOwnersResponse>.Success(new GetPetOwnersResponse(selectedPeople));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error when retrieving people: {exception}");
            }

            return OperationResult<GetPetOwnersResponse>.Failure("Cannot retrieve people");
        }
    }
}
