using System.Diagnostics;

namespace PeopleAreUs.Domain.Models
{
    [DebuggerDisplay("{Type}:{Name}")]
    public class Pet
    {
        public PetType Type { get; set; }
        public string Name { get; set; }
    }
}