using System.Threading.Tasks;
using PeopleAreUs.Core;
using PeopleAreUs.Services.Requests;
using PeopleAreUs.Services.Responses;

namespace PeopleAreUs.Services
{
    public interface IPeopleService
    {
        Task<OperationResult<GetPetOwnersResponse>> GetPetOwnersAsync(GetPetOwnersRequest request);
    }
}