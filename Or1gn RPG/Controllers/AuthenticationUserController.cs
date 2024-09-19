using Core.Extentions;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static Core.DTO.EntitiesDTO;
using static Core.DTO.RequestEntities.RequestEntities;

namespace Or1gn_RPG.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationUserController : ControllerBase
    {
        public IAuthenticationUserService AuthenticationUserService { get; }
        public AuthenticationUserController(IAuthenticationUserService authenticationUserService) { 
            AuthenticationUserService = authenticationUserService;
        }

        [HttpGet("login")]
        public IActionResult Login([FromQuery] UserLoginAndRegisterRequest userRequest) {
            try {
                var user = AuthenticationUserService.LoginUser(userRequest.name, userRequest.password);

                if (user == null) return Ok("Пользователь не найден!");

                return Ok(user.AsDto());
            }
            catch (Exception ex) {
                return BadRequest(new ErrorDto(ex.Message));
            }
        }

        [HttpPost("register")]
        public IActionResult Register([Required] UserLoginAndRegisterRequest userRequest) {
            try
            {
                var registerUser = AuthenticationUserService.RegisterUser(userRequest.name, userRequest.password);

                if (registerUser == null) return Ok("Пользователь с таким именем уже существует.");

                return Ok(registerUser.AsDto());
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
