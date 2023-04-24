using dotnet_api_first.Data;
using dotnet_api_first.DTOs.user;
using dotnet_api_first.models;
using Microsoft.AspNetCore.Mvc;


namespace dotnet_api_first.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Auth : ControllerBase
    {
        private readonly IAuthRepo _authRepo;

        public Auth(IAuthRepo authRepo)
        {
            _authRepo = authRepo;

        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceRespinse<int>>> Register(UserDTO user)
        {
            var response = await _authRepo.Register(
                new User { userName = user.userName }, user.password
            );
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceRespinse<int>>> Login(UserLoginDTO user)
        {
            var response = await _authRepo.Login(user.userName, user.password);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }
    }

}