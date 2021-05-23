using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsEssup.Models;
using ServiceEssupProject_Azure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceEssupProject_Azure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectorController : ControllerBase
    {
        IRepositoryEssup repositoryEssup;
        public SectorController(IRepositoryEssup repositoryEssup)
        {
            this.repositoryEssup = repositoryEssup;
        }
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<int>> DeleteSectorById(string id)
        //{
        //    return await this.repositoryEssup.DeleteSectorById(id);
        //}
        [HttpGet]
        public async Task<ActionResult<List<Sector>>> FindAllSector()
        {
            return await this.repositoryEssup.FindAllSector();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Sector>> FindSectorById(string id)
        {
            return await this.repositoryEssup.FindSectorById(id);
        }
        //[HttpPost]
        //public async Task<ActionResult<Sector>> InsertSector(Sector sector)
        //{
        //    return await this.repositoryEssup.InsertSector(sector.sector);
        //}
        //[HttpPut]
        //public async Task<ActionResult<Sector>> UpdatetSector(Sector sector)
        //{
        //    return await this.repositoryEssup.UpdatetSector(sector.sector);
        //}
    }
}
