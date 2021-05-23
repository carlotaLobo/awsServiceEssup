using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ModelsEssup.Models;
using Newtonsoft.Json;
using ServiceEssupProject_Azure.Helpers;
using ServiceEssupProject_Azure.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ServiceEssupProject_Azure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        IRepositoryEssup repo;
        HelperToken helperToken;
        public LoginController(IRepositoryEssup repo, HelperToken helperToken)
        {
            this.repo = repo;
            this.helperToken = helperToken;
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuario usuario)
        {
            Usuarios usuarios = await this.repo.Login(usuario.Mail, usuario.Pwd);
            if (usuarios == null)
            {
                return Unauthorized();
            }
            String usuariojson = JsonConvert.SerializeObject(usuarios);
            Claim[] claims = new[] { new Claim("UserData", usuariojson) };
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: this.helperToken.Issuer,
                audience: this.helperToken.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(this.helperToken.GetKeyToken(), SecurityAlgorithms.HmacSha256)
                );
            return Ok(
                new
                {
                    response = new JwtSecurityTokenHandler().WriteToken(token)
                }
                );

        }
    }
}
