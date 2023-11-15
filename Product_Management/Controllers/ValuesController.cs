using Product_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;


namespace Product_Management.Controllers
{
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        [HttpGet]
        public Object GetToken(string name)
        {
            string key = "vvvvvvvvvvvvvvvvvvvvvvvvviiiiiiiiiiiiiiijjjjjjjjjjaaaaaaaaayyyyyyyyy"; // Secret key with sufficient length    
            var issuer = "http://mysite.com";  // Normally this will be your site URL    

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Create a List of Claims
            var permClaims = GetUserClaimsFromDatabase(name);
           /* var permClaims = new List<Claim>();
            permClaims.Add(new Claim("username", "vijaykishor06")); // Replace with the actual username
            permClaims.Add(new Claim("password", "unnakuyennapa")); // Replace with the actual password
*/
            // Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: issuer, // You can set it to the same value as issuer or your app's client ID
                claims: permClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return new { data = jwt_token };
        }

        private List<Claim> GetUserClaimsFromDatabase(string username)
        {
            using(var context=new Users_context())
            {
                var user = context.UserCredentials.FirstOrDefault(u => u.username == username);
                if(user != null)
                {
                    return new List<Claim>
                    {
                        new Claim("username",user.username),
                        new Claim("password",user.password)
                    };
                }
                return null;
            }
        }


        [HttpPost]
        [Route("validatecredentials")]
        public IHttpActionResult ValidateCredentials([FromBody] UserCredentials model)
        {

            if (model == null || string.IsNullOrWhiteSpace(model.username) || string.IsNullOrWhiteSpace(model.password))
            {
                return BadRequest("Invalid input");
            }

            // Validate the credentials using your logic
            var isValid = ValidateUserCredentials(model.username, model.password);
            if (isValid)
            {
                return Ok("Credentials are valid");
            }
            return BadRequest("Invalid credentials");
        }

        private bool ValidateUserCredentials(string username, string password)
        {
            var tokenUsernameClaim = ((ClaimsIdentity)User.Identity).FindFirst("username")?.Value;
            var tokenPasswordClaim = ((ClaimsIdentity)User.Identity).FindFirst("password")?.Value;
            return username == tokenUsernameClaim && tokenPasswordClaim == password;
        }       
    }
}


