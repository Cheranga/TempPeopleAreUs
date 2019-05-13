using System;
using System.Collections.Generic;
using System.Text;
using PeopleAreUs.Console.DTO.External;

namespace PeopleAreUs.Console.Business.Models
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }

        public IEnumerable<Pet> Pets { get; set; }

        public Person()
        {
            Pets = new List<Pet>();
        }
    }
}
