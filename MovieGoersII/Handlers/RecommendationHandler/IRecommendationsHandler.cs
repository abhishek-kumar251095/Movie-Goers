using MovieGoersIIBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieGoersII.Handlers.RecommendationHandler
{
    public interface IRecommendationsHandler
    {
        public Task<IEnumerable<Recommendations>> GetRecommendationRatingsAsync();
        public Task<Recommendations> GetRecommendationRatingsByMovieIdAsync(int movieId);
        public Task<Recommendations> PostRecommendationsAsync(Recommendations recommendations);
    }
}
