using PeopleAreUs.Domain.Models;

namespace PeopleAreUs.Services.Requests
{
    public class GetPetOwnersRequest
    {
        public GetPetOwnersRequest(PetType petType)
        {
            Type = petType;
        }

        public PetType Type { get; }
    }
}