using MovieGoersIIBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieGoersII.Handlers.MoviesHandler
{
    public interface IMoviesHandler
    {
        public Task<IEnumerable<Movies>> GetMoviesByStatusAsync(int status);
        public Task<Movies> GetMovieByIdAsync(int movieId);
        public Task<Movies> AddMovieToListAsync(Movies movie);
        public Task<Movies> GetMovieByTmdbIdAsync(int tmdbId);
        public Task<Movies> EditMovieStatusAsync(Movies movie);
    }
}
