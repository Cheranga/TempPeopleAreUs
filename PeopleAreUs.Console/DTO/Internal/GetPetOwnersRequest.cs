using PeopleAreUs.Console.Business.Models;

namespace PeopleAreUs.Console.DTO.Internal
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