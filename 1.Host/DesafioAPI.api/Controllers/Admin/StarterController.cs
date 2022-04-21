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
                        DeleteImageOnRoot(starter.Photo);

                    _starterService.UploadPhotoByIdStarter(starter, "Default.jpg");
                    bytes = ShowImageFromRoot(starter);

                    return File(bytes, "image/png");
                }

                if (!starter.Photo.Equals("Default.jpg"))
                    DeleteImageOnRoot(starter.Photo);

                string photoPath = SaveImageOnRoot(fileUpload.photo, starter.Abbreviation);
                _starterService.UploadPhotoByIdStarter(starter, photoPath);
                bytes = ShowImageFromRoot(starter);

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
                var bytes = ShowImageFromRoot(starter);

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
                DeleteImageOnRoot(starter.Photo);
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

        private byte[] ShowImageFromRoot(Starter starter)
        {
            string directoryPath = Path.Combine(_environment.WebRootPath, "Assets/Images");
            string photoPath = Path.Combine(directoryPath, starter.Photo);
            if (!System.IO.File.Exists(photoPath))
                throw new ArgumentException("Foto não encontrada");

            return System.IO.File.ReadAllBytes(photoPath);
        }

        private void DeleteImageOnRoot(string photo)
        {
            if (photo == "Default.jpg")
                return;

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