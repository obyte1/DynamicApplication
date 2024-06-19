using CapitalPlacementTask.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CapitalPlacementTask.Data.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<CandidateAnswer> CandidateAnswers { get; set; }
        public DbSet<Question> Questions { get; set; }

    }
}
