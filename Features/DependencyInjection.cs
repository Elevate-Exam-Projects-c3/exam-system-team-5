using exam_system.Common.Behaviors;
using exam_system.Infrastructure.Services;
using exam_system.Infrastructure.Settings;
using FluentValidation;
using MediatR;
using MimeKit;
using System.Reflection;
using System.Threading.Channels;

namespace exam_system.Features
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFeatureServices(this IServiceCollection services)
        {
            services.AddOptions<EmailSettings>()
                .BindConfiguration("EmailSettings")
                .ValidateDataAnnotations()
                .ValidateOnStart();
           
            services.AddSingleton(Channel.CreateBounded<MimeMessage>(new BoundedChannelOptions(1000)
            {
                FullMode = BoundedChannelFullMode.Wait
            }));

            services.AddHostedService<EmailBackgroundService>();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
