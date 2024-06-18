using CapitalPlacementTask.Data.DTOs;
using CapitalPlacementTask.Data.Enums;
using CapitalPlacementTask.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalPlacementTask.Business.Services.Interfaces
{
    public interface ICandidateQuestionManager
    {
        Task<bool> CreateQuestionAsync(ApplicationDto createApplicationDTO);
        Task<bool> DeleteQuestionAsync(int questionId);
        Task<Question> GetQuestionAsync(QuestionTypes questionType);
        Task<bool> UpdateQuestionAsync(QuestionDto createQuestionDTO);
    }
}
