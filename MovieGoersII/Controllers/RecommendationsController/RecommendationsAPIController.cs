using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieGoersIIBL;
using MovieGoersIIDAL;
using MovieGoersIIDAL.Services.Repositories;

namespace MovieGoersII.Controllers.RecommendationsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationsAPIController : ControllerBase
    {
        RecommendationsRepository _recommendationsRepository;

        public RecommendationsAPIController(RecommendationsRepository recommendationsRepository)
        {
            _recommendationsRepository = recommendationsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recommendations>>> GetRecommendationRatingsAsync()
        {
            return Ok(await _recommendationsRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Recommendations>> GetRecommendationRatingsByIdAsync(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }

            return Ok(await _recommendationsRepository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<Recommendations>> PostRecommendationRatingsAsync(Recommendations recommendation)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(await _recommendationsRepository.PostAsync(recommendation));
        }

        [HttpDelete]
        public async Task<ActionResult<Recommendations>> DeleteRecommendationRatingsAsync(int id)
        {
            var recommendation = await _recommendationsRepository.DeleteAsync(id);
            if(recommendation == null)
            {
                return BadRequest();
            }

            return Ok(recommendation);
        }

        [HttpGet]
        [Route("Movie/{movieId}")]
        public async Task<ActionResult<Recommendations>> GetRecommendationRatingsByMovieIdAsync(int movieId)
        {
            return Ok(await _recommendationsRepository.GetRecommendationsByMovieId(movieId));
        }
    }
}