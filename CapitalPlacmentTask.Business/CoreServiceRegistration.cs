using CapitalPlacementTask.Business.Services.Implementation;
using CapitalPlacementTask.Business.Services.Interfaces;
using CapitalPlacementTask.Data.Repository.Implementation;
using CapitalPlacementTask.Data.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace CapitalPlacementTask.Business
{
    public class CoreServiceRegistration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICandidateManager, CandidateManager>();
            services.AddScoped<ICandidateQuestionManager, CandidateQuestionManager>();
        }

    }
}
