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
    public class OwnerController:Controller
    {
        private readonly IOwnerRepository _ownerrepository;
        private readonly ICountryRepository _countryrepository;
        private readonly IMapper _mapper;
        public OwnerController(IOwnerRepository ownerrepository, ICountryRepository countryrepository, IMapper mapper)
        {
            _ownerrepository = ownerrepository;
            _countryrepository = countryrepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        public IActionResult GetPokemons()
        {
            var owners = _mapper.Map<List<OwnerDto>>(_ownerrepository.GetOwners());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owners);
        }


        [HttpGet("{ownerId}")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]

        public IActionResult GetPokemon(int ownerId)
        {

            if (!_ownerrepository.OwnerExist(ownerId))
            {
                return NotFound();
            }

            var owner = _mapper.Map<OwnerDto>(_ownerrepository.GetOwner(ownerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owner);
        }

        [HttpGet("{ownerId}/pokemon")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]

        public IActionResult GetPokemonByOwner(int ownerId) 
        {
            if (!_ownerrepository.OwnerExist(ownerId))
            {
                return NotFound();
            }

            var owner = _mapper.Map<List<PokemonDto>>(_ownerrepository.GetPokemonByOwner(ownerId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owner);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult CreateOwner([FromQuery] int countryId ,[FromBody] OwnerDto ownerCreate)
        {
            if (ownerCreate == null)
                return BadRequest(ModelState);

            var owner = _ownerrepository.GetOwners()
                .Where(c => c.LastName.Trim().ToUpper() == ownerCreate.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (owner != null)
            {
                ModelState.AddModelError("", "Owner already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ownerMap = _mapper.Map<Owner>(ownerCreate);

            ownerMap.Country = _countryrepository.GetCountry(countryId);

            if (!_ownerrepository.CreateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }
    }
}
