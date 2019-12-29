using MovieGoersIIBL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieGoersII.Handlers.MoviesHandler
{
    public class MoviesHandler : IMoviesHandler
    {
        IHttpClientFactory _client;

        public MoviesHandler(IHttpClientFactory client)
        {
            _client = client;
        }

        public async Task<Movies> GetMovieByIdAsync(int movieId)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "api/moviesapi/" + movieId);
            var client = _client.CreateClient("moviegoersWebApi");
            using (var request = await client.SendAsync(requestMessage))
            {
                if(request.IsSuccessStatusCode)
                {
                    var response = await request.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Movies>(response);
                    return result;
                }
            }

            return null;
        }

        public async Task<Movies> AddMovieToListAsync(Movies movie)
        {
            var uri = new Uri("https://localhost:44357/api/moviesapi/");
            var client = new HttpClient();
            using (var request = await client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(movie), Encoding.UTF8, "application/json"))) 
            {
                if (request.IsSuccessStatusCode)
                {
                    var response = await request.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Movies>(response);
                    return result;
                }
            }

            return null;
        }

        public async Task<Movies> GetMovieByTmdbIdAsync(int tmdbId)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "api/moviesapi/tmdb/" + tmdbId);
            var client = _client.CreateClient("moviegoersWebApi");
            using (var request = await client.SendAsync(requestMessage))
            {
                if (request.IsSuccessStatusCode)
                {
                    var response = await request.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Movies>(response);
                    return result;
                }
            }

            return null;
        }
    }
}
