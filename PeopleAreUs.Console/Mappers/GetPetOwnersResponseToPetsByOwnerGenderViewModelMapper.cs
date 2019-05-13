using System;
using System.Linq;
using PeopleAreUs.Console.Application.ViewModels;
using PeopleAreUs.Console.DTO.Internal;

namespace PeopleAreUs.Console.Mappers
{
    public class GetPetOwnersResponseToPetsByOwnerGenderViewModelMapper : IMapper<GetPetOwnersResponse, PetsByOwnerGenderViewModel>
    {
        public PetsByOwnerGenderViewModel Map(GetPetOwnersResponse source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var peopleWithPets = source.People.Where(x => x.Pets != null && x.Pets.Any()).ToList();
            if (!peopleWithPets.Any())
            {
                return new PetsByOwnerGenderViewModel();
            }
            //
            // Group the collection by the owner's gender and select the pet names
            //
            var petsMappedByOwnerGender = peopleWithPets.GroupBy(x => x.Gender)
                .ToDictionary(x => x.Key, x => x.SelectMany(y => y.Pets.Select(z => z.Name)).ToList());

            return new PetsByOwnerGenderViewModel
            {
                PetsMappedByOwnersGender = petsMappedByOwnerGender
            };
        }
    }
}