using PeopleAreUs.Domain.Models;

namespace PeopleAreUs.Services.Specifications
{
    internal class PetTypeSpecification : IPetTypeSpecification
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