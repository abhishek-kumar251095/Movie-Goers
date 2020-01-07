using MovieGoersIIBL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieGoersII.Handlers.RecommendationHandler
{
    public class RecommendationsHandler : IRecommendationsHandler
    {
        IHttpClientFactory _client;

        public RecommendationsHandler(IHttpClientFactory client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Recommendations>> GetRecommendationRatingsAsync()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "api/recommendationsapi");
            var client = _client.CreateClient("moviegoersWebApi");
            using (var request = await client.SendAsync(requestMessage))
            {
                if (request.IsSuccessStatusCode)
                {
                    var message = await request.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<IEnumerable<Recommendations>>(message);
                    return result;
                }
            }

            return null;
        }

        public async Task<Recommendations> GetRecommendationRatingsByMovieIdAsync(int movieId)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "api/recommendationsapi/movie/" + movieId);
            var client = _client.CreateClient("moviegoersWebApi");
            using (var request = await client.SendAsync(requestMessage))
            {
                if(request.IsSuccessStatusCode)
                {
                    var message = await request.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Recommendations>(message);
                    return result;
                }
            }

            return null;
        }

        public async Task<Recommendations> PostRecommendationsAsync(Recommendations recommendations)
        {
            var uri = new Uri("https://localhost:44357/api/recommendationsapi/");
            var client = new HttpClient();
            using (var request = await client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(recommendations), Encoding.UTF8, "application/json")))
            {
                if (request.IsSuccessStatusCode)
                {
                    var response = await request.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Recommendations>(response);
                    return result;
                }
            }

            return null;
        }
    }
}
