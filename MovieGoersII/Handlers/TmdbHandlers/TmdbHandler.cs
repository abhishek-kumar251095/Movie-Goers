using MovieGoersII.ViewModels;
using MovieGoersIIBL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieGoersII.Handlers
{
    public class TmdbHandler : ITmdbHandler
    {
        public IHttpClientFactory _tmdbClient;

        public TmdbHandler(IHttpClientFactory tmdbClient)
        {
            DotNetEnv.Env.Load();
            _tmdbClient = tmdbClient;
        }
        public async Task<Movies> MovieDetails(int tmdbID)
        {
            var apiKey = DotNetEnv.Env.GetString("TMDB_Key");
            var request = new HttpRequestMessage(HttpMethod.Get, "movie/15400?api_key=" + apiKey + "&language=en-US");
            var client = _tmdbClient.CreateClient("tmdbApi");
            using (var response = await client.SendAsync(request))
            {
                if(response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    dynamic resp = JToken.Parse(responseString);
                    Movies movie = new Movies
                    {
                        Title = resp.title,
                        Image = resp.backdrop_path,
                        Genre = "default",
                        TMDBId = resp.id,
                        IMDBId = resp.imdb_id,
                        Overview = resp.overview,
                        Language = resp.original_language,
                        ReleaseDate = resp.release_date,
                        Status = resp.status,
                        Runtime = resp.runtime,
                        IsAdminRated = false
                    };

                    return movie;
                }
                else
                {
                    return null;
                }
            }
            
        }

        public async Task<IEnumerable<SearchViewModel>> MovieSearchList(string query, string page)
        {
            var apiKey = DotNetEnv.Env.GetString("TMDB_Key");
            var request = new HttpRequestMessage(HttpMethod.Get,
                "search/movie?api_key="+apiKey+"&language=en-US&query=" + query + "&page=" + Convert.ToInt32(page) + "&include_adult=false");

            var client = _tmdbClient.CreateClient("tmdbApi");
            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    dynamic resp = JToken.Parse(responseString);

                    List<SearchViewModel> searchResults = new List<SearchViewModel>();

                    foreach (var result in resp.results)
                    {
                        SearchViewModel searchResult = new SearchViewModel
                        {
                            MovieId = result.id,
                            MovieName = result.title
                        };
                        searchResults.Add(searchResult);
                    }

                    return searchResults;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
