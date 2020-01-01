using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieGoersII.Handlers;
using MovieGoersII.Handlers.MoviesHandler;
using MovieGoersIIBL;

namespace MovieGoersII.Controllers.MoviesControllers
{
    public class MoviesDetailsController : Controller
    {
        IMoviesHandler _moviesHandler;
        ITmdbHandler _tmdbHandler;

        public MoviesDetailsController(IMoviesHandler moviesHandler, ITmdbHandler tmdbHandler)
        {
            _moviesHandler = moviesHandler;
            _tmdbHandler = tmdbHandler;
        }

        public async Task<IActionResult> DisplayMovies(int id)
        {
            Movies movie = await _moviesHandler.GetMovieByTmdbIdAsync(id);
            if(movie == null)
            {
                movie = await _tmdbHandler.MovieDetailsAsync(id);
            }
            return View(movie);
        }
    }
}