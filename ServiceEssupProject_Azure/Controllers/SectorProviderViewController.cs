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
    public class SectorProviderViewController : ControllerBase
    {
        IRepositoryEssup repo;

        public SectorProviderViewController(IRepositoryEssup repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        [Route("[action]/{sector}")]
        public async Task<ActionResult<List<SectorProviderView>>> FindProviderBySectorView(string sector)
        {
            return await this.repo.FindProviderBySectorView(sector);
        }
        [HttpGet]
        [Route("[action]/{provider}")]
        public async Task<ActionResult<List<SectorProviderView>>> FindSectorsProviderViewByProvider(string provider)
        {
            return await this.repo.FindSectorsProviderViewByProvider(provider);
        }

    }
}
