using Microsoft.EntityFrameworkCore;
using MovieGoersIIBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieGoersIIDAL.Services.Repositories
{
    public class RecommendationsRepository: Repository<Recommendations, ApplicationDBContext>
    {
        public RecommendationsRepository(ApplicationDBContext context):base(context)
        {
        }

        public async Task<Recommendations> GetRecommendationsByMovieId(int movieId)
        {
            var result = await _context.Recommendations.Where(o => o.MovieId == movieId).FirstOrDefaultAsync();

            return result;
        }
    }
}
