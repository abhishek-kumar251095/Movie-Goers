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
    /*
     * This class contains the API methods
     * that communicate the Movies table in the DB.
     */

    [Route("api/[controller]")]
    [ApiController]
    public class MoviesAPIController : ControllerBase
    {
        public MoviesRepository _movieRepository;

        public MoviesAPIController(MoviesRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        //Returns the movie details given the movie-id in the DB.
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

        //Returns the movie details using the TMDB-Id.
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

        //Adds movie to the DB.
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

        //Deletes movie from the DB.
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

        //Gets the list of movies that are rated/unrated by admin depending upon the parameter.
        [HttpGet]
        [Route("movies/{isAdminRated}")]
        public async Task<ActionResult<IEnumerable<Movies>>> GetMovieByIsRatedAsync(int isAdminRated)
        {
            var movies = await _movieRepository.GetMovieByStatusAsync(isAdminRated == 1 ? true: false );
            if(movies == null)
            {
                return null;
            }
            return Ok(movies); 
        }

        [HttpPut]
        public async Task<ActionResult<Movies>> EditMovieStatusAsync(Movies movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _movieRepository.PutAsync(movie);
            return Ok(movie);

        }
    }
}