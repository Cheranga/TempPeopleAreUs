using System.Collections.Generic;
using System.Threading.Tasks;
using PeopleAreUs.Core;
using PeopleAreUs.DTO;

namespace PeopleAreUs.Infrastructure
{
    public interface IPeopleAreUsHttpClient
    {
        Task<OperationResult<List<Person>>> GetPeopleAsync();
    }
}