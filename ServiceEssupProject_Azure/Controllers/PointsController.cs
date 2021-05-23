using ServiceEssupProject_Azure.Extensions;
using ModelsEssup.Models;
using ServiceEssupProject_Azure.Repositories;
using Microsoft.AspNetCore.Http;
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
    public class PointsController : ControllerBase
    {
        IRepositoryEssup repositoryEssup;
        public PointsController(IRepositoryEssup repositoryEssup)
        {
            this.repositoryEssup = repositoryEssup;
        }
        [HttpPost]
        public async Task<Point> InsertPointWithoutPoint(Point point)
        {
            return await this.repositoryEssup.InsertPoint(point.Id_Provider, point.Id_Company);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<Point>> InsertPointWithoutPointLogicApp(Point point)
        {
            var model = new
            {
                valor = await this.repositoryEssup.InsertPoint(point.Id_Provider, point.Id_Company)
            };
            return Ok(
               model
                );
        }
        [HttpPut]
        [Authorize]
        public async Task<Point> UpdatePoint(Point point)
        {
           return await this.repositoryEssup.UpdatetPoint(point.Id, point.Id_Provider, point.Id_Company, point.Points);
        }
        [HttpGet]
        [Route("[action]/{provider}")]
        public async Task<ActionResult<int>> GetPoint(String provider)
        {
            return await this.repositoryEssup.FindPointByProvider(provider);
        }
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<Point>> FindPointById(int id)
        {
            return await this.repositoryEssup.FindPointById(id);
        }
        [HttpGet]
        [Route("[action]/{business}")]
        public async Task<ActionResult<List<Point>>> Find0PointByBusiness(String business)
        {
            return await this.repositoryEssup.Find0PointByBusiness(business);
        }

    }
}
