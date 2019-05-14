using System.Collections.Generic;
using PeopleAreUs.Domain.Models;

namespace PeopleAreUs.Console.ViewModels
{
    public class PetsByOwnerGenderViewModel
    {
        public IDictionary<Gender, List<string>> PetsMappedByOwnersGender { get; set; }

        public PetsByOwnerGenderViewModel()
        {
            PetsMappedByOwnersGender = new Dictionary<Gender, List<string>>();
        }
    }
}