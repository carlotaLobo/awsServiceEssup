using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceEssupProject_Azure.Helpers
{
    public class HelperToken
    {
        public String Issuer { get; set; }
        public String Audience { get; set; }
        public String SecretKey { get; set; }

        public HelperToken(IConfiguration configuration)
        {
            this.Issuer = configuration["ApiOAuth:Issuer"];
            this.Audience = configuration["ApiOAuth:Audience"];
            this.SecretKey = configuration["ApiOAuth:SecretKey"];
        }
        public SymmetricSecurityKey GetKeyToken()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.SecretKey));
        }
        public Action<JwtBearerOptions> GetJwtBearerOptions()
        {
            return new Action<JwtBearerOptions>(
                o => o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = this.Issuer,
                    ValidAudience = this.Audience,
                    IssuerSigningKey = this.GetKeyToken()
                }
            );
        }
        public Action<AuthenticationOptions> GetAuthenticationOptions()
        {
            return new Action<AuthenticationOptions>(o=>

            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            );
        }
    }
}
