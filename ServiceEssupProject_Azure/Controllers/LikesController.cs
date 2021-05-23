using ServiceEssupProject_Azure.Extensions;
using ServiceEssupProject_Azure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ModelsEssup.Models;
using Microsoft.AspNetCore.Authorization;

namespace ServiceEssupProject_Azure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        IRepositoryEssup repositoryEssup;
        public LikesController(IRepositoryEssup repository)
        {
            this.repositoryEssup = repository;
        }
        [HttpDelete("{idcompany}/{idProvider}")]
        [Authorize]
        public async Task<ActionResult<int>> DeleteLike(String idcompany, String idProvider)
        {
            return await this.repositoryEssup.DeleteLike(idcompany, idProvider);
        }
        [HttpPost]
        public async Task<ActionResult<Likes>> InsertLike(Likes like)
        {
            return await this.repositoryEssup.InsertLikes(like.Id_Provider, like.Id_Company);
        }
        [HttpGet("{business}")]
        public async Task<ActionResult<List<Likes>>> GetLikesBusiness(String business)
        {
            return await this.repositoryEssup.FindLikesByBusiness(business);
        }
        [HttpGet]
        [Route("[action]/{email}/{posicion}/{registros}")]
        public ActionResult<List<Likes>> LikesBusinessPaginacio(String email, int posicion, int registros)
        {
            return this.repositoryEssup.LikesBusinessPaginacion(email, posicion, ref registros);
        }
        [HttpGet]
        [Route("[action]/{provider}")]
        public async Task<ActionResult<List<Provider>>> FindLikesBusiness_Providers(string provider)
        {
            return await this.repositoryEssup.FindLikesBusiness(provider);
        }
        [HttpGet]
        [Route("[action]/{business}/{provider}")]
        public async Task<ActionResult<Likes>> FindLikesByProviderAndBusiness(Likes likes) 
        {
            return await this.repositoryEssup.FindLikesByProviderAndBusiness(likes.Id_Company, likes.Id_Provider);
        }
        [HttpGet]
        [Route("[action]/{business}")]
        public async Task<ActionResult<List<Likes>>> FindLikesByBusiness(string business) 
        {

            return await this.repositoryEssup.FindLikesByBusiness(business);
        
        }
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<Likes>> FindLikeById(int id)
        {
            return await this.repositoryEssup.FindLikeById(id);
        }
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<ActionResult<int>> DeleteLike(int id) 
        {
            return await this.repositoryEssup.DeleteLike(id);
        }
        [HttpDelete]
        [Route("[action]/{business}/{provider}")]
        [Authorize]
        public async Task<ActionResult<int>> DeleteLikeByBusinessAndProvider(Likes likes)
        {
            return await this.repositoryEssup.DeleteLike(likes.Id_Company, likes.Id_Provider);
        }
    }
}
