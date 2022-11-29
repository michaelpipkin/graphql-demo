using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GraphQLDemoBase.Authorization
{
    public class TokenManager : DelegatingHandler, ITokenManager
    {
        public TokenInfo tokenInfo { get; set; }
        public TokenManager(TokenInfo info) {
            tokenInfo = info;
        }

        protected async override Task<HttpResponseMessage> SendAsync(
         HttpRequestMessage request, CancellationToken cancellationToken) {
            Console.WriteLine("TOken Manager processing the request");
            // Calling the inner handler
            var response = await base.SendAsync(request, cancellationToken);
            Console.WriteLine("TOken Manager processing the response");
            return response;
        }
 
        public bool ValidateCurrentToken(string token) {
            var mySecret = tokenInfo.JWTSecretKey;
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var myIssuer = tokenInfo.JWTIssuer;
            var myAudience = tokenInfo.JWTAudience;

            var tokenHandler = new JwtSecurityTokenHandler();
            try {
                tokenHandler.ValidateToken(token, new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = myIssuer,
                    ValidAudience = myAudience,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            }
            catch {
                return false;
            }
            return true;
        }

        public string GenerateToken(int userId) {
            var mySecret = tokenInfo.JWTSecretKey;
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var myIssuer = tokenInfo.JWTIssuer;
            var myAudience = tokenInfo.JWTAudience;

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserRole", "DHSUser"),
					//add new claims for each role the user has according to the roles table?
				}),
                Expires = DateTime.UtcNow.AddMinutes(tokenInfo.JWTExpiration),
                Issuer = myIssuer,
                Audience = myAudience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
