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
    [Authorize(Roles = "ADMIN, USER")]
    [Route("v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IStarterService _starterService;

        public UserController(IUserService userService, IStarterService starterService)
        {
            _userService = userService;
            _starterService = starterService;
        }

        [HttpGet]
        [Route("Starter")]
        public IActionResult GetStarters()
        {
            try
            {
                var starters = _starterService.GetStarters();
                var startersActive = _userService.VerifyStartersIsActive(starters);
                var startersHiddenInfo = new List<object>();
                foreach (var starter in startersActive)
                {
                    startersHiddenInfo.Add(_userService.HideStarterInformations(starter));  
                }

                return Ok(startersHiddenInfo);
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

        [HttpGet]
        [Route("Starter/{name}")]
        public IActionResult GetByNameStarters([FromRoute] string name)
        {
            try
            {
                var starters = _starterService.GetByNameStarters(name);
                var startersActive = _userService.VerifyStartersIsActive(starters);
                var startersHiddenInfo = new List<object>();
                foreach (var starter in startersActive)
                {
                    startersHiddenInfo.Add(_userService.HideStarterInformations(starter));  
                }

                return Ok(startersHiddenInfo);
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

        [HttpGet]
        [Route("Starter/Ascending")]
        public IActionResult GetAscendingStarters()
        {
            try
            {
                var starters = _starterService.GetStarters();
                var startersActive = _userService.VerifyStartersIsActive(starters).OrderBy(e => e.Name);
                var startersHiddenInfo = new List<object>();
                foreach (var starter in startersActive)
                {
                    startersHiddenInfo.Add(_userService.HideStarterInformations(starter));  
                }

                return Ok(startersHiddenInfo);
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

        [HttpGet]
        [Route("Starter/Descending")]
        public IActionResult GetDescendingStarters()
        {
            try
            {
                var starters = _starterService.GetStarters();
                var startersActive = _userService.VerifyStartersIsActive(starters).OrderByDescending(e => e.Name);
                var startersHiddenInfo = new List<object>();
                foreach (var starter in startersActive)
                {
                    startersHiddenInfo.Add(_userService.HideStarterInformations(starter));  
                }

                return Ok(startersHiddenInfo);
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