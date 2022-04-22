using System;
using System.Collections.Generic;
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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IUrlHelper _urlHelper;
        private readonly HateoasHelper _hateoasHelper;

        public CategoryController(ICategoryService categoryService, IUrlHelper urlHelper, HateoasHelper hateoasHelper)
        {
            _categoryService = categoryService;
            _urlHelper = urlHelper;
            _hateoasHelper = hateoasHelper;
        }

        /// <summary>
        /// Lista categorias.
        /// </summary>
        /// <returns>Categorias cadastrados</returns>
        /// <response code="200"> Retorna categorias cadastradas</response>
        [HttpGet]
        public IActionResult GetCategories()
        {
            try
            {
                var categories = _categoryService.GetCategories();
                var categoriesWithLinks = new List<CategoryContainer>();
                foreach (var category in categories)
                {
                    categoriesWithLinks.Add(_hateoasHelper.CategoryGenerateLink(category, GetUrlOfActions(category)));
                }
                
                return Ok(categoriesWithLinks);
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

        /// <summary>
        /// Categoria com o id informado.
        /// </summary>
        /// <returns>Categoria com o id informado</returns>
        /// <response code="200"> Retorna categoria com o id informado</response>
        [HttpGet]
        [Route("{id:int}", Name = nameof(GetByIdCategory))]
        public IActionResult GetByIdCategory([FromRoute] int id)
        {
            try
            {
                var category = _categoryService.GetByIdCategory(id);
                var categoryWithLinks = _hateoasHelper.CategoryGenerateLink(category, GetUrlOfActions(category));
                return Ok(categoryWithLinks);
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

        /// <summary>
        /// Cadastra uma categoria.
        /// </summary>
        /// <response code="200"> Categoria cadastrada com sucesso</response>
        [HttpPost]
        public IActionResult PostCategory([FromBody] CategoryCreateDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var category = _categoryService.PostCategory(categoryDto);
                var categoryWithLinks = _hateoasHelper.CategoryGenerateLink(category, GetUrlOfActions(category));
                return Created($"v1/Admin/Category/{category.Id}", categoryWithLinks);
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

        /// <summary>
        /// Atualiza dados parciais da categoria com o id informado.
        /// </summary>
        /// <response code="200"> Categoria atualizada com sucesso</response>
        [HttpPatch]
        [Route("{id:int}", Name = nameof(PatchByIdCategory))]
        public IActionResult PatchByIdCategory([FromBody] CategoryUpdateDto categoryDto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                _categoryService.PatchByIdCategory(categoryDto, id);
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

        /// <summary>
        /// Atualiza todos os dados da categoria com o id informado.
        /// </summary>
        /// <response code="200"> Categoria atualizada com sucesso</response>
        [HttpPut]
        [Route("{id:int}", Name = nameof(PutByIdCategory))]
        public IActionResult PutByIdCategory([FromBody] CategoryUpdateDto categoryDto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (string.IsNullOrEmpty(categoryDto.Name) || string.IsNullOrEmpty(categoryDto.Technology) || categoryDto.IsActive is null)
                return BadRequest("Preencha todos os campos");

            try
            {
                _categoryService.PutByIdCategory(categoryDto, id);
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

        /// <summary>
        /// Deleta a categoria com o id informado.
        /// </summary>
        /// <response code="200"> Categoria deletada com sucesso</response>
        [HttpDelete]
        [Route("{id:int}",  Name = nameof(DeleteByIdCategory))]
        public IActionResult DeleteByIdCategory([FromRoute] int id)
        {
            try
            {
                _categoryService.DeleteByIdCategory(id);
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
        private List<string> GetUrlOfActions(Category category)
        {
            try
            {
                var hrefList = new List<string>();
                hrefList.Add(_urlHelper.Link(nameof(GetByIdCategory), new { id = category.Id }));
                hrefList.Add(_urlHelper.Link(nameof(PatchByIdCategory), new { id = category.Id }));
                hrefList.Add(_urlHelper.Link(nameof(PutByIdCategory), new { id = category.Id }));
                hrefList.Add(_urlHelper.Link(nameof(DeleteByIdCategory), new { id = category.Id }));

                return hrefList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
    }
}