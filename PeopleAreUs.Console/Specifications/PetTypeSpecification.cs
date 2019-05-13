using PeopleAreUs.Console.Business.Models;

namespace PeopleAreUs.Console.Specifications
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