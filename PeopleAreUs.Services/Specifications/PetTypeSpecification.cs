using PeopleAreUs.Domain.Models;

namespace PeopleAreUs.Services.Specifications
{
    public class PetTypeSpecification : IPetTypeSpecification
    {
        public bool IsSatisfiedBy(Pet pet, PetType petType)
        {
            if (pet == null)
            {
                return false;
            }

            return pet.Type == petType;
        }
    }
}