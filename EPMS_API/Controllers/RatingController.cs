using EPMS_API.Models;
using EPMS_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EPMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly RatingService _ratingService;

        public RatingController(RatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet]
        public async Task<List<Rating>> Get() => await _ratingService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Rating>> Get(string id)
        {
            var rating = await _ratingService.GetAsync(id);

            if (rating is null)
            {
                return NotFound();
            }

            return rating;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Rating newRating)
        {
            await _ratingService.CreateAsync(newRating);

            return CreatedAtAction(nameof(Get), new { id = newRating.Id }, newRating);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Rating updatedRating)
        {
            var rating = await _ratingService.GetAsync(id);

            if (rating is null)
            {
                return NotFound();
            }

            updatedRating.Id = rating.Id;

            await _ratingService.UpdateAsync(id, updatedRating);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var rating = await _ratingService.GetAsync(id);

            if (rating is null)
            {
                return NotFound();
            }

            await _ratingService.RemoveAsync(id);

            return NoContent();
        }
    }
}