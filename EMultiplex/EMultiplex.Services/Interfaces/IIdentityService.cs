using EMultiplex.Models.Responses;
using System.Threading.Tasks;

namespace EMultiplex.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
        Task<AuthenticationResult> LoginAsync(string email, string password);
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
        Task<(bool IsSuccess, string ErrorMessage)> AddRoleAsync(string role);
        Task<(bool IsSuccess, string ErrorMessage)> AddUserRoleAsync(string email, string roleName);
    }
}
