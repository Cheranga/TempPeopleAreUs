using System.Collections.Generic;
using PeopleAreUs.Core;
using PeopleAreUs.DTO;

namespace PeopleAreUs.Infrastructure
{
    public interface IPeopleDataConverter
    {
        OperationResult<List<Person>> Convert(string content);
    }
}