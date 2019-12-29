using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieGoersII.Handlers;
using MovieGoersII.Handlers.MoviesHandler;
using MovieGoersII.Handlers.UserCollectionHandler;
using MovieGoersII.ViewModels;
using MovieGoersIIBL;
using Newtonsoft.Json;

namespace MovieGoersII.Controllers
{
    public class SearchController : Controller
    {
        ITmdbHandler _handler;
        IUserCollectionHandler _collectionHandler;
        IMoviesHandler _moviesHandler;

        public SearchController(ITmdbHandler handler, IUserCollectionHandler collectionHandler, IMoviesHandler moviesHandler)
        {
            DotNetEnv.Env.Load();
            _handler = handler;
            _collectionHandler = collectionHandler;
            _moviesHandler = moviesHandler;
        }

        public IActionResult SearchMovies(SearchCriteriaViewModel searchCriteria)
        {
            return View(searchCriteria);
        }

        public async Task<IActionResult> SearchForMovies(string page, string searchQuery)
        {
            ViewBag.page = page;
            ViewBag.searchQuery = searchQuery;
            int userId = DotNetEnv.Env.GetInt("User_Id");
            var result = await _handler.MovieSearchListAsync(searchQuery, page);

            foreach(var item in result)
            {
                if(await _collectionHandler.CheckCollectionForMovieAsync(item.MovieId,userId))
                {
                    item.ButtonTask.Text = "-";
                    item.ButtonTask.Action = "RemoveFromCollection";
                    item.ButtonTask.Description = "Remove the movie from your collection";
                    item.ButtonTask.Controller = "Search";
                }
            }

            SearchCriteriaViewModel searchCriteriaViewModel = new SearchCriteriaViewModel
            {
                SearchModel = result.ToList()
            };
            
            return View("SearchMovies", searchCriteriaViewModel);
        }

        public async Task<IActionResult> AddToCollection(int id, string page, string searchQuery)
        {
            Movies movie = await _moviesHandler.GetMovieByTmdbIdAsync(id);

            if (movie == null)
            {
                var result = await _handler.MovieDetailsAsync(id);
                movie = await _moviesHandler.AddMovieToListAsync(result);
            }

            UserCollection collection = new UserCollection
            {
                MovieId = movie.Id,
                UserId = DotNetEnv.Env.GetInt("User_Id"),
                Rating = 4,
                Review = "Review Comments",
                IsSoftDeleted = false
            };

            await _collectionHandler.AddCollectionAsync(collection);
            return RedirectToAction("SearchForMovies", new { page = page, searchQuery = searchQuery });

        }

        public async Task<IActionResult> RemoveFromCollection(int id, string page, string searchQuery)
        {
            int userId = DotNetEnv.Env.GetInt("User_Id");
            var result = await _collectionHandler.RemoveCollectionAsync(id, userId);

            return RedirectToAction("SearchForMovies", new { page = page, searchQuery = searchQuery });
        }
    }
}