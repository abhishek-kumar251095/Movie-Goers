using MovieGoersIIBL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieGoersII.Handlers.UserCollectionHandler
{
    public class UserCollectionHandler: IUserCollectionHandler
    {
        IHttpClientFactory _client;

        public UserCollectionHandler(IHttpClientFactory client)
        {
            _client = client;
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
    }
}
