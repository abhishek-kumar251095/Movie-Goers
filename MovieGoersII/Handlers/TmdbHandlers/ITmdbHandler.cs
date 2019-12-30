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
        public Task<Tuple<List<SearchViewModel>, int>> MovieSearchListAsync(string query, string page);
        public Task<Movies> MovieDetailsAsync(int tmdbID);
    }
}
