using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Toro.Domain;
using Toro.Repository;
using Toro.Repository.Context;

namespace Toro.Service.Extensions {
    public static class ServiceExtensions {
        public static IServiceCollection RegisterServices(this IServiceCollection services, string connectionString) {

            services.AddDbContext<ToroContext>(o => o.UseSqlite(connectionString));
            services.AddTransient<IToroContext, ToroContext>();

            //Serviços
            services.AddTransient<IInvestorRepository, InvestorRepository>();
            services.AddTransient<IPatrimonyRepository, PatrimonyRepository>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IUserService, UserService>();
            services.AddScoped<IdentityErrorDescriber, IdentityErrorDescriberService>();

            return services;
        }
    }
}
