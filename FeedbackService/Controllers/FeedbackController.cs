using FeedbackService.Model;
using FeedbackService.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {

        private readonly FeedbackServiceImpl _feedbackService;

        public FeedbackController(FeedbackServiceImpl feedbackService) =>
            _feedbackService = feedbackService;

        [HttpGet]
        public async Task<List<Feedback>> Get() =>
            await _feedbackService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Feedback>> Get(string id)
        {
            var feedback = await _feedbackService.GetAsync(id);

            if (feedback is null)
            {
                return NotFound();
            }

            return feedback;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Feedback newFeedback)
        {
            await _feedbackService.CreateAsync(newFeedback);

            return CreatedAtAction(nameof(Get), new { id = newFeedback.Id }, newFeedback);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Feedback updatedFeedback)
        {
            var feedback = await _feedbackService.GetAsync(id);

            if (feedback is null)
            {
                return NotFound();
            }

            updatedFeedback.Id = feedback.Id;

            await _feedbackService.UpdateAsync(id, updatedFeedback);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var feedback = await _feedbackService.GetAsync(id);

            if (feedback is null)
            {
                return NotFound();
            }

            await _feedbackService.RemoveAsync(id);

            return NoContent();
        }
    }
}
