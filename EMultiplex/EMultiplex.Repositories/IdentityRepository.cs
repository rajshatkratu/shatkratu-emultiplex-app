using EMultiplex.DAL;
using EMultiplex.DAL.Domain;
using EMultiplex.Models.Options;
using EMultiplex.Models.Responses;
using EMultiplex.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EMultiplex.Repositories
{
    public class IdentityRepository : IIdentityRespository
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtOptions _jwtOptions;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly EMultiplexDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityRepository(UserManager<IdentityUser> userManager, JwtOptions jwtOptions, TokenValidationParameters tokenValidationParameters, EMultiplexDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _jwtOptions = jwtOptions ?? throw new ArgumentNullException(nameof(jwtOptions));
            _tokenValidationParameters = tokenValidationParameters ?? throw new ArgumentNullException(nameof(tokenValidationParameters));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }



        #region jwt_auth_token_sections

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exists" }
                };
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!isValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User-password does not match" }
                };
            }

            return await GeneratAuthenticationResultForAUserAsync(user);

        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            var principal = GetClaimsPrincipalFromToken(token);

            if (principal == null)
            {
                return new AuthenticationResult()
                {
                    Errors = new[] { "Invalid token" }
                };
            }

            var expiryDateTimeInSeconds = long.Parse(principal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);


            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                                        .AddSeconds(expiryDateTimeInSeconds);

            if (expiryDateTimeUtc > DateTime.UtcNow)
            {
                return new AuthenticationResult()
                {
                    Errors = new[] { "This token is not expired yet" }
                };
            }

            var jti = principal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            var storedRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken);

            if (storedRefreshToken == null)
            {
                return new AuthenticationResult()
                {
                    Errors = new[] { "This refresh token does not exist" }
                };
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                return new AuthenticationResult()
                {
                    Errors = new[] { "This refresh token has expired" }
                };
            }

            if (storedRefreshToken.IsUsed)
            {
                return new AuthenticationResult()
                {
                    Errors = new[] { "This refresh has already been used" }
                };
            }

            if (storedRefreshToken.IsInvalidated)
            {
                return new AuthenticationResult()
                {
                    Errors = new[] { "This refresh token has been invalidated" }
                };
            }

            if (storedRefreshToken.JwtId != jti)
            {
                return new AuthenticationResult()
                {
                    Errors = new[] { "This refresh token does not match this JWT" }
                };
            }

            storedRefreshToken.IsUsed = true;

            _context.RefreshTokens.Update(storedRefreshToken);

            await _context.SaveChangesAsync();

            string userId = principal.Claims.Single(x => x.Type == "id").Value;

            var user = await _userManager.FindByIdAsync(userId);

            return await GeneratAuthenticationResultForAUserAsync(user);

        }

        public async Task<AuthenticationResult> RegisterAsync(string email, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User already exists" }
                };
            }

            var newUser = new IdentityUser
            {
                Email = email,
                UserName = email
            };

            var createdUser = await _userManager.CreateAsync(newUser, password);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }

            return await GeneratAuthenticationResultForAUserAsync(newUser);
        }

        #endregion


        #region role_service_sections

        public async Task<(bool IsSuccess, string ErrorMessage)> AddRoleAsync(string role)
        {
            bool isRoleExists = await _roleManager.RoleExistsAsync(role);
            if (!isRoleExists)
            {
                var identityRole = new IdentityRole();
                identityRole.Name = role;
                var identityResult = await _roleManager.CreateAsync(identityRole);

                if (identityResult.Succeeded)
                    return (true, null);

                return (false, "Error while creating role.");
            }

            return (false, "Role already exists.");
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> AddUserRoleAsync(string email, string roleName)
        {
            bool isRoleExists = await _roleManager.RoleExistsAsync(roleName);
            if (isRoleExists)
            {
                var user = await _userManager.FindByEmailAsync(email);
                bool isRoleAlreadyAssigned = await _userManager.IsInRoleAsync(user, roleName);
                if (user != null && !isRoleAlreadyAssigned)
                {
                    var idResult = await _userManager.AddToRoleAsync(user, roleName);

                    if (idResult.Succeeded)
                        return (true, null);

                }
                return (false, "Error while creating role.");
            }

            return (false, "Role does not exist.");

        }

        #endregion


        #region private_methods
        private async Task<AuthenticationResult> GeneratAuthenticationResultForAUserAsync(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Convert.FromBase64String(_jwtOptions.Secret);

            var symmetricSecurityKey = new SymmetricSecurityKey(key);
            var lstRoles = await _userManager.GetRolesAsync(user);
            var roles = string.Empty;
            if (lstRoles != null && lstRoles.Any())
            {
                roles = string.Join(",", lstRoles);
            }

            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("id", user.Id),
                    new Claim(ClaimTypes.Role, roles)
                };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtOptions.ExpiryTime),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtToken = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshToken
            { 
                Token = Guid.NewGuid().ToString(),
                JwtId = jwtToken.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(90)
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return new AuthenticationResult
            {
                IsSuccess = true,
                Token = tokenHandler.WriteToken(jwtToken),
                RefreshToken = refreshToken.Token
            };

        }


        private ClaimsPrincipal GetClaimsPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);

                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                    return null;

                return principal;
            }
            catch
            {

                return null;
            }

        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken securityToken)
        {
            return securityToken is JwtSecurityToken jwtSecurityToken && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion
    }
}
