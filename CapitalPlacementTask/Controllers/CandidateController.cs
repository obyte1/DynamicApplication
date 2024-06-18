using CapitalPlacementTask.Business.Services.Interfaces;
using CapitalPlacementTask.Data.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapitalPlacementTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateManager _candidateManager;

        public CandidateController(ICandidateManager candidateManager)
        {
            _candidateManager = candidateManager;
        }

        [HttpPost("save-candidate")]
        public async Task<IActionResult> SaveCandidate([FromBody] CandidateDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var success = await _candidateManager.SaveACandidateAsync(request);
                if (success)
                {
                    return Ok(new { message = "Candidate saved successfully" });
                }
                else
                {
                    return StatusCode(500, new { message = "Failed to save candidate" });
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, new { message = "An error occurred while saving the candidate", error = ex.Message });
            }
        }
    }
}
