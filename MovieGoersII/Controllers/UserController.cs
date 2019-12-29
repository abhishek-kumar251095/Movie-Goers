using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieGoersIIBL;
using Newtonsoft.Json;
using System.Threading;
using System.Text;
using System.IO;
using System.Security;

namespace MovieGoersII.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private IEnumerable<Users> Users {get; set;}   

        public UserController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/userapi");
            var client = _clientFactory.CreateClient("moviegoersWebApi");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();

                Users = JsonConvert.DeserializeObject<IEnumerable<Users>>(responseString);
                
            }

            return View(Users);
            
        }
    }
}