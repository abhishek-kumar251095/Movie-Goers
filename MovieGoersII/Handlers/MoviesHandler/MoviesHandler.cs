using MovieGoersIIBL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    }
}
