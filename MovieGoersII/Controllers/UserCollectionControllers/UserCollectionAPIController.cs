using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieGoersIIBL;
using MovieGoersIIDAL.Services.Repositories;

namespace MovieGoersII.Controllers.UserCollectionControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCollectionAPIController : ControllerBase
    {
        readonly UserCollectionRepository _collectionRepository;

        public UserCollectionAPIController(UserCollectionRepository collectionRepository)
        {
            _collectionRepository = collectionRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserCollection>> GetCollectionByIdAsync(int id)
        {
            var collection = await _collectionRepository.GetByIdAsync(id);
            if (collection == null)
            {
                return BadRequest();
            }
            return Ok(collection);
        }

        [HttpPost]
        public async Task<ActionResult<UserCollection>> CreateCollectionAsync(UserCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _collectionRepository.PostAsync(collection);

            return Ok(collection);
        }

        [HttpPut]
        public async Task<ActionResult<UserCollection>> SoftDeleteCollectionAsync(UserCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _collectionRepository.SoftDeleteCollectionAsync(collection);
            return Ok(collection);

        }

        [HttpGet]
        [Route("User/{userId}")]
        public async Task<ActionResult<IEnumerable<UserCollection>>> GetCollectionByUserIdAsync(int userId)
        {
            var collection = await _collectionRepository.GetCollectionByUserIdAsync(userId);
            if(collection == null)
            {
                return BadRequest();
            }

            return Ok(collection);
        }
    }
}