using System;
using System.Collections.Generic;
using System.Linq;
using PeopleAreUs.Core;
using PeopleAreUs.DTO;

namespace PeopleAreUs.Services.Mappers
{
    internal class DtoPersonToBusinessPerson : IMapper<Person, Domain.Models.Person>
    {
        private readonly IMapper<Pet, Domain.Models.Pet> _petMapper;

        public DtoPersonToBusinessPerson(IMapper<Pet, Domain.Models.Pet> petMapper)
        {
            _petMapper = petMapper;
        }

        public Domain.Models.Person Map(Person source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var pets = source.Pets?.ToList() ?? new List<Pet>();
            return new Domain.Models.Person
            {
                Name = source.Name,
                Age = source.Age,
                Gender = GetGender(source.Gender),
                Pets = pets.Select(x => _petMapper.Map(x))
            };
        }

        private Domain.Models.Gender GetGender(string value)
        {
            if (Enum.TryParse(value, true, out Domain.Models.Gender gender))
            {
                return gender;
            }

            return Domain.Models.Gender.None;
        }
    }
}