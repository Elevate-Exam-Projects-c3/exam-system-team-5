using exam_system.Common.Behaviors;
using exam_system.Common.Constants;
using exam_system.Common.Middleware;
using exam_system.Infrastructure.Services;
using exam_system.Infrastructure.Settings;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
            services.AddAuthorization(option =>
            {
                option.AddPolicy(AppPolicies.RequireAdmin, policy => policy.RequireRole(AppRole.Admin));

                option.AddPolicy(AppPolicies.RequireStudent, policy => policy.RequireRole(AppRole.Student));
            });
            services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationResultHandler>();

            services.AddSingleton(Channel.CreateBounded<MimeMessage>(new BoundedChannelOptions(1000)
            {
                FullMode = BoundedChannelFullMode.Wait
            }));

            services.AddHostedService<EmailBackgroundService>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();


            return services;
        }
    }
}
