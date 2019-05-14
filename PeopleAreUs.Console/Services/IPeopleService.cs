﻿using System.Threading.Tasks;
using PeopleAreUs.Console.DTO.Internal;
using PeopleAreUs.Core;

namespace PeopleAreUs.Console.Services
{
    public interface IPeopleService
    {
        Task<OperationResult<GetPetOwnersResponse>> GetPetOwnersAsync(GetPetOwnersRequest request);
    }
}