using System.Collections.Generic;
using System.Diagnostics;

namespace PeopleAreUs.Console.Business.Models
{
    [DebuggerDisplay("{Gender}:{Name}")]
    public class Person
    {
        public Person()
        {
            Pets = new List<Pet>();
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }

        public IEnumerable<Pet> Pets { get; set; }
    }
}