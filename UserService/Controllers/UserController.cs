using Microsoft.AspNetCore.Mvc;
using UserService.User;
using UserServiceAPI.Repositories;

namespace UserServiceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
            {
                //404
                return NotFound(new { message = $"User with ID {id} not found." });
            }
            //200
            return Ok(user);
        }
    }
}
