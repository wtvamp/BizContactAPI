﻿using System.Threading.Tasks;
using System.Security.Claims;

namespace BizContacts.API
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
    }
}