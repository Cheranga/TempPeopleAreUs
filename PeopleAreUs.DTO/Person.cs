using System.Collections.Generic;
using System.Diagnostics;

namespace PeopleAreUs.DTO
{
    [DebuggerDisplay("{Name}:{Gender}")]
    public class Person
    {
        public Person()
        {
            Pets = new List<Pet>();
        }

        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public IEnumerable<Pet> Pets { get; set; }
    }
}