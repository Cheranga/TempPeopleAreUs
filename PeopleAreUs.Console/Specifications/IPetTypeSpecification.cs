using PeopleAreUs.Console.Business.Models;

namespace PeopleAreUs.Console.Specifications
{
    public interface IPetTypeSpecification
    {
        bool IsSatisfiedBy(Pet pet, PetType petType);
    }
}