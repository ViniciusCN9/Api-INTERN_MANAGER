using System;
using DesafioAPI.application.DataTransferObjects;
using DesafioAPI.application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesafioAPI.api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public HomeController(IAccountService accountService, ITokenService tokenService, IEmailService emailService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        /// <summary>
        /// Realiza login.
        /// </summary>
        /// <remarks>
        /// Example:
        ///
        ///     Admin
        ///     {
        ///        "username": "Admin",
        ///        "password": "Gft@1234"
        ///     }
        ///     
        ///     User
        ///     {
        ///        "username": "User",
        ///        "password": "Gft@1234"
        ///     }
        ///
        /// </remarks>
        /// <returns>Token para acesso correspondente a função da conta</returns>
        /// <response code="200"> Retorna Token para acesso correspondente a função da conta</response>
        [HttpPost]
        [Route("Login")]
        public IActionResult PostLogin([FromBody] AccountLoginDto login)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var account = _accountService.PostLogin(login.Username, login.Password);
                if (!account.IsActive)
                    return Unauthorized("A conta está desativada");

                var token = _tokenService.GenerateToken(account);
                _emailService.SendEmail(account.Email, "Login realizado");

                return Ok(token);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Cria uma conta.
        /// </summary>
        /// <returns>Usuário e senha para login</returns>
        /// <response code="200"> Retorna usuário e senha para login</response>
        [HttpPost]
        [Route("Register")]
        public IActionResult PostRegister([FromBody] AccountRegisterDto register)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                _accountService.PostRegister(register);
                return CreatedAtAction(nameof(PostLogin), new { username = register.Username, password = register.Password });
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}