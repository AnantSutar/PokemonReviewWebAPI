using AutoMapper;
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
    }
}
