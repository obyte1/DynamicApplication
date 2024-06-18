using CapitalPlacementTask.Data.DTOs;

namespace CapitalPlacementTask.Business.Services.Interfaces
{
    public interface ICandidateManager
    {
        Task<bool> SaveACandidateAsync(CandidateDto request);
    }
}
