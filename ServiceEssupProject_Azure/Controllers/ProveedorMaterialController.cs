using ServiceEssupProject_Azure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelsEssup.Models;
using Microsoft.AspNetCore.Authorization;

namespace ServiceEssupProject_Azure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorMaterialController : ControllerBase
    {
        IRepositoryEssup repositoryEssup;

        public ProveedorMaterialController(IRepositoryEssup repositoryEssup)
        {
            this.repositoryEssup = repositoryEssup;
        }
        [HttpDelete]
        [Route("[action]/{id}")]
        [Authorize]
        public async Task<ActionResult<int>> DeleteProviderMaterialById(int id)
        {
           return await this.repositoryEssup.DeleteProviderMaterialById(id);
        }
        [HttpGet]
        [Route("[action]/{material}")]
        public async Task<ActionResult<List<ProvidersMaterialView>>> FindProviderByMaterialView(String material)
        {
           return await this.repositoryEssup.FindProviderByMaterialView(material);
        }
        [HttpGet]
        [Route("[action]/{material}")]
        public async Task<ActionResult<List<ProviderMaterial>>> FindProviderMaterialByMaterial(String material)
        {
            return await this.repositoryEssup.FindProviderMaterialByMaterial(material);
        }

        [HttpGet]
        [Route("[action]/{provider}")]
        public async Task<ActionResult<List<ProviderMaterial>>> FindMaterialByProvider(String provider)
        {
            return await this.repositoryEssup.FindMaterialByProvider(provider);
        }
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<ProviderMaterial>> FindProviderMaterialById(int id)
        {
            return await this.repositoryEssup.FindProviderMaterialById(id);
        }
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<List<ProviderMaterial>>> FindProviderMaterialByProvider(String id)
        {
            return await this.repositoryEssup.FindProviderMaterialByProvider(id);
        }
        [HttpPost]
        public async Task<ActionResult<ProviderMaterial>> InsertProviderMaterial(ProviderMaterial providerMaterial)
        {
            return await this.repositoryEssup.InsertProviderMaterial(providerMaterial.Id_Providers, providerMaterial.Id_Material);
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ProviderMaterial>> UpdatetProviderMaterial(ProviderMaterial providerMaterial)
        {
            return await this.repositoryEssup.UpdatetProviderMaterial(providerMaterial.Id, providerMaterial.Id_Providers, providerMaterial.Id_Material);
        }


    }
}
