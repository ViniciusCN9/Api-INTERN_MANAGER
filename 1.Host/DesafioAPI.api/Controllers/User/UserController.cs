using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesafioAPI.api.Controllers.User
{
    [ApiController]
    [Authorize(Roles = "USER")]
    [Route("v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IStarterService _starterService;

        public UserController(IStarterService starterService)
        {
            _starterService = starterService;
        }

        [HttpGet]
        [Route("Starter")]
        public IActionResult GetStarters()
        {
            try
            {
                var starters = _starterService.GetStarters();
                
                return Ok(starters);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}