using System;
using PeopleAreUs.Core;
using PeopleAreUs.Domain.Models;
using Pet = PeopleAreUs.DTO.Pet;

namespace PeopleAreUs.Services.Mappers
{
    internal class DtoPetToBusinessPet : IMapper<Pet, Domain.Models.Pet>
    {
        public Domain.Models.Pet Map(Pet source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Domain.Models.Pet
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