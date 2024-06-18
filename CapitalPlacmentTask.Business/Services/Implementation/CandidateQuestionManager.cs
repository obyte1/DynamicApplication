using CapitalPlacementTask.Business.Services.Interfaces;
using CapitalPlacementTask.Data.DTOs;
using CapitalPlacementTask.Data.Enums;
using CapitalPlacementTask.Data.Models;
using CapitalPlacementTask.Data.Repository.Implementation;
using CapitalPlacementTask.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CapitalPlacementTask.Business.Services.Implementation
{
    public class CandidateQuestionManager : ICandidateQuestionManager
    {
        private readonly IGenericRepository<Candidate> _repository;
        private readonly IGenericRepository<Question>  questionRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CandidateQuestionManager(IGenericRepository<Candidate> repository, IUnitOfWork unitOfWork, IGenericRepository<Question> questionRepo)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            this.questionRepo = questionRepo;
        }

        public async Task<bool> CreateQuestionAsync(ApplicationDto createApplicationDTO)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                // Create PersonalInformation object
                var personalInfo = new Candidate
                {
                    Email = createApplicationDTO.Email,
                    FirstName = createApplicationDTO.FirstName,
                    LastName = createApplicationDTO.LastName,
                    PhoneNumber = createApplicationDTO.PhoneNumber,
                    Nationality = createApplicationDTO.Nationality,
                    Address = createApplicationDTO.Address,
                    DateOfBirth = createApplicationDTO.DateOfBirth,
                    Gender = createApplicationDTO.Gender
                };

                // Save personal info
                var personalInfoSaved =  _repository.InsertAsync(personalInfo);
                await _unitOfWork.SaveChangesAsync();

                if (personalInfoSaved == null)
                {
                    await _unitOfWork.RollbackAsync();
                    return false;
                }

                var questions = new List<Question>();

                // Create questions
                foreach (var question in createApplicationDTO.Questions)
                {
                    var quest = new Question
                    {
                        QuestionText = question.QuestionText,
                        DateCreated = DateTime.Now,
                        QuestionType = question.QuestionType 
                    };

                    questions.Add(quest);
                }

                // Save questions
                var questionsSaved = await questionRepo.InsertMultipleAsync(questions);

                if (!questionsSaved)
                {
                    await _unitOfWork.RollbackAsync();
                    return false;
                }

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception appropriately (log, etc.)
                await _unitOfWork.RollbackAsync();
                throw new Exception("Error creating question", ex);
            }
        }


        public async Task<bool> UpdateQuestionAsync(QuestionDto createQuestionDTO)
        {
            var createQuestionModel = new Question
            {
                QuestionType = createQuestionDTO.QuestionType,
                QuestionText = createQuestionDTO.QuestionText,
                DateModified = DateTime.Now,
            };

            var res = await questionRepo.UpdateAsync(createQuestionModel);

            if (res)
            {
                await _unitOfWork.CommitAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Question> GetQuestionAsync(QuestionTypes questionType)
        {
            var question = questionType.ToString();
            var res = await questionRepo.GetFirstAsync(x=>x.QuestionType == questionType);
            return res;
        }

        public async Task<bool> DeleteQuestionAsync(int questionId)
        {
            try
            {
                var questionToDelete = await questionRepo.GetFirstAsync(x=>x.Id == questionId);
                if (questionToDelete == null)
                {
                    return false; // Question not found
                }

                await questionRepo.SoftDeleteAsync(questionToDelete);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception appropriately (log, etc.)
                throw new Exception("Error deleting question", ex);
            }
        }
    }
}
