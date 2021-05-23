using ServiceEssupProject_Azure.Extensions;
using ModelsEssup.Models;
using ServiceEssupProject_Azure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ServiceEssupProject_Azure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorSectorController : ControllerBase
    {
        IRepositoryEssup repositoryEssup;

        public ProveedorSectorController(IRepositoryEssup repositoryEssup)
        {
            this.repositoryEssup = repositoryEssup;
        }
        [HttpGet]
        [Route("[action]/{sector}")]
        public async Task<ActionResult<List<SectorProvider>>> FindSectorProviderBySector(string sector)
        {
            return await this.repositoryEssup.FindSectorProviderBySector(sector);
        }
        [HttpGet]
        [Route("[action]/{provider}")]
        public async Task<ActionResult<List<SectorProvider>>> FindSectorByProvider(string provider)
        {
            return await this.repositoryEssup.FindSectorByProvider(provider);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SectorProvider>> FindSectorProviderById(int id)
        {
            return await this.repositoryEssup.FindSectorProviderById(id);
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<int>> DeleteSectorProviderById(int id)
        {
           return await this.repositoryEssup.DeleteSectorProviderById(id);
        }
        [HttpPost]
        public async Task<ActionResult<SectorProvider>> InsertSectorProvider(SectorProvider sectorProvider)
        {
            return await this.repositoryEssup.InsertSectorProvider(sectorProvider.Id_Provider, sectorProvider.Id_Sector);
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<SectorProvider>> UpdateSectorProvider(SectorProvider sectorProvider)
        {
            return await this.repositoryEssup.UpdatetSectorProvider(sectorProvider.Id, sectorProvider.Id_Provider, sectorProvider.Id_Sector);
        }
    }
}
