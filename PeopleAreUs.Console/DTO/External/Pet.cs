using System.Diagnostics;

namespace PeopleAreUs.Console.DTO.External
{
    [DebuggerDisplay("{Name}:{Type}")]
    public class Pet
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}