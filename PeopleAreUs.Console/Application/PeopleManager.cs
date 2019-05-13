using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PeopleAreUs.Console.Application.ViewModels;
using PeopleAreUs.Console.Business.Models;
using PeopleAreUs.Console.DTO.Internal;
using PeopleAreUs.Console.Mappers;
using PeopleAreUs.Console.Services;

namespace PeopleAreUs.Console.Application
{
    public interface IPeopleManager
    {
        Task ShowPetsAsync(ShowPetsRequest request);
    }

    public interface IRenderer<in T>
    {
        Task RenderAsync(T data);
    }

    public class PetsByOwnerGenderRenderer:IRenderer<PetsByOwnerGenderViewModel>
    {
        public async Task RenderAsync(PetsByOwnerGenderViewModel data)
        {
            if (data?.PetsMappedByOwnersGender == null || !data.PetsMappedByOwnersGender.Any())
            {
                await System.Console.Out.WriteLineAsync("No pets");
                return;
            }

            foreach (var (key, value) in data.PetsMappedByOwnersGender)
            {
                await System.Console.Out.WriteLineAsync(key.ToString());
                foreach (var subItem in value)
                {
                    await System.Console.Out.WriteLineAsync($"* {subItem}");
                }
            }
        }
    }

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

            var viewModel = _mapper.Map(getPeopleOperation.Data);
            await _renderer.RenderAsync(viewModel);
        }

    }

    public class ShowPetsRequest
    {
        public PetType PetType { get; }

        public ShowPetsRequest(PetType petType)
        {
            PetType = petType;
        }
    }
}
