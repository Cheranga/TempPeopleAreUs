using System;
using PeopleAreUs.Console.Business.Models;
using Pet = PeopleAreUs.Console.DTO.External.Pet;

namespace PeopleAreUs.Console.Mappers
{
    public class DtoPetToBusinessPet : IMapper<Pet, Business.Models.Pet>
    {
        public Business.Models.Pet Map(Pet source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Business.Models.Pet
            {
                Name = source.Name,
                Type = GetPetType(source.Type)
            };
        }

        private PetType GetPetType(string value)
        {
            if (Enum.TryParse(value, true, out PetType petType))
            {
                return petType;
            }

            return PetType.None;
        }
    }
}