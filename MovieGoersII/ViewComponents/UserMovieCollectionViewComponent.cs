using Microsoft.AspNetCore.Mvc;
using MovieGoersII.Handlers.MoviesHandler;
using MovieGoersII.Handlers.UserCollectionHandler;
using MovieGoersIIBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieGoersII.ViewComponents
{
    public class UserMovieCollectionViewComponent: ViewComponent
    {
        IMoviesHandler _moviesHandler;
        IUserCollectionHandler _collectionHandler;
        public UserMovieCollectionViewComponent(IMoviesHandler moviesHandler, IUserCollectionHandler collectionHandler)
        {
            _moviesHandler = moviesHandler;
            _collectionHandler = collectionHandler;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetUsersMoviesAsync();
            return View(items);
        }

        public async Task<IEnumerable<Movies>> GetUsersMoviesAsync()
        {
            var collection = await _collectionHandler.GetCollectionByUserIdAsync(1);
            List<Movies> movies = new List<Movies>();
            foreach (var item in collection)
            {
                var res = await _moviesHandler.GetMovieByIdAsync(item.MovieId);
                Movies movie = new Movies
                {
                    TMDBId = res.TMDBId,
                    Title = res.Title
                };
                movies.Add(movie);
            }
            return movies;
        }
    }
}
