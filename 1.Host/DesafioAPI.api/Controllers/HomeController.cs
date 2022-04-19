using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public HomeController(IAccountService accountService)
        {
            _accountService = accountService;
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
                return Ok(account);
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