﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using PeopleAreUs.Domain.Models;

namespace PeopleAreUs.Console.DTO.Internal
{
    public class GetPetOwnersResponse
    {
        public ReadOnlyCollection<Person> People { get; }

        public GetPetOwnersResponse(List<Person> people)
        {
            People = new ReadOnlyCollection<Person>(people ?? new List<Person>());
        }
    }
}