using PeopleAreUs.Console.Business.Models;

namespace PeopleAreUs.Console.Application.Requests
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