using Microsoft.EntityFrameworkCore;
using MovieGoersIIBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieGoersIIDAL.Services.Repositories
{
    public class MoviesRepository: Repository<Movies, ApplicationDBContext>
    {
        public MoviesRepository(ApplicationDBContext context):base(context)
        {
        }

        public async Task<IEnumerable<Movies>> GetMovieByStatusAsync(bool isAdminRated)
        {
            var movies = await _context.Movies.Where(o => o.IsAdminRated == isAdminRated).ToListAsync();
            if(!movies.Any())
            {
                return null;
            }

            return movies;

        }
    }
}
