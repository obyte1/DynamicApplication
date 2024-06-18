using CapitalPlacementTask.Business.Services.Interfaces;
using CapitalPlacementTask.Data.DTOs;
using CapitalPlacementTask.Data.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapitalPlacementTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateQuestionController : ControllerBase
    {
        private readonly ICandidateQuestionManager _questionManager;

        public CandidateQuestionController(ICandidateQuestionManager questionManager)
        {
            _questionManager = questionManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateQuestion([FromBody] ApplicationDto createApplicationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var success = await _questionManager.CreateQuestionAsync(createApplicationDTO);
                if (success)
                {
                    return Ok(new { message = "Questions created successfully" });
                }
                else
                {
                    return StatusCode(500, new { message = "Failed to create questions" });
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, new { message = "An error occurred while creating questions", error = ex.Message });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateQuestion([FromBody] QuestionDto createQuestionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var success = await _questionManager.UpdateQuestionAsync(createQuestionDTO);
                if (success)
                {
                    return Ok(new { message = "Question updated successfully" });
                }
                else
                {
                    return StatusCode(500, new { message = "Failed to update question" });
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, new { message = "An error occurred while updating question", error = ex.Message });
            }
        }

        [HttpGet("get/{questionType}")]
        public async Task<IActionResult> GetQuestion(QuestionTypes questionType)
        {
            try
            {
                var question = await _questionManager.GetQuestionAsync(questionType);
                if (question != null)
                {
                    return Ok(question);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, new { message = "An error occurred while retrieving question", error = ex.Message });
            }
        }

        [HttpDelete("soft-delete/{questionId}")]
        public async Task<IActionResult> DeleteQuestion(int questionId)
        {
            try
            {
                var success = await _questionManager.DeleteQuestionAsync(questionId);
                if (success)
                {
                    return Ok(new { message = "Question deleted successfully" });
                }
                else
                {
                    return NotFound(new { message = "Question not found" });
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, new { message = "An error occurred while deleting question", error = ex.Message });
            }
        }
    }
}

