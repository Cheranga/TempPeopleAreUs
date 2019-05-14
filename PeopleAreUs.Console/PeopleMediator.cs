using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PeopleAreUs.Console.Output;
using PeopleAreUs.Console.Requests;
using PeopleAreUs.Console.ViewModels;
using PeopleAreUs.Core;
using PeopleAreUs.Services;
using PeopleAreUs.Services.Requests;
using PeopleAreUs.Services.Responses;

namespace PeopleAreUs.Console
{
    public class PeopleMediator : IPeopleMediator
    {
        private readonly IPeopleService _peopleService;
        private readonly IMapper<GetPetOwnersResponse, PetsByOwnerGenderViewModel> _mapper;
        private readonly IRenderer<PetsByOwnerGenderViewModel> _renderer;
        private readonly ILogger<PeopleMediator> _logger;

        public PeopleMediator(IPeopleService peopleService, IMapper<GetPetOwnersResponse, PetsByOwnerGenderViewModel> mapper,
            IRenderer<PetsByOwnerGenderViewModel> renderer, ILogger<PeopleMediator> logger)
        {
            _peopleService = peopleService;
            _mapper = mapper;
            _renderer = renderer;
            _logger = logger;
        }

        public async Task ShowPetsAsync(ShowPetsRequest request)
        {
            var getPeopleOperation = await _peopleService.GetPetOwnersAsync(new GetPetOwnersRequest(request.PetType));
            if (!getPeopleOperation.Status)
            {
                _logger.LogError("Error when getting the people");
                return;
            }

            foreach (var person in getPeopleOperation.Data.People)
            {
                var pets = person.Pets.OrderBy(x => x.Name);
                person.Pets = pets;
            }

            var viewModel = _mapper.Map(getPeopleOperation.Data);


            await _renderer.RenderAsync(viewModel);
        }

    }
}
