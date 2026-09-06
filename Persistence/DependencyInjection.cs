using exam_system.Common.Middleware;
using exam_system.Persistence.Context;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? "Server=(localdb)\\mssqllocaldb;Database=ExaminationSystemDb;Trusted_Connection=True;MultipleActiveResultSets=true";

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        // auto mapper version 13.0.0 not have any commercial license, so we can use it for free. But version 14.0.0 and above have a commercial license
        //services.AddAutoMapper(typeof(Program).Assembly);

        // version 11.0.0 not have any commercial license, so we can use it for free. But version 12.0.0 and above have a commercial license, so we need to use version 11.0.0 for free usage.
        services.AddMediatR(typeof(Program));


        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<TransactionMiddleware>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();



        return services;
    }
}
