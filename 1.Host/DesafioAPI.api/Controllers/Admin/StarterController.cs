using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.api.Hateoas.Containers;
using DesafioAPI.api.Helpers;
using DesafioAPI.application.DataTransferObjects;
using DesafioAPI.application.Interfaces;
using DesafioAPI.domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesafioAPI.api.Controllers.Admin
{
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    [Route("v1/Admin/[controller]")]
    public class StarterController : ControllerBase
    {
        private readonly IStarterService _starterService;
        private readonly IUrlHelper _urlHelper;
        private readonly HateoasHelper _hateoasHelper;
        private readonly FilesHelper _filesHelper;

        public StarterController(IStarterService starterService, IUrlHelper urlHelper, HateoasHelper hateoasHelper, FilesHelper filesHelper)
        {
            _starterService = starterService;
            _urlHelper = urlHelper;
            _hateoasHelper = hateoasHelper;
            _filesHelper = filesHelper;
        }

        [HttpGet]
        public IActionResult GetStarters()
        {
            try
            {
                var starters = _starterService.GetStarters();
                var startersWithLinks = new List<StarterContainer>();
                foreach (var starter in starters)
                {
                    startersWithLinks.Add(_hateoasHelper.StarterGenerateLink(starter, GetUrlOfActions(starter)));
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
        [Route("{id:int}", Name = nameof(GetByIdStarter))]
        public IActionResult GetByIdStarter([FromRoute] int id)
        {
            try
            {
                var starter = _starterService.GetByIdStarter(id);
                var starterWithLinks = _hateoasHelper.StarterGenerateLink(starter, GetUrlOfActions(starter));
                return Ok(starterWithLinks);
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
        [Route("Name/{name}", Name = nameof(GetByNameStarter))]
        public IActionResult GetByNameStarter([FromRoute] string name)
        {
            try
            {
                var starters = _starterService.GetByNameStarters(name);
                var startersWithLinks = new List<StarterContainer>();
                foreach (var starter in starters)
                {
                    startersWithLinks.Add(_hateoasHelper.StarterGenerateLink(starter, GetUrlOfActions(starter)));
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

        [HttpPost]
        [Route("Upload/{id:int}", Name = nameof(UploadByIdPhotoStarter))]
        public IActionResult UploadByIdPhotoStarter([FromForm] FileUpload fileUpload, [FromRoute] int id)
        {
            try
            {
                var starter = _starterService.GetByIdStarter(id);
                byte[] bytes;

                if (fileUpload.photo is null)
                {
                    if (starter.Photo != "Default.jpg")
                        _filesHelper.DeleteImageOnRoot(starter.Photo);

                    _starterService.UploadPhotoByIdStarter(starter, "Default.jpg");
                    bytes = _filesHelper.ShowImageFromRoot(starter);

                    return File(bytes, "image/png");
                }

                if (!starter.Photo.Equals("Default.jpg"))
                    _filesHelper.DeleteImageOnRoot(starter.Photo);

                string photoPath = _filesHelper.SaveImageOnRoot(fileUpload.photo, starter.Abbreviation);
                _starterService.UploadPhotoByIdStarter(starter, photoPath);
                bytes = _filesHelper.ShowImageFromRoot(starter);

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
        [Route("Photo/{id:int}", Name = nameof(PhotoByIdStarter))]
        public IActionResult PhotoByIdStarter([FromRoute] int id)
        {
            try
            {
                var starter = _starterService.GetByIdStarter(id);
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

        [HttpPost]
        public IActionResult PostStarter([FromBody] StarterCreateDto starterDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var starter = _starterService.PostStarter(starterDto);

                return Created($"v1/Admin/Starter/{starter.Id}", starter);
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

        [HttpPatch]
        [Route("{id:int}", Name = nameof(PatchByIdStarter))]
        public IActionResult PatchByIdStarter([FromBody] StarterUpdateDto starterDto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                _starterService.PatchByIdStarter(starterDto, id);
                return Ok();
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

        [HttpPut]
        [Route("{id:int}", Name = nameof(PutByIdStarter))]
        public IActionResult PutByIdStarter([FromBody] StarterUpdateDto starterDto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (string.IsNullOrEmpty(starterDto.Name) || string.IsNullOrEmpty(starterDto.Name) || string.IsNullOrEmpty(starterDto.Name) || string.IsNullOrEmpty(starterDto.Name) || starterDto.CategoryId == 0 || starterDto.IsActive is null)
                return BadRequest("Preencha todos os campos");

            try
            {
                _starterService.PutByIdStarter(starterDto, id);
                return Ok();
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

        [HttpDelete]
        [Route("{id:int}", Name = nameof(DeleteByIdStarter))]
        public IActionResult DeleteByIdStarter([FromRoute] int id)
        {
            try
            {
                var starter = _starterService.GetByIdStarter(id);
                _filesHelper.DeleteImageOnRoot(starter.Photo);
                _starterService.DeleteByIdStarter(id);

                return Ok();
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
                hrefList.Add(_urlHelper.Link(nameof(GetByIdStarter), new { id = starter.Id }));
                hrefList.Add(_urlHelper.Link(nameof(GetByNameStarter), new { name = starter.Name }));
                hrefList.Add(_urlHelper.Link(nameof(UploadByIdPhotoStarter), new { id = starter.Id }));
                hrefList.Add(_urlHelper.Link(nameof(PhotoByIdStarter), new { id = starter.Id }));
                hrefList.Add(_urlHelper.Link(nameof(PatchByIdStarter), new { id = starter.Id }));
                hrefList.Add(_urlHelper.Link(nameof(PutByIdStarter), new { id = starter.Id }));
                hrefList.Add(_urlHelper.Link(nameof(DeleteByIdStarter), new { id = starter.Id }));

                return hrefList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }        
    }
    //-------------------- Temporary solution for Swagger IFormFile/Object bug --------------------
    public class FileUpload
    {
        public IFormFile photo { get; set; }
    }
}