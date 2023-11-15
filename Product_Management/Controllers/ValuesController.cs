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
    public class ValuesController : ApiController
    {
        [HttpGet]
        public Object GetToken()
        {
            string key = "vvvvvvvvvvvvvvvvvvvvvvvvviiiiiiiiiiiiiiijjjjjjjjjjaaaaaaaaayyyyyyyyy"; // Secret key with sufficient length    
            var issuer = "http://mysite.com";  // Normally this will be your site URL    

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Create a List of Claims
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim("username", "vijaykishor06")); // Replace with the actual username
            permClaims.Add(new Claim("password", "unnakuyennapa")); // Replace with the actual password

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

        [Authorize]
        [HttpPost]
        public IHttpActionResult GetName1()
        {
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    // You can access claims here
                }
                return Ok("Valid");
            }
            else
            {
                return Unauthorized();
            }
        }

        [Authorize]
        [HttpPost]
        public Object GetName2()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var username= claims.Where(p => p.Type == "username").FirstOrDefault()?.Value;
                return new
                {
                    data = username
                };

            }
            return null;
        }
    }
}
