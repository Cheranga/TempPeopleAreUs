using System.Collections.Generic;
using System.Linq;
using PeopleAreUs.Console.Business.Models;

namespace PeopleAreUs.Console.Application.ViewModels
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