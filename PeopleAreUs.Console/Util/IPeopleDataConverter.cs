using System.Collections.Generic;
using PeopleAreUs.Console.Core;
using PeopleAreUs.Console.DTO.External;

namespace PeopleAreUs.Console.Util
{
    public interface IPeopleDataConverter
    {
        ResultStatus<List<Person>> Convert(string content);
    }
}