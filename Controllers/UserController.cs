
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Contracts.Query;
using MyAPI.Contracts.Requests;
using MyAPI.services.Interfaces;
using MyAPI.Services;

namespace MyAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase

    {
        private readonly ILogger _logger;

        private IUserService _userService;
        //todo lägg till nlogg: fil logging vad som går rätt 

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _userService.Get(id);
            if (user == null)
            {
                _logger.LogInformation("Failed");
                return NotFound("Failed to find user");
            }
            return Ok(user);
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] UserQuery query)
        {
            var user = await _userService.Get(query);
            if (user == null)
            {
                _logger.LogInformation("Failed");
                return NotFound("Failed to find user");
            }
            return Ok(user);

        }

      
        [HttpPost]
       public async Task<IActionResult> Create(UserRequest userRequest)
        {
            var user = await _userService.Create(userRequest);
            if (user == null)
            {
                _logger.LogInformation("Failed");
                return NotFound("Failed to create user");
            }
            return Ok(user);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, UserRequest userRequest)
        {
            var user = await _userService.Update(id, userRequest);
            if (user == null)
            {
                _logger.LogInformation("Failed");
                return NotFound("Failed to update user");
            }
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userService.Delete(id);
            if (user == null)
            {
                _logger.LogInformation("Failed");
                return NotFound("Failed to delete user");
            }
            return Ok(user);
        }


    }
}
