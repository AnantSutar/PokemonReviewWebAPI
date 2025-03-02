﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReview.DTO;
using PokemonReview.Interfaces;
using PokemonReview.Models;
using PokemonReview.Repository;

namespace PokemonReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController:Controller
    {
        private readonly ICategoryRepository _categoryrepository;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository,IMapper mapper)
        {
            _categoryrepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryrepository.GetCategories());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]

        public IActionResult GetCategory(int categoryId)
        {

            if (!_categoryrepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var category = _mapper.Map<CategoryDto>(_categoryrepository.GetCategory(categoryId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(category);
        }

        [HttpGet("pokemon/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]

        public IActionResult GetPokemonByCategoryId(int categoryId)
        {
            var pokemon = _mapper.Map<List<PokemonDto>>(
                _categoryrepository.GetPokemonByCategory(categoryId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pokemon);


        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if (categoryCreate == null)
                return BadRequest(ModelState);

            var category = _categoryrepository.GetCategories()
                .Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryMap = _mapper.Map<Category>(categoryCreate);
            if (!_categoryrepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int categoryId, [FromBody]CategoryDto updatedCategory)
        {
            if(updatedCategory == null)
            {
                return BadRequest(ModelState);
            }
            if(categoryId!= updatedCategory.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_categoryrepository.CategoryExists(categoryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();


            var categoryMap = _mapper.Map<Category>(updatedCategory);

            if (!_categoryrepository.UpdateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{cateogryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public  IActionResult DeleteCategory(int categoryId,)
        {
            if (!_categoryrepository.CategoryExists(categoryId))
                return NotFound();

            var categoryToDelete = _categoryrepository.GetCategory(categoryId);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_categoryrepository.DeleteCategoroy(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();

        }

    }
}
