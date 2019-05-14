using PeopleAreUs.Domain.Models;

namespace PeopleAreUs.Console.Requests
{
    public class ShowPetsRequest
    {
        public PetType PetType { get; }

        public ShowPetsRequest(PetType petType)
        {
            PetType = petType;
        }
    }
}