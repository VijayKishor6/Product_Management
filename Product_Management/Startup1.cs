using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Tokens;
using System.Text;

[assembly: OwinStartup(typeof(Product_Management.Startup))]

namespace Product_Management
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var issuer = "http://mysite.com";
            var key = "vvvvvvvvvvvvvvvvvvvvvvvvviiiiiiiiiiiiiiijjjjjjjjjjaaaaaaaaayyyyyyyyy";

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero // Adjust the clock skew if needed
                }
            });
        }
    }
}
