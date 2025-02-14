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
    public class ReviewerController:Controller
    {
        private readonly IReviewerRepository _reviwerRepository;
        private readonly IMapper _mapper;
        public ReviewerController(IReviewerRepository reviwerRepository, IMapper mapper)
        {
            _reviwerRepository = reviwerRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
        public IActionResult GetReviewers()
        {
            var reviewers = _mapper.Map<List<ReviewerDto>>(_reviwerRepository.GetReviewers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviewers);
        }


        [HttpGet("{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]

        public IActionResult GetReview(int reviewerId)
        {

            if (!_reviwerRepository.ReviewerExists(reviewerId))
            {
                return NotFound();
            }

            var reviewer = _mapper.Map<ReviewerDto>(_reviwerRepository.GetReviewer(reviewerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviewer);
        }

        [HttpGet("{reviewerId}/reviews")]
        public IActionResult GetReviewsByAReviewer(int reviewerId)
        {
            if (!_reviwerRepository.ReviewerExists(reviewerId))
            {
                return NotFound();
            }

            var reviews = _mapper.Map<List<ReviewDto>>(_reviwerRepository.GetReviewsByReviewer(reviewerId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }
    }


}
