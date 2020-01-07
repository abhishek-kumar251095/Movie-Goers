using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieGoersII.Handlers.MoviesHandler;
using MovieGoersII.Handlers.RecommendationHandler;
using MovieGoersII.Handlers.UserCollectionHandler;
using MovieGoersII.ViewModels;
using MovieGoersIIBL;

namespace MovieGoersII.Controllers.RecommendationsController
{
    public class RecommendationsController : Controller
    {
        IRecommendationsHandler _recommendationsHandler;
        IMoviesHandler _moviesHandler;
        IUserCollectionHandler _collectionHandler;

        public RecommendationsController(IRecommendationsHandler recommendationsHandler, IMoviesHandler moviesHandler, IUserCollectionHandler collectionHandler)
        {
            _recommendationsHandler = recommendationsHandler;
            _moviesHandler = moviesHandler;
            _collectionHandler = collectionHandler;
        }


        public async Task<IActionResult> UserRecommendations()
        {
            Recommendations recommendationsInit;
            
            var movieRatings = await _recommendationsHandler.GetRecommendationRatingsAsync();
            var res = await _collectionHandler.GetCollectionByUserIdAsync(1);
            int rateParameter = 0;
            List<Movies> userMovies = new List<Movies>();

            foreach(var item in res.ToList())
            {
                userMovies.Add(await _moviesHandler.GetMovieByIdAsync(item.MovieId));
            }

            List<Movies> recommendedMovies = new List<Movies>();
            
            
            foreach(var userMovie in userMovies.Where(o => o.IsAdminRated))
            {
                recommendationsInit = await _recommendationsHandler.GetRecommendationRatingsByMovieIdAsync(userMovie.Id);

                foreach (var item in movieRatings.Where(o => o.Id != recommendationsInit.Id))
                {
                    rateParameter = 0;
                    if (Math.Abs(item.InformationRequired - recommendationsInit.InformationRequired) <= 1)
                    {
                        rateParameter += item.InformationRequired == recommendationsInit.InformationRequired ? 2 : 1;
                    }
                    if (Math.Abs(item.SpecialEffects - recommendationsInit.SpecialEffects) <= 1)
                    {
                        rateParameter += item.SpecialEffects == recommendationsInit.SpecialEffects ? 2 : 1;
                    }
                    if (Math.Abs(item.Suspense - recommendationsInit.Suspense) <= 1)
                    {
                        rateParameter += item.Suspense == recommendationsInit.Suspense ? 2 : 1;
                    }
                    if (Math.Abs(item.Intensity - recommendationsInit.Intensity) <= 1)
                    {
                        rateParameter += item.Intensity == recommendationsInit.Intensity ? 2 : 1;
                    }
                    if (Math.Abs(item.Pace - recommendationsInit.Pace) <= 1)
                    {
                        rateParameter += item.Pace == recommendationsInit.Pace ? 2 : 1;
                    }
                    if (Math.Abs(item.Violence - recommendationsInit.Violence) <= 1)
                    {
                        rateParameter += item.Violence == recommendationsInit.Violence ? 2 : 1;
                    }


                    if (!res.ToList().Where(o => o.MovieId == item.MovieId).Any() 
                            && rateParameter >= 10
                            && !recommendedMovies.ToList().Where(m => m.Id == item.MovieId).Any())
                    {
                        recommendedMovies.Add(await _moviesHandler.GetMovieByIdAsync(item.MovieId));
                    }
                }
            }
            
            return View(recommendedMovies);
        }

        public async Task<IActionResult> AddRatingsPage()
        {
            var movies = await _moviesHandler.GetMoviesByStatusAsync(0);
            List<SelectListItem> moviesSelectList = new List<SelectListItem>();
            SelectListItem movieItem;
            movies.ToList().ForEach(item =>
            {
                movieItem = new SelectListItem();
                movieItem.Text = item.Title;
                movieItem.Value = Convert.ToString(item.Id);
                moviesSelectList.Add(movieItem);
            });
            
            return View(new RecommendationsViewModel { MoviesList = moviesSelectList});
        }

        public async Task<IActionResult> AddRecommendationRatings(RecommendationsViewModel recommendations)
        {
            Recommendations recommendationRatings = new Recommendations
            {
                MovieId = recommendations.MovieId,
                Intensity = recommendations.Intensity,
                InformationRequired = recommendations.InformationRequired,
                Suspense = recommendations.Suspense,
                SpecialEffects = recommendations.SpecialEffects,
                CharacterCentered = recommendations.CharacterCentered,
                Pace = recommendations.Pace,
                Violence = recommendations.Violence
            };

            await _recommendationsHandler.PostRecommendationsAsync(recommendationRatings);

            var movie = await _moviesHandler.GetMovieByIdAsync(recommendationRatings.MovieId);
            movie.IsAdminRated = true;

            var res = await _moviesHandler.EditMovieStatusAsync(movie);

            return RedirectToAction("AddRatingsPage");

        }
    }
}