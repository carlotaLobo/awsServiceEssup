using ModelsEssup.Models;
using ServiceEssupProject_Azure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ServiceEssupProject_Azure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SectorMaterialController : ControllerBase
    {
        IRepositoryEssup repositoryEssup;
        public SectorMaterialController(IRepositoryEssup repositoryEssup)
        {
            this.repositoryEssup = repositoryEssup;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<SectorMaterial>>> FindSectorMaterialBySectors([FromQuery] List<String> sector)
        {
            return await this.repositoryEssup.FindSectorMaterialBySectors(sector);
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<int>> DeleteSectorMaterialById(int id)
        {
            return await this.repositoryEssup.DeleteSectorMaterialById(id);
        }
        [HttpGet]
        [Route("[action]/{sector}")]
        public async Task<ActionResult<List<SectorMaterial>>> FindMaterialBySector(string sector)
        {
            return await this.repositoryEssup.FindMaterialBySector(sector);
        }
        [HttpGet]
        [Route("[action]/{material}/tasks")]
        public async Task<ActionResult<List<SectorMaterial>>> FindMaterialNewBySectorLogicApp(string material)
        {
 
            return Ok(
                new
                {
                    value = await this.repositoryEssup.FindMaterialNewByMaterial(material)
                }
                );
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SectorMaterial>> FindSectorMaterialById(int id)
        {
            return await this.repositoryEssup.FindSectorMaterialById(id);
        }
        [HttpGet]
        [Route("[action]/{sector}")]
        public async Task<ActionResult<List<SectorMaterial>>> FindSectorMaterialBySector(string sector)
        {
            return await this.repositoryEssup.FindSectorMaterialBySector(sector);
        }
        [HttpPost]
        public async Task<ActionResult<SectorMaterial>> InsertSectorMaterial(SectorMaterial sectorMaterial)
        {
            return await this.repositoryEssup.InsertSectorMaterial(sectorMaterial.Material, sectorMaterial.Sector);
        }
        [HttpPut]
        [Authorize]
        public async Task<SectorMaterial> UpdatetSectorMaterial(SectorMaterial sectorMaterial)
        {
            return await this.repositoryEssup.UpdatetSectorMaterial(sectorMaterial.Id, sectorMaterial.Material, sectorMaterial.Sector);
        }
    }
}
