using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        public static IWebHostEnvironment _environment;

        public StarterController(IStarterService starterService, IWebHostEnvironment environment)
        {
            _starterService = starterService;
            _environment = environment;
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
                var starter = _starterService.GetByNameStarter(name);
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

        [HttpPost]
        [Route("Upload/{id:int}")]
        public IActionResult UploadPhotoByIdStarter([FromForm] FileUpload fileUpload, [FromRoute] int id)
        {
            try
            {
                var starter = _starterService.GetByIdStarter(id);
                if (!starter.Photo.Equals("Default.jpg"))
                    DeleteImageOnRoot(starter.Photo);

                string photoPath = SaveImageOnRoot(fileUpload.photo, starter.Abbreviation);
                
                _starterService.UploadPhotoByIdStarter(starter, photoPath);

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

        [HttpPost]
        public IActionResult PostStarter([FromBody] StarterCreateDto starterDto)
        {
            if(!ModelState.IsValid)
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
            if(!ModelState.IsValid)
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
            if(!ModelState.IsValid)
                return BadRequest();

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

        //-------------------- Internal methods --------------------

        private string SaveImageOnRoot(IFormFile photo, string abbreviation)
        {
            try
            {
                string directoryPath = Path.Combine(_environment.WebRootPath, "Assets/Images");
                string photoUniqueName;
                if (photo is null)
                    throw new ArgumentException("Insira uma foto");

                string photoExtension = photo.FileName.Split('.').Last();

                photoUniqueName = Guid.NewGuid().ToString().Substring(0,9) + abbreviation + "." + photoExtension;
                string photoPath = Path.Combine(directoryPath, photoUniqueName);

                using (var fileStream = new FileStream(photoPath, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }

                return photoUniqueName;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void DeleteImageOnRoot(string photo)
        {
            try
            {
                string directoryPath = Path.Combine(_environment.WebRootPath, "Assets/Images");
                string photoPath = Path.Combine(directoryPath, photo);
                if (System.IO.File.Exists(photoPath))
                    System.IO.File.Delete(photoPath);
            }
            catch
            {
                throw new Exception("Falha ao deletar foto antiga");
            }
        }
    }

    //-------------------- Swagger object bug --------------------
    public class FileUpload
    {
        public IFormFile photo { get; set; }
    }
}