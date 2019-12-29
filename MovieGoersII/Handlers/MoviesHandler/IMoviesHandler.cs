using MovieGoersIIBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieGoersII.Handlers.MoviesHandler
{
    public interface IMoviesHandler
    {
        public Task<Movies> GetMovieByIdAsync(int movieId);
    }
}
