using System;
using System.Collections.Generic;
using System.Linq;
using BusinessModels = PeopleAreUs.Console.Business.Models;
using ExternalDto = PeopleAreUs.Console.DTO.External;

namespace PeopleAreUs.Console.Mappers
{
    public class DtoPersonToBusinessPerson : IMapper<ExternalDto.Person, BusinessModels.Person>
    {
        private readonly IMapper<ExternalDto.Pet, BusinessModels.Pet> _petMapper;

        public DtoPersonToBusinessPerson(IMapper<ExternalDto.Pet, BusinessModels.Pet> petMapper)
        {
            _petMapper = petMapper;
        }

        public BusinessModels.Person Map(ExternalDto.Person source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var pets = source.Pets?.ToList() ?? new List<ExternalDto.Pet>();
            return new BusinessModels.Person
            {
                Name = source.Name,
                Age = source.Age,
                Gender = GetGender(source.Gender),
                Pets = pets.Select(x => _petMapper.Map(x))
            };
        }

        private BusinessModels.Gender GetGender(string value)
        {
            if (Enum.TryParse(value, true, out BusinessModels.Gender gender))
            {
                return gender;
            }

            return BusinessModels.Gender.None;
        }
    }
}