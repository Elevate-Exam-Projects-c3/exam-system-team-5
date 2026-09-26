using exam_system.Features.Analytics.Behaviors;
using exam_system.Common.Behaviors;
using exam_system.Common.CurrentUser;
using exam_system.Common.Middleware;
using exam_system.Persistence.Context;
using exam_system.Persistence.DataAccess;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace exam_system.Persistence;
public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
            //?? "Server=(localdb)\\mssqllocaldb;Database=ExaminationSystem_Team5_Db;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False";
        
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddScoped<IDbConnection>(_ => new SqlConnection(connectionString));
        services.AddMemoryCache();
        services.AddMediatR(typeof(Program));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

        services.AddHttpContextAccessor();
        //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(SaveChangesBehavior<,>));
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<TransactionMiddleware>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<ExpiredAttemptsSweepJob>();



        return services;
    }

    public static IServiceCollection AddMapsterConfig(this IServiceCollection services)
    {

        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMapper>(new Mapper(mappingConfig));
        return services;
    }

    public static IServiceCollection AddFluentValidationConfig(
    this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
