using Microsoft.AspNetCore.Mvc;
using MyAPI.Contracts.Query;
using MyAPI.Contracts.Requests;
using MyAPI.Services.Interfaces;

namespace MyAPI.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ILogger _logger;
        private IRoleService _roleService;

        public RoleController(IRoleService roleService, ILogger<RoleController> logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var role = await _roleService.Get(id);
            if (role == null)
            {
                _logger.LogInformation("Failed");
                return NotFound("Failed to find user");
            }
            return Ok(role);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] RoleQuery query)
        {
            var role = await _roleService.Get(query);
            if (role == null)
            {
                _logger.LogInformation("Failed");
                return NotFound("Failed to find user");
            }
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleRequest roleRequest)
        {
            var role = await _roleService.Create(roleRequest);
            if (role == null)
            {
                _logger.LogInformation("Failed");
                return NotFound("Failed to create user");
            }
            return Ok(role);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] Guid id, RoleRequest roleRequest)
        {
            var role = await _roleService.Update(id, roleRequest);
            if (role == null)
            {
                _logger.LogInformation("Failed");
                return NotFound("Failed to update user");
            }
            return Ok(role);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var role = await _roleService.Delete(id);
            if (role == null)
            {
                _logger.LogInformation("Failed");
                return NotFound("Failed to delete user");
            }
            return Ok(role);
        }

    }
}
