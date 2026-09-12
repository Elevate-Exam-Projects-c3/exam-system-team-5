using FluentValidation;
using MediatR;
using System.Reflection;

namespace exam_system.Features
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFeatureServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
