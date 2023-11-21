using Microsoft.IdentityModel.Tokens;
using Product_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Product_Management.Controllers
{
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        private const string SecretKey = "vvvvvvvvvvvvvvvvvvvvvvvvviiiiiiiiiiiiiiijjjjjjjjjjaaaaaaaaayyyyyyyyy";
        private readonly SymmetricSecurityKey _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

        [HttpPost]
        [Route("validatecredentials")]
        public IHttpActionResult ValidateCredentials([FromBody] UserInput model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.username) || string.IsNullOrWhiteSpace(model.password))
            {
                return BadRequest("Invalid input");
            }

            // Validate the credentials using your logic
            var isValid = ValidateUserCredentials(model.username, model.password);

            if (isValid)
            {
                // If credentials are valid, generate and return the JWT token
                var token = GenerateToken(model.username, model.password);
                return Ok(new { token });
            }

            return BadRequest("Invalid credentials");
        }

        private string GenerateToken(string username, string password)
        {
            var credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = GetUserClaimsFromDatabase(username, password);

            var token = new JwtSecurityToken(
                issuer: "http://mysite.com",
                audience: "http://mysite.com",
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(1), // Set expiration time to 1 minute
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private List<Claim> GetUserClaimsFromDatabase(string username, string password)
        {
            using (var context = new Users_context())
            {
                var user = context.UserCredentials.FirstOrDefault(u => u.username == username && u.password == password);
                if (user != null)
                {
                    return new List<Claim>
                    {
                        new Claim("username", user.username),
                        // new Claim("password", user.password),
                        new Claim(ClaimTypes.Role, Enum.GetName(typeof(RoleEnum), user.role))
                    };
                }
                return null;
            }
        }

        private bool ValidateUserCredentials(string username, string password)
        {
            using (var context = new Users_context())
            {
                var user = context.UserCredentials.FirstOrDefault(u => u.username == username && u.password == password);
                return user != null;
            }
        }

        [HttpGet]
        [Route("validateToken")]
        public IHttpActionResult ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetTokenValidationParameters();

            try
            {
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return Ok("Token is valid!");
            }
            catch (SecurityTokenExpiredException)
            {
                // Token has expired
                // Generate a new JWT token and return it along with the response
                var newToken = GenerateTokenForExpiredToken(token);
                return Ok(new { token = newToken, message = "Token refreshed successfully!" });
            }
            catch (Exception)
            {
                return BadRequest("Invalid token!");
            }
        }

        private string GenerateTokenForExpiredToken(string expiredToken)
        {
            // Extract claims from the expired token
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(expiredToken) as JwtSecurityToken;

            // Use the claims from the expired token to generate a new JWT token
            var usernameClaim = jwtToken?.Claims.FirstOrDefault(c => c.Type == "username")?.Value;

            if (usernameClaim != null)
            {
                // Generate a new token using the claims from the expired token
                var context = new Users_context();
                var user = context.UserCredentials.FirstOrDefault(u => u.username == usernameClaim);
                var expires = DateTime.Now.AddMinutes(2);
                var newToken = new JwtSecurityToken(
           issuer: "http://mysite.com",
           audience: "http://mysite.com",
           claims: GetUserClaimsFromDatabase(usernameClaim, user.password),
           expires: expires, // Set expiration time to 2 minutes
           signingCredentials: new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256)
       );
                return GenerateToken(usernameClaim, user.password);
            }

            return null;
        }      

        private TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "http://mysite.com",
                ValidateAudience = true,
                ValidAudience = "http://mysite.com",
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _securityKey,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        }
    }
}
