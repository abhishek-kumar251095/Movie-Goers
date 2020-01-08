using MovieGoersIIBL;
using MovieGoersIIDAL.Services.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieGoersII.Handlers.UserCollectionHandler
{
    public class UserCollectionHandler: IUserCollectionHandler
    {
        IHttpClientFactory _client;
        UserCollectionRepository _collectionRepository;

        public UserCollectionHandler(IHttpClientFactory client, UserCollectionRepository collectionRepository)
        {
            _client = client;
            _collectionRepository = collectionRepository;
        }

        public async Task<IEnumerable<UserCollection>> GetCollectionByUserIdAsync(int userId)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "api/UserCollectionAPI/User/" + userId);
            var client = _client.CreateClient("moviegoersWebApi");
            using (var request = await client.SendAsync(requestMessage))
            {
                if (request.IsSuccessStatusCode)
                {
                    var response = await request.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<IEnumerable<UserCollection>>(response);
                    return result;
                }
            }

            return null;
        }

        public async Task<UserCollection> AddCollectionAsync(UserCollection userCollection)
        {
            var uri = new Uri("https://localhost:44357/api/usercollectionapi/");
            var client = new HttpClient();
            using (var request = await client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(userCollection), Encoding.UTF8, "application/json")))
            {
                if (request.IsSuccessStatusCode)
                {
                    var response = await request.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<UserCollection>(response);
                    return result;
                }
            }

            return null;
        }

        public async Task<UserCollection> RemoveCollectionAsync(int tmdbId, int userId)
        {
            var collection = await _collectionRepository.GetCollectionFromTmdbIdAAsync(tmdbId, userId);
            var uri = new Uri("https://localhost:44357/api/usercollectionapi/");
            var client = new HttpClient();
            using (var request = await client.PutAsync(uri, new StringContent(JsonConvert.SerializeObject(collection), Encoding.UTF8, "application/json")))
            {
                if (request.IsSuccessStatusCode)
                {
                    var response = await request.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<UserCollection>(response);
                    return result;
                }
            }

            return null;

        }

        public async Task<bool> CheckCollectionForMovieAsync(int tmdbId, int userId)
        {
            var res = await _collectionRepository.CheckMovieInCollectionAsync(tmdbId, userId);
            return res;
        }

        public async IAsyncEnumerable<UserCollection> GetMovieReviewsFromId(int movieId)
        {
            var res = await _collectionRepository.GetReviewsFromMovieId(movieId);
            foreach(var item in res)
            {
                yield return item;
            }

        }
    }
}
