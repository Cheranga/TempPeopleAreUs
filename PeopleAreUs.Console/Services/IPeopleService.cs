using System.Threading.Tasks;
using PeopleAreUs.Console.Core;
using PeopleAreUs.Console.DTO.Internal;

namespace PeopleAreUs.Console.Services
{
    public interface IPeopleService
    {
        Task<ResultStatus<GetPetOwnersResponse>> GetPetOwnersAsync(GetPetOwnersRequest request);
    }
}