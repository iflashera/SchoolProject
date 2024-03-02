using Services.Services;
using Services.IServices;
using Repository.IRepositories;
using Repository.Repositories;

namespace API
{
    public static  class DependencyInjectionContainer
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAccountServices, AccountServices>();
            return services;
        }
       
    }
}
