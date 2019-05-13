using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using PeopleAreUs.Console.Application.ViewModels;
using PeopleAreUs.Console.DTO.Internal;
using PeopleAreUs.Console.Mappers;
using PeopleAreUs.Console.Services;

namespace PeopleAreUs.Console.Application
{
    public interface IPeopleManager
    {

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

    public class PeopleManager
    {
        private readonly IPeopleService _peopleService;
        private readonly IMapper<GetPetOwnersResponse, PetsByOwnerGenderViewModel> _mapper;

        public PeopleManager(IPeopleService peopleService, IMapper<GetPetOwnersResponse, PetsByOwnerGenderViewModel> mapper,
            IRenderer<PetsByOwnerGenderViewModel> renderer)
        {
            _peopleService = peopleService;
            _mapper = mapper;
        }

    }
}
