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
    public class ProviderMaterialViewController : ControllerBase
    {
        IRepositoryEssup repo;

        public ProviderMaterialViewController(IRepositoryEssup repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        [Route("[action]/{material}")]
        public async Task<ActionResult<List<ProvidersMaterialView>>> FindProviderByMaterialView(String material)
        {
            return await this.repo.FindProviderByMaterialView(material);
        }
        [HttpGet]
        [Route("[action]/{material}/tasks")]
        public async Task<ActionResult<List<ProvidersMaterialView>>> FindProviderByMaterialViewLogicApp(String material)
        {
            return Ok(new
            {
                value = await this.repo.FindProviderByMaterialView(material)
            });
        }

    }
}
