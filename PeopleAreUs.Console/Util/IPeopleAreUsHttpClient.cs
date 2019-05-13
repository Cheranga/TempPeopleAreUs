using System.Collections.Generic;
using System.Threading.Tasks;
using PeopleAreUs.Console.Core;
using PeopleAreUs.Console.DTO.External;

namespace PeopleAreUs.Console.Util
{
    public interface IPeopleAreUsHttpClient
    {
        Task<ResultStatus<List<Person>>> GetPeopleAsync();
    }
}