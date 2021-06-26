using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AssignmentManager.Server.Models;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace AssignmentManager.Server.Services
{
    public class ProfileService : IProfileService
    {
        protected UserManager<ApplicationUser> _userManager;
        
        public ProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //>Processing
            var user = await _userManager.GetUserAsync(context.Subject);
            var role = await _userManager.GetRolesAsync(user);
            string rolesString = String.Empty;
            if (role.Count == 1)
            {
                rolesString = role[0];
            } else
            {
                rolesString = String.Join(',', role);
            }
            var claims = new List<Claim>
            {
                new Claim("Name", user.UserName),
                new Claim("role", rolesString),
                new Claim("IsuId", user.IsuId.ToString()),
            };

            context.IssuedClaims.AddRange(claims);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var user = _userManager.GetUserAsync(context.Subject).Result;
            context.IsActive = user != null;
            return Task.FromResult(0);
        }
    }
}