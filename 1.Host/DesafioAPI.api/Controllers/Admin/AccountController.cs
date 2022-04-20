using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.application.DataTransferObjects;
using DesafioAPI.application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesafioAPI.api.Controllers.Admin
{
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    [Route("v1/Admin/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult GetAccounts()
        {
            try
            {
                var accounts = _accountService.GetAccounts();
                return Ok(accounts);
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
        
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetByIdAccount([FromRoute] int id)
        {
            try
            {
                var account = _accountService.GetByIdAccount(id);
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

        [HttpPatch]
        [Route("{id:int}")]
        public IActionResult PatchByIdAccount([FromBody] AccountDto accountDto, [FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            try
            {
                _accountService.PatchByIdAccount(accountDto, id);
                return Ok();
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

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult PutByIdAccount([FromBody] AccountDto accountDto, [FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            try
            {
                _accountService.PutByIdAccount(accountDto, id);
                return Ok();
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

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteById([FromRoute] int id)
        {
            try
            {
                _accountService.DeleteByIdAccount(id);
                return Ok();
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
    }
}