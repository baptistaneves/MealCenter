using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MealCenter.Identity.Application.Services
{
    public class JwtAdminService
    {
        private readonly JwtService _jwtService;
        private readonly UserManager<IdentityUser> _userManager;

        public JwtAdminService(JwtService jwtService, UserManager<IdentityUser> userManager)
        {
            _jwtService = jwtService;
            _userManager = userManager;
        }

        public async Task<string> GetJwtString(IdentityUser identity, Guid userProfileId)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, identity.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, identity.Email),
                new Claim("IdentityId", identity.Id),
                new Claim("UserProfileId", userProfileId.ToString())
            });

            var userRoles = await _userManager.GetRolesAsync(identity);
            foreach (var role in userRoles)
            {
                claimsIdentity.AddClaim(new Claim("Role", role));
            }

            var token = _jwtService.CreateSecurityToken(claimsIdentity);

            return _jwtService.WriteToken(token);
        }
    }
}
