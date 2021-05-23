using Microsoft.AspNetCore.Authorization;
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
    public class MaterialController : ControllerBase
    {
        IRepositoryEssup repositoryEssup;
        public MaterialController(IRepositoryEssup repositoryEssup)
        {
            this.repositoryEssup = repositoryEssup;
        }
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult< Materials>> FindMaterialById(string id)
        {
            return await this.repositoryEssup.FindMaterialById(id);
        }
        [HttpDelete]
        [Route("[action]/{id}")]
        [Authorize]
        public async Task<ActionResult<int>> DeleteMaterialById(string id)
        {
            return await this.repositoryEssup.DeleteMaterialById(id);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Materials>>> FindAllMaterials()
        {
            return await this.repositoryEssup.FindAllMaterials();
        }
        [HttpGet]
        [Route("[action]/task")]
        public async Task<ActionResult<List<Materials>>> FindNewMaterials()
        {
            return Ok(new { value= await this.repositoryEssup.FindNewMaterials()}) ;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<Materials>> InsertMaterials(Materials materials)
        {
            return await this.repositoryEssup.InsertMaterials(materials.Material);
        }

    }
}
