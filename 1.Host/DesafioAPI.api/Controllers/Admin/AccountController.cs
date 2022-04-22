using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.api.Hateoas.Containers;
using DesafioAPI.api.Helpers;
using DesafioAPI.application.DataTransferObjects;
using DesafioAPI.application.Interfaces;
using DesafioAPI.domain.Entities;
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
        private readonly IUrlHelper _urlHelper;
        private readonly HateoasHelper _hateoasHelper;

        public AccountController(IAccountService accountService, IUrlHelper urlHelper, HateoasHelper hateoasHelper)
        {
            _accountService = accountService;
            _urlHelper = urlHelper;
            _hateoasHelper = hateoasHelper;
        }

        [HttpGet]
        public IActionResult GetAccounts()
        {
            try
            {
                var accounts = _accountService.GetAccounts();
                var accountsWithLinks = new List<AccountContainer>();
                foreach (var account in accounts)
                {
                    accountsWithLinks.Add(_hateoasHelper.AccountGenerateLink(account, GetUrlOfActions(account)));
                }
                
                return Ok(accountsWithLinks);
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
        [Route("{id:int}", Name = nameof(GetByIdAccount))]
        public IActionResult GetByIdAccount([FromRoute] int id)
        {
            try
            {
                var account = _accountService.GetByIdAccount(id);
                var accountWithLinks = _hateoasHelper.AccountGenerateLink(account, GetUrlOfActions(account));
                return Ok(accountWithLinks);
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
        [Route("{id:int}", Name = nameof(PatchByIdAccount))]
        public IActionResult PatchByIdAccount([FromBody] AccountDto accountDto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
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
        [Route("{id:int}", Name = nameof(PutByIdAccount))]
        public IActionResult PutByIdAccount([FromBody] AccountDto accountDto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (string.IsNullOrEmpty(accountDto.Username) || string.IsNullOrEmpty(accountDto.Password) || string.IsNullOrEmpty(accountDto.Email) || accountDto.IsActive is null)
                return BadRequest("Preencha todos os campos");

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
        [Route("{id:int}", Name = nameof(DeleteByIdAccount))]
        public IActionResult DeleteByIdAccount([FromRoute] int id)
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

        //-------------------- Internal Methods --------------------
        private List<string> GetUrlOfActions(Account account)
        {
            try
            {
                var hrefList = new List<string>();
                hrefList.Add(_urlHelper.Link(nameof(GetByIdAccount), new { id = account.Id }));
                hrefList.Add(_urlHelper.Link(nameof(PatchByIdAccount), new { id = account.Id }));
                hrefList.Add(_urlHelper.Link(nameof(PutByIdAccount), new { id = account.Id }));
                hrefList.Add(_urlHelper.Link(nameof(DeleteByIdAccount), new { id = account.Id }));

                return hrefList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
    }
}