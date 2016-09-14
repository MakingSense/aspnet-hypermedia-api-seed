using MakingSense.AspNetCore.HypermediaApi.ExceptionHandling;
using MakingSense.AspNetCore.HypermediaApi.Seed.Problems;
using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MakingSense.AspNetCore.HypermediaApi.Seed
{
    public class SeedTokenValidator : ISecurityTokenValidator
    {
        public const string DEMO_TOKEN = "1234";
        public const string DEMO_USERNAME = "demo";
        public const string DEMO_EMAIL = "demo@mail.com";

        public int MaximumTokenSizeInBytes { get; set; }

        public bool CanValidateToken => true;

        public bool CanReadToken(string securityToken) => true;

        public SeedTokenValidator()
        {
            // Inject dependencies here
        }

        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            validatedToken = null;

            if (securityToken == DEMO_TOKEN)
            {
                // TODO: Complete all claims and other user data
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, DEMO_USERNAME),
                    new Claim(ClaimsIdentity.DefaultNameClaimType, DEMO_USERNAME),
                    new Claim(ClaimTypes.Email, DEMO_EMAIL)
                };
                return new ClaimsPrincipal(new ClaimsIdentity(claims, "Bearer") { BootstrapContext = securityToken });
            }
            else
            {
                throw new ApiException(new InvalidTokenProblem());
            }
        }
    }
}
