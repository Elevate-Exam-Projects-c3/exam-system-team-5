using exam_system.Common.Interfaces;
using exam_system.Infrastructure.Services;

namespace exam_system.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
