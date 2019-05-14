using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using PeopleAreUs.Core;
using PeopleAreUs.Domain.Models;
using PeopleAreUs.Services.Mappers;
using Xunit;
using Person = PeopleAreUs.DTO.Person;
using Pet = PeopleAreUs.DTO.Pet;

namespace PeopleAreUs.Services.Tests.Unit
{
    public class DtoPersonToBusinessPersonTests
    {
        [Fact]
        public void If_PetMapper_Is_Null_Must_Throw_Error()
        {
            //
            // Arrange, act and assert
            //
            Assert.Throws<ArgumentNullException>(() => new DtoPersonToBusinessPerson(null));
        }

        [Fact]
        public void If_DTO_Person_Is_Null_Must_Throw_Error()
        {
            //
            // Arrange, act and assert
            //
            Assert.Throws<ArgumentNullException>(() =>
            {
                var mapper = new DtoPersonToBusinessPerson(Mock.Of<IMapper<Pet, Domain.Models.Pet>>());
                var target = mapper.Map(null);
            });
        }

        [Fact]
        public void If_The_Owner_Gender_Is_Not_Supported_It_Must_Be_Returned_As_None()
        {
            //
            // Arrange
            //
            var mapper = new DtoPersonToBusinessPerson(Mock.Of<IMapper<Pet, Domain.Models.Pet>>());
            //
            // Act
            //
            var target = mapper.Map(new Person {Name = "some name", Gender = "blah"});
            //
            // Assert
            //
            Assert.Equal(Gender.None, target.Gender);
        }

        [Fact]
        public void If_There_Are_Null_Pets_Must_Throw_Error()
        {
            //
            // Arrange
            //
            var petMapper = new DtoPetToBusinessPet();
            var mapper = new DtoPersonToBusinessPerson(petMapper);
            //
            // Act and assert
            //
            Assert.Throws<ArgumentNullException>(() =>
            {
                var target = mapper.Map(new Person
                {
                    Name = "some name",
                    Gender = "Male",
                    Pets = new List<Pet>(new Pet[] { null, null })
                });
            });
        }

        [Fact]
        public void If_The_Owner_Data_Are_Valid_Must_Return_A_Valid_Person()
        {
            //
            // Arrange
            //
            var mapper = new DtoPersonToBusinessPerson(new DtoPetToBusinessPet());
            //
            // Act
            //
            var target = mapper.Map(new Person { Name = "some name", Gender = "Female", Age = 20, Pets =  new Pet[]
            {
                new Pet{Name = "some pet1", Type = "Cat"},
                new Pet{Name = "some pet2", Type = "Dog"}
            }
            });
            //
            // Assert
            //
            Assert.Equal("some name", target.Name);
            Assert.Equal(20, target.Age);
            Assert.Equal(Gender.Female,target.Gender);

            Assert.Equal(1, target.Pets.Count(x=>x.Type == PetType.Cat));
            Assert.Equal(1, target.Pets.Count(x => x.Type == PetType.Dog));
        }
    }
}