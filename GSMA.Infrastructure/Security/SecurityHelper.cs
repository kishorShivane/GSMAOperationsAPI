using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace GSMA.Infrastructure.Security
{
    public class SecurityHelper
    {
        private string OAuthToken;
        public SecurityHelper()
        {

        }

        public static string GetOAuthTokenFromHeader(IHeaderDictionary headers)
        {
            var token = "";
            if (headers != null && headers.ContainsKey("Authorization"))
            {
                token = headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
            }
            return token;
        }

        public static string GetClaim(string token, string claimType)
        {
            var stringClaimValue = "";
            if (!string.IsNullOrEmpty(token))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
            }
            return stringClaimValue;
        }

    }
}
