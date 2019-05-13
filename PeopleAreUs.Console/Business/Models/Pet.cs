using System.Diagnostics;

namespace PeopleAreUs.Console.Business.Models
{
    [DebuggerDisplay("{Type}:{Name}")]
    public class Pet
    {
        public PetType Type { get; set; }
        public string Name { get; set; }
    }
}