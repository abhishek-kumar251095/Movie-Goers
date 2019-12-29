using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieGoersIIBL;
using MovieGoersIIDAL;
using MovieGoersIIDAL.Services.Repositories;

namespace MovieGoersII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        readonly UserRepository _userRepository;

        public UserAPIController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetAllUsers()
        {
            var res  = await _userRepository.GetAllAsync();
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<Users>> CreateUser(Users user)
        {

            await _userRepository.PostAsync(user);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {

            await _userRepository.DeleteAsync(id);
            return Ok();
        }
    }
}