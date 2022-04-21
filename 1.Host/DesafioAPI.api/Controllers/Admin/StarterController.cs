using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly FilesHelper _filesHelper;

        public StarterController(IStarterService starterService, FilesHelper filesHelper)
        {
            _starterService = starterService;
            _filesHelper = filesHelper;
        }

        [HttpGet]
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

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetByIdStarter([FromRoute] int id)
        {
            try
            {
                var starter = _starterService.GetByIdStarter(id);
                return Ok(starter);
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
        [Route("Name/{name}")]
        public IActionResult GetByNameStarter([FromRoute] string name)
        {
            try
            {
                var starters = _starterService.GetByNameStarters(name);
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

        [HttpPost]
        [Route("Upload/{id:int}")]
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
        [Route("Photo/{id:int}")]
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
        [Route("{id:int}")]
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
        [Route("{id:int}")]
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
        [Route("{id:int}")]
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
    }

    //-------------------- Swagger object bug --------------------
    public class FileUpload
    {
        public IFormFile photo { get; set; }
    }
}