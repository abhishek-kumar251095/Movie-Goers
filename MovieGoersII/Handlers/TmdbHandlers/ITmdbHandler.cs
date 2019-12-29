using MovieGoersII.ViewModels;
using MovieGoersIIBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieGoersII.Handlers
{
    public interface ITmdbHandler
    {
        public Task<IEnumerable<SearchViewModel>> MovieSearchList(string query, string page);
        public Task<Movies> MovieDetails(int tmdbID);
    }
}
