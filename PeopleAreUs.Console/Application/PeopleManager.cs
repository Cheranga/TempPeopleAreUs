using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PeopleAreUs.Console.Application.Requests;
using PeopleAreUs.Console.Application.ViewModels;
using PeopleAreUs.Console.DTO.Internal;
using PeopleAreUs.Console.Mappers;
using PeopleAreUs.Console.Output;
using PeopleAreUs.Console.Services;

namespace PeopleAreUs.Console.Application
{
    public class PeopleManager : IPeopleManager
    {
        private readonly IPeopleService _peopleService;
        private readonly IMapper<GetPetOwnersResponse, PetsByOwnerGenderViewModel> _mapper;
        private readonly IRenderer<PetsByOwnerGenderViewModel> _renderer;
        private readonly ILogger<PeopleManager> _logger;

        public PeopleManager(IPeopleService peopleService, IMapper<GetPetOwnersResponse, PetsByOwnerGenderViewModel> mapper,
            IRenderer<PetsByOwnerGenderViewModel> renderer, ILogger<PeopleManager> logger)
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
