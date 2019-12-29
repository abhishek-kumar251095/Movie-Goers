using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieGoersII.Handlers;
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

        public SearchController(ITmdbHandler handler, IUserCollectionHandler collectionHandler)
        {
            _handler = handler;
            _collectionHandler = collectionHandler;
        }

        public IActionResult SearchMovies(SearchCriteriaViewModel searchCriteria)
        {
            return View(searchCriteria);
        }

        public async Task<IActionResult> SearchForMovies(string page, string searchQuery)
        {
            var result = await _handler.MovieSearchList(searchQuery, page);
            SearchCriteriaViewModel searchCriteriaViewModel = new SearchCriteriaViewModel
            {
                SearchModel = result.ToList()
            };
            
            return View("SearchMovies", searchCriteriaViewModel);
        }
    }
}