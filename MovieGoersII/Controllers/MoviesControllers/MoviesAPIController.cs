using System;
using System.Collections.Generic;
using System.Linq;
using MovieGoersIIBL;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieGoersIIDAL.Services;
using MovieGoersIIDAL.Services.Repositories;
using MovieGoersII.Handlers;

namespace MovieGoersII.Controllers.MoviesControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesAPIController : ControllerBase
    {
        public MoviesRepository _movieRepository;

        public MoviesAPIController(MoviesRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Movies>>> GetMovieByIdAsync(int id)
        { 
            var movie = await _movieRepository.GetByIdAsync(id);
            if(movie == null)
            {
                return null;
            }
            return Ok(movie);
        }

        [HttpGet]
        [Route(("tmdb/{id}"))]
        public async Task<ActionResult<IEnumerable<Movies>>> GetMovieByTmdbIdAsync(int id)
        {
            var movie = await _movieRepository.GetMovieByTmdbIdAsync(id);
            if (movie == null)
            {
                return null;
            }
            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<Movies>> AddMovieAsync(Movies movie)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _movieRepository.PostAsync(movie);
            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Movies>> DeleteMovieAsync(int id)
        {
            var movie = await _movieRepository.DeleteAsync(id);
            if (movie == null)
            {
                return BadRequest();
            }
            return Ok(movie);
        }

        [HttpGet]
        [Route("Movies/{isAdminRated}")]
        public async Task<ActionResult<IEnumerable<Movies>>> GetMovieByIsRatedAsync(int isAdminRated)
        {
            var movies = await _movieRepository.GetMovieByStatusAsync(isAdminRated == 1 ? true: false );
            if(movies == null)
            {
                return BadRequest();
            }
            return Ok(movies); 
        }
    }
}