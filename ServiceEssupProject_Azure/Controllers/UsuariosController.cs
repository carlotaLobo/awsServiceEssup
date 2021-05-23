using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsEssup.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ServiceEssupProject_Azure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public ActionResult<Usuarios> GetUsuario()
        {
            List<Claim> claims = HttpContext.User.Claims.ToList();
            return JsonConvert.DeserializeObject<Usuarios>(claims.SingleOrDefault(x => x.Type == "UserData").Value);
        }
    }
}
