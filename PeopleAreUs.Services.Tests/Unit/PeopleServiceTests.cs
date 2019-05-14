using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using PeopleAreUs.Core;
using PeopleAreUs.Domain.Models;
using PeopleAreUs.Infrastructure;
using PeopleAreUs.Services.Mappers;
using PeopleAreUs.Services.Requests;
using PeopleAreUs.Services.Specifications;
using Xunit;
using Person = PeopleAreUs.DTO.Person;
using Pet = PeopleAreUs.DTO.Pet;

namespace PeopleAreUs.Services.Tests.Unit
{
    public class PeopleServiceTests
    {
        [Fact]
        public async Task If_People_Cannot_Be_Retrieved_Must_Return_Failure()
        {
            //
            // Arrange
            //
            var mockedApiClient = new Mock<IPeopleAreUsHttpClient>();
            mockedApiClient.Setup(x => x.GetPeopleAsync()).ReturnsAsync(OperationResult<List<Person>>.Failure("some error"));
            var service = new PeopleService(mockedApiClient.Object, Mock.Of<IPetTypeSpecification>(), Mock.Of<IMapper<Person, Domain.Models.Person>>(), Mock.Of<ILogger<PeopleService>>());
            //
            // Act
            //
            var operation = await service.GetPetOwnersAsync(It.IsAny<GetPetOwnersRequest>());
            //
            // Assert
            //
            Assert.False(operation.Status);
        }

        [Fact]
        public async Task If_The_Data_Conversion_Encounters_an_Error_Must_Return_Failure()
        {
            //
            // Arrange
            //
            var mockedApiClient = new Mock<IPeopleAreUsHttpClient>();
            mockedApiClient.Setup(x => x.GetPeopleAsync()).ReturnsAsync(OperationResult<List<Person>>.Success(new List<Person>
            {
                new Person{Name = "some name", Age = 18, Gender = "blah", Pets = new List<Pet>()}
            }));

            var mockedMapper = new Mock<IMapper<Person, Domain.Models.Person>>();
            mockedMapper.Setup(x => x.Map(It.IsAny<Person>())).Throws<Exception>();
            var service = new PeopleService(mockedApiClient.Object, Mock.Of<IPetTypeSpecification>(), mockedMapper.Object, Mock.Of<ILogger<PeopleService>>());
            //
            // Act
            //
            var operation = await service.GetPetOwnersAsync(It.IsAny<GetPetOwnersRequest>());
            //
            // Assert
            //
            Assert.False(operation.Status);
        }

        [Fact]
        public async Task If_There_Are_No_People_Must_Return_Success_With_Empty_People_List()
        {
            //
            // Arrange
            //
            var mockedApiClient = new Mock<IPeopleAreUsHttpClient>();
            mockedApiClient.Setup(x => x.GetPeopleAsync()).ReturnsAsync(OperationResult<List<Person>>.Success(new List<Person>()));
            var service = new PeopleService(mockedApiClient.Object, Mock.Of<IPetTypeSpecification>(), Mock.Of<IMapper<Person, Domain.Models.Person>>(), Mock.Of<ILogger<PeopleService>>());
            //
            // Act
            //
            var operation = await service.GetPetOwnersAsync(It.IsAny<GetPetOwnersRequest>());
            //
            // Assert
            //
            Assert.True(operation.Status);
            Assert.NotNull(operation.Data);
            Assert.Empty(operation.Data.People);
        }


        [Fact]
        public async Task If_There_Are_No_Owners_For_The_Requested_PetType_Must_Return_Success_With_Empty_People_List()
        {
            //
            // Arrange
            //
            var mockedClient = new Mock<IPeopleAreUsHttpClient>();
            mockedClient.Setup(x => x.GetPeopleAsync()).ReturnsAsync(OperationResult<List<Person>>.Success(new List<Person>
            {
                new Person
                {
                    Name = "some name1", Age = 18, Gender = "Male", Pets = new List<Pet>
                    {
                        new Pet{Name = "some dog 1", Type = "Dog"},
                        new Pet{Name = "some dog 2", Type = "Dog"},
                    }
                }
            }));
            var petMapper = new DtoPetToBusinessPet();
            var personMapper = new DtoPersonToBusinessPerson(petMapper);
            var specification = new PetTypeSpecification();
            var service = new PeopleService(mockedClient.Object, specification, personMapper, Mock.Of<ILogger<PeopleService>>());
            //
            // Act
            //
            var operation = await service.GetPetOwnersAsync(new GetPetOwnersRequest(PetType.Cat));
            //
            // Assert
            //
            Assert.True(operation.Status);
            Assert.NotNull(operation.Data);
            Assert.Empty(operation.Data.People);
        }

        [Fact]
        public async Task If_There_Are_Owners_For_The_Requested_PetType_Must_Return_Success_With_Those_People()
        {
            //
            // Arrange
            //
            var mockedClient = new Mock<IPeopleAreUsHttpClient>();
            mockedClient.Setup(x => x.GetPeopleAsync()).ReturnsAsync(OperationResult<List<Person>>.Success(new List<Person>
            {
                new Person
                {
                    Name = "some name1", Age = 18, Gender = "Male", Pets = new List<Pet>
                    {
                        new Pet{Name = "some dog 1", Type = "Dog"},
                        new Pet{Name = "some dog 2", Type = "Dog"},
                    }
                }
            }));
            var petMapper = new DtoPetToBusinessPet();
            var personMapper = new DtoPersonToBusinessPerson(petMapper);
            var specification = new PetTypeSpecification();
            var service = new PeopleService(mockedClient.Object, specification, personMapper, Mock.Of<ILogger<PeopleService>>());
            //
            // Act
            //
            var operation = await service.GetPetOwnersAsync(new GetPetOwnersRequest(PetType.Dog));
            //
            // Assert
            //
            Assert.True(operation.Status);
            Assert.NotNull(operation.Data);
            Assert.True(operation.Data.People.All(x=>x.Pets.All(y=>y.Type == PetType.Dog)));
        }

    }
}
