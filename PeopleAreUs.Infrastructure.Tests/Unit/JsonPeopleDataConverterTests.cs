using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace PeopleAreUs.Infrastructure.Tests.Unit
{
    public class JsonPeopleDataConverterTests
    {
        [Fact]
        public void If_The_Content_Is_Null_Or_Empty_A_Failure_Result_Will_Be_Returned()
        {
            //
            // Arrange
            //
            var mockedLogger = new Mock<ILogger<JsonPeopleDataConverter>>();
            var converter = new JsonPeopleDataConverter(mockedLogger.Object);
            //
            // Act
            //
            var operationWithNull = converter.Convert(null);
            var operationWithEmpty = converter.Convert(string.Empty);
            //
            // Assert
            //
            Assert.False(operationWithNull.Status);
            Assert.False(operationWithEmpty.Status);
        }

        [Fact]
        public void If_The_Content_Is_Invalid_A_Failure_Result_Will_Be_Returned()
        {
            //
            // Arrange
            //
            var mockedLogger = new Mock<ILogger<JsonPeopleDataConverter>>();
            var converter = new JsonPeopleDataConverter(mockedLogger.Object);
            //
            // Act
            //
            var operation = converter.Convert("blah");
            //
            // Assert
            //
            Assert.False(operation.Status);
        }
    }
}
