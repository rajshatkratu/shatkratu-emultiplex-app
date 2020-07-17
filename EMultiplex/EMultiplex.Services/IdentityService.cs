using EMultiplex.Models.Options;
using EMultiplex.Models.Responses;
using EMultiplex.Repositories.Interfaces;
using EMultiplex.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EMultiplex.Services
{
    public class IdentityService : IIdentityService
    {

        private readonly IIdentityRespository _identityRespository;

        public IdentityService(IIdentityRespository identityRespository)
        {
            _identityRespository = identityRespository ?? throw new ArgumentNullException(nameof(identityRespository));
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> AddRoleAsync(string role)
        {
            return await _identityRespository.AddRoleAsync(role);
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> AddUserRoleAsync(string email, string roleName)
        {
            return await _identityRespository.AddUserRoleAsync(email, roleName);
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            return await _identityRespository.LoginAsync(email, password);
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            return await _identityRespository.RefreshTokenAsync(token, refreshToken);
        }

        public async Task<AuthenticationResult> RegisterAsync(string email, string password)
        {
            return await _identityRespository.RegisterAsync(email, password);
        }
    }
}
