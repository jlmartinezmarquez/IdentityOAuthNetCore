using MedidorLoginNew.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MedidorLoginNew.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppSettings _appSettings;

        public IdentityService(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager,
            IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        public async Task<bool> UserExists(string username) => await _userManager.FindByNameAsync(username) != null;

        public async Task<bool> LogIn(Login model)
        {
            var passwordSignInResult = await _signInManager.PasswordSignInAsync(
                model.Username, 
                model.Password, 
                isPersistent: true, 
                lockoutOnFailure: false);

            return passwordSignInResult.Succeeded;
        }

        public async Task<UserToken> GetJwtToken(Login model)
        {
            var identityUser = await _userManager.FindByNameAsync(model.Username);
            var user = new UserToken
            {
                Username = model.Username,
                
            };

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }
    }
}
