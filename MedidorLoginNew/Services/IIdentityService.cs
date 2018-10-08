using MedidorLoginNew.Models;
using System.Threading.Tasks;

namespace MedidorLoginNew.Services
{
    public interface IIdentityService
    {
        Task<bool> UserExists(string username);
        Task<bool> LogIn(Login model);
        Task<UserToken> GetJwtToken(Login model);
    }
}