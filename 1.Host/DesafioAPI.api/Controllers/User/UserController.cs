using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.api.Hateoas.Containers;
using DesafioAPI.api.Helpers;
using DesafioAPI.application.Interfaces;
using DesafioAPI.domain.Entities;
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
        private readonly IUrlHelper _urlHelper;
        private readonly HateoasHelper _hateoasHelper;
        private readonly FilesHelper _filesHelper;

        public UserController(IUserService userService, IStarterService starterService, IUrlHelper urlHelper, HateoasHelper hateoasHelper, FilesHelper filesHelper)
        {
            _userService = userService;
            _starterService = starterService;
            _urlHelper = urlHelper;
            _hateoasHelper = hateoasHelper;
            _filesHelper = filesHelper;
        }

        [HttpGet]
        [Route("Starter")]
        public IActionResult GetStarters()
        {
            try
            {
                var starters = _starterService.GetStarters();
                var startersActive = _userService.VerifyStartersIsActive(starters);

                var startersWithLinks = new List<object>();
                foreach (var starter in startersActive)
                {
                    var starterContainer = _hateoasHelper.UserGenerateLink(starter, GetUrlOfActions(starter));
                    var starterHiddenInfo = _userService.HideStarterInformations(starterContainer.Starter);
                    startersWithLinks.Add(new { starter = starterHiddenInfo, links = starterContainer.Links});
                }

                return Ok(startersWithLinks);
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
        [Route("Starter/{name}", Name = nameof(GetByNameStarters))]
        public IActionResult GetByNameStarters([FromRoute] string name)
        {
            try
            {
                var starters = _starterService.GetByNameStarters(name);
                var startersActive = _userService.VerifyStartersIsActive(starters);
                var startersWithLinks = new List<object>();
                foreach (var starter in startersActive)
                {
                    var starterContainer = _hateoasHelper.UserGenerateLink(starter, GetUrlOfActions(starter));
                    var starterHiddenInfo = _userService.HideStarterInformations(starterContainer.Starter);
                    startersWithLinks.Add(new { starter = starterHiddenInfo, links = starterContainer.Links});
                }

                return Ok(startersWithLinks);
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
        [Route("Starter/Photo/{name}", Name = nameof(PhotoByNameStarter))]
        public IActionResult PhotoByNameStarter([FromRoute] string name)
        {
            try
            {
                var starter = _starterService.GetByNameStarters(name).First(e => e.Name == name);
                var bytes = _filesHelper.ShowImageFromRoot(starter);

                    return File(bytes, "image/png");
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

                var startersWithLinks = new List<object>();
                foreach (var starter in startersActive)
                {
                    var starterContainer = _hateoasHelper.UserGenerateLink(starter, GetUrlOfActions(starter));
                    var starterHiddenInfo = _userService.HideStarterInformations(starterContainer.Starter);
                    startersWithLinks.Add(new { starter = starterHiddenInfo, links = starterContainer.Links});
                }

                return Ok(startersWithLinks);
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

                var startersWithLinks = new List<object>();
                foreach (var starter in startersActive)
                {
                    var starterContainer = _hateoasHelper.UserGenerateLink(starter, GetUrlOfActions(starter));
                    var starterHiddenInfo = _userService.HideStarterInformations(starterContainer.Starter);
                    startersWithLinks.Add(new { starter = starterHiddenInfo, links = starterContainer.Links});
                }

                return Ok(startersWithLinks);
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

        //-------------------- Internal Methods --------------------
        private List<string> GetUrlOfActions(Starter starter)
        {
            try
            {
                var hrefList = new List<string>();
                hrefList.Add(_urlHelper.Link(nameof(GetByNameStarters), new { name = starter.Name }));
                hrefList.Add(_urlHelper.Link(nameof(PhotoByNameStarter), new { name = starter.Name }));

                return hrefList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}