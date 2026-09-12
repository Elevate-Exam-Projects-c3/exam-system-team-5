using exam_system.Common.Middleware;
using exam_system.Persistence.Context;
using exam_system.Persistence.DataAccess;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace exam_system.Persistence;
public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
            //?? "Server=(localdb)\\mssqllocaldb;Database=ExaminationSystem_Team5_Db;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False";

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddMediatR(typeof(Program));

        //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(SaveChangesBehavior<,>));
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<TransactionMiddleware>();

        //services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection AddMapsterConfig(this IServiceCollection services)
    {

        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMapper>(new Mapper(mappingConfig));
        return services;
    }
}
