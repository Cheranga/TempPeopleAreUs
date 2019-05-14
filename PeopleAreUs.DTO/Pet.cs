using System.Diagnostics;

namespace PeopleAreUs.DTO
{
    [DebuggerDisplay("{Name}:{Type}")]
    public class Pet
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}