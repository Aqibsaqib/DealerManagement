using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace DealerManagment.Models
{
    public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
    {
        public MyUserClaimsPrincipalFactory(
           UserManager<ApplicationUser> userManager,
           IOptions<IdentityOptions> optionsAccessor)
           : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            // Get user properties that are needed
            identity.AddClaim(new Claim("DealershipName", user.DealershipName ?? ""));
            return identity;
        }

    }
}
