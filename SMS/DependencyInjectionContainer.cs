using Services.Services;
using Services.IServices;
using Repository.IRepositories;
using Repository.Repositories;
using Common.CurrentUser;

namespace API
{
    public static  class DependencyInjectionContainer
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAccountServices, AccountServices>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<IFilterRepository, FilterRepository>();
            services.AddScoped<IFilterService, FilterService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IParentService, ParentService>();
            services.AddScoped<IParentRepository, ParentRepository>();

            return services;
        }
       
    }
}
