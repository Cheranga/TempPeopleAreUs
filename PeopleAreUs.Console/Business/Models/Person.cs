using System.Collections.Generic;

namespace PeopleAreUs.Console.Business.Models
{
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