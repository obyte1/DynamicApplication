using CapitalPlacementTask.Business.Services.Interfaces;
using CapitalPlacementTask.Data.DTOs;
using CapitalPlacementTask.Data.Models;
using CapitalPlacementTask.Data.Repository.Interface;

namespace CapitalPlacementTask.Business.Services.Implementation
{
    public class CandidateManager : ICandidateManager
    {
        private readonly IGenericRepository<Candidate> _repository;
        private readonly IGenericRepository<CandidateAnswer> _candidateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CandidateManager(IGenericRepository<Candidate> repository, IUnitOfWork unitOfWork, IGenericRepository<CandidateAnswer> candidateRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _candidateRepository = candidateRepository;
        }

        public async Task<bool> SaveACandidateAsync(CandidateDto request)
        {
            try
            {
                // Save personal information
                var personalInfo = new Candidate
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber,
                    Nationality = request.Nationality,
                    Address = request.Address,
                    DateOfBirth = request.DateOfBirth,
                    Gender = request.Gender
                };

                await _repository.InsertAsync(personalInfo);
                await _unitOfWork.SaveChangesAsync();

                // Save answers to questions
                var answers = new List<CandidateAnswer>();

                foreach (var question in request.Questions)
                {
                    var answer = new CandidateAnswer
                    {
                        CandidateId = request.Email,
                        QuestionId = question.QuestionId,
                        Response = question.Answer
                    };

                    answers.Add(answer);
                }
                await _candidateRepository.InsertMultipleAsync(answers);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                // Rollback transaction if necessary
                await _unitOfWork.RollbackAsync();
                return false;
            }
        }
    }

}
