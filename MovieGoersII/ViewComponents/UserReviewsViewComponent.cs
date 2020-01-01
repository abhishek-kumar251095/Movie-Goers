using Microsoft.AspNetCore.Mvc;
using MovieGoersII.Handlers.UserCollectionHandler;
using MovieGoersII.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieGoersII.ViewComponents
{
    public class UserReviewsViewComponent: ViewComponent
    {
        IUserCollectionHandler _collectionhandler;

        public UserReviewsViewComponent(IUserCollectionHandler collectionhandler)
        {
            _collectionhandler = collectionhandler;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var reviews = await GetReviewsAsync(id);
            return View(reviews);
        }

        public async Task<IEnumerable<ReviewsViewModel>> GetReviewsAsync(int movieId)
        {
            var res = await _collectionhandler.GetMovieReviewsFromId(movieId);
            List<ReviewsViewModel> reviewsModels = new List<ReviewsViewModel>();
            ReviewsViewModel review;


            foreach (var item in res)
            {
                review = new ReviewsViewModel
                {
                    UserName = item.User.UserName,
                    MovieId = item.MovieId,
                    Rating = item.Rating,
                    Review = item.Review
                };

                reviewsModels.Add(review);
            }

            return reviewsModels;
        }

    }
}
