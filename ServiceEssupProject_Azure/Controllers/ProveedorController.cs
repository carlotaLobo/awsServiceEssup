using ServiceEssupProject_Azure.Extensions;
using ServiceEssupProject_Azure.Helpers;
using ModelsEssup.Models;
using ServiceEssupProject_Azure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;

namespace ServiceEssupProject_Azure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        IRepositoryEssup repository;

        public ProveedorController(IRepositoryEssup repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<Provider>> InsertProvider(Provider provider)
        {
            return await this.repository.InsertProvider(provider.Mail,provider.Name,provider.Address,provider.Population,provider.Cp, provider.Tlf, 
                provider.Pwd, provider.Description, provider.Img1, provider.Img2, provider.Img3);
        }
        [HttpGet]
        [Route("[action]/{email}")]
        public async Task<ActionResult<Provider>> GetProvider(String email)
        {
            return await this.repository.FindProviderById(email);
        }
        [HttpPut]
        [Route("[action]")]
        [Authorize]
        public async Task<ActionResult<Provider>> UpdateProvider(Provider provider)
        {
            return await this.repository.UpdatetProvider(provider.Mail, provider.Name, provider.Address, provider.Population, provider.Cp, provider.Tlf,
                provider.Pwd, provider.Description, provider.Img1, provider.Img2, provider.Img3);
        }

    }
}
