using Core.DTOs;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signUp")]
        public async Task<ActionResult> SignUp(SignUpRequestDTO signUpRequestDTO)
        {
            try
            {
                User user = await _userService.SignUp(signUpRequestDTO);
                return Ok(new { Message = "Usuário registrado com sucesso!", UserId = user.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Erro ao registrar usuário", Details = ex.Message });
            }
        }

    }
}
