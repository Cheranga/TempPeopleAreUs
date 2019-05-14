using PeopleAreUs.Domain.Models;
using PeopleAreUs.Services.Specifications;
using Xunit;

namespace PeopleAreUs.Services.Tests.Unit
{
    public class PetTypeSpecificationTests
    {
        [Fact]
        public void If_Pet_Is_Null_Return_False()
        {
            //
            // Arrange
            //
            var specification = new PetTypeSpecification();
            //
            // Act
            //
            var status = specification.IsSatisfiedBy(null, PetType.Dog);
            //
            // Assert
            //
            Assert.False(status);
        }

        [Fact]
        public void If_Requested_PetType_Does_Not_Match_The_PetType_Must_Return_False()
        {
            //
            // Arrange
            //
            var specification = new PetTypeSpecification();
            //
            // Act
            //
            var status = specification.IsSatisfiedBy(new Pet {Type = PetType.Dog}, PetType.Cat);
            //
            // Assert
            //
            Assert.False(status);
        }

        [Fact]
        public void If_Requested_PetType_Matches_The_PetType_Must_Return_True()
        {
            //
            // Arrange
            //
            var specification = new PetTypeSpecification();
            //
            // Act
            //
            var status = specification.IsSatisfiedBy(new Pet { Type = PetType.Dog }, PetType.Dog);
            //
            // Assert
            //
            Assert.True(status);
        }
    }
}
