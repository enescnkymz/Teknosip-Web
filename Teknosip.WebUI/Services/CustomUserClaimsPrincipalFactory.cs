using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Teknosip.Domain.Entities;

namespace Teknosip.WebUI.Services
{
	public class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, AppRole>
	{
		public CustomUserClaimsPrincipalFactory(
			UserManager<AppUser> userManager,
			RoleManager<AppRole> roleManager,
			IOptions<IdentityOptions> optionsAccessor)
			: base(userManager, roleManager, optionsAccessor)
		{
		}

		protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
		{
			var identity = await base.GenerateClaimsAsync(user);

			
			if (user.UserType != null)
			{
				identity.AddClaim(new Claim(ClaimTypes.Role, user.UserType.ToString()));
			}
			identity.AddClaim(new Claim("UserEmail", user.Email ?? "Mail Bulunamadı"));
            identity.AddClaim(new Claim("PhotoUrl", user.ProfilePhoto ?? "/images/profiles/default.webp"));

			return identity;
		}
	}
}
