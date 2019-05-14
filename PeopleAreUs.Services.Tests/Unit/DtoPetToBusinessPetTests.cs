using System;
using PeopleAreUs.Domain.Models;
using PeopleAreUs.Services.Mappers;
using Xunit;
using Pet = PeopleAreUs.DTO.Pet;

namespace PeopleAreUs.Services.Tests.Unit
{
    public class DtoPetToBusinessPetTests
    {
        [Fact]
        public void If_DTO_Pet_Is_Null_Must_Throw_Error()
        {
            //
            // Arrange, act and assert
            //
            Assert.Throws<ArgumentNullException>(() =>
            {
                var mapper = new DtoPetToBusinessPet();
                var target = mapper.Map(null);
            });
        }

        [Fact]
        public void If_The_PetType_Is_Not_Supported_It_Must_Be_Returned_As_None()
        {
            //
            // Arrange
            //
            var mapper = new DtoPetToBusinessPet();
            //
            // Act
            //
            var target = mapper.Map(new Pet{Name = "some name1", Type = "blah"});
            //
            // Assert
            //
            Assert.Equal(PetType.None, target.Type);
        }

        [Fact]
        public void If_The_Pet_Data_Are_Valid_Must_Return_A_Valid_Pet()
        {
            //
            // Arrange
            //
            var mapper = new DtoPetToBusinessPet();
            //
            // Act
            //
            var target = mapper.Map(new Pet
            {
                Name = "some pet1",
                Type = "Dog"
            });
            //
            // Assert
            //
            Assert.Equal("some pet1", target.Name);
            Assert.Equal(PetType.Dog, target.Type);
        }
    }
}
