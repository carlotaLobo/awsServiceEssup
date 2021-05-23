using ServiceEssupProject_Azure.Extensions;
using ServiceEssupProject_Azure.Helpers;
using ServiceEssupProject_Azure.Repositories;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ModelsEssup.Models;
using Microsoft.AspNetCore.Authorization;

namespace ServiceEssupProject_Azure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {

        IRepositoryEssup repositoryEssup;

        public BusinessController(IRepositoryEssup repositoryEssup)
        {
            this.repositoryEssup = repositoryEssup;

        }
    
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<Business>> InsertBusiness(Business business)
        {
            return await this.repositoryEssup.InsertBusiness(business.Mail, business.Name, business.Address, business.Population, business.Cp, business.Sector, business.Tlf, business.Pwd);
        }
        [HttpGet]
        [Route("[action]/{mail}")]
        public async Task<ActionResult<Business>> GetBusiness(String mail)
        {
            return await this.repositoryEssup.FindBussinesById(mail);
        }
       
        [HttpPut]
        [Route("[action]")]
        [Authorize]
        public async Task<ActionResult<Business>> UpdateBusiness(Business business)
        {
          return await repositoryEssup.UpdatetBusiness(business.Mail, business.Name, business.Address, business.Population, business.Cp, business.Sector, business.Tlf, business.Pwd);
        }
    }
}
