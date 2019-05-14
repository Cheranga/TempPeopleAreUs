using PeopleAreUs.Domain.Models;

namespace PeopleAreUs.Services.Specifications
{
    public interface IPetTypeSpecification
    {
        bool IsSatisfiedBy(Pet pet, PetType petType);
    }
}