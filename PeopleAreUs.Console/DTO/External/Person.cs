using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PeopleAreUs.Console.DTO.External
{
    [DebuggerDisplay("{Name}:{Gender}")]
    public class Person
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public IEnumerable<Pet> Pets { get; set; }

        public Person()
        {
            Pets = new List<Pet>();
        }
    }
}
