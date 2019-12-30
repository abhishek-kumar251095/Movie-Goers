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

    /*
     * This class controls the functionalities such as
     * searching for a movie,
     * adding a movie to users' collection,
     * removing the movie from collection
     * and rendering the home page.
     */

    public class SearchController : Controller
    {
        readonly ITmdbHandler _handler;
        readonly IUserCollectionHandler _collectionHandler;
        readonly IMoviesHandler _moviesHandler;

        public SearchController(ITmdbHandler handler, IUserCollectionHandler collectionHandler, IMoviesHandler moviesHandler)
        {
            DotNetEnv.Env.Load(); //The API Key is currenly stored in a .env file.
            _handler = handler;
            _collectionHandler = collectionHandler;
            _moviesHandler = moviesHandler;
        }

        //Renders the home page that displays users' collection.
        public IActionResult SearchMovies(SearchCriteriaViewModel searchCriteria)
        {
            return View(searchCriteria);
        }


        //Searches for movies using the TMDB API.
        public async Task<IActionResult> SearchForMovies(string page, string searchQuery)
        {
            ViewBag.page = page;
            ViewBag.searchQuery = searchQuery;
            int userId = DotNetEnv.Env.GetInt("User_Id");
            var result = await _handler.MovieSearchListAsync(searchQuery, page);

            foreach(var item in result)
            {
                //Check if a movie is already in the collection. If yes, the button for removing the collection goes with the details.
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


        //Adds movie to the users' collection using the TMDB Id for the movie.
        public async Task<IActionResult> AddToCollection(int id, string page, string searchQuery)
        {
            Movies movie = await _moviesHandler.GetMovieByTmdbIdAsync(id);

            // If the movie isn't already in the DB, fetch the details from the API and add it to the DB.
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

            //Redirect so that the same search page is displayed.
            return RedirectToAction("SearchForMovies", new { page = page, searchQuery = searchQuery });
        }


        //Removes the movie from users' collection.
        public async Task<IActionResult> RemoveFromCollection(int id, string page, string searchQuery)
        {
            int userId = DotNetEnv.Env.GetInt("User_Id");
            var result = await _collectionHandler.RemoveCollectionAsync(id, userId);

            //Redirect so that the same search page is displayed.
            return RedirectToAction("SearchForMovies", new { page = page, searchQuery = searchQuery });
        }
    }
}