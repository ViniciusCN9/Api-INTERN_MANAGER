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