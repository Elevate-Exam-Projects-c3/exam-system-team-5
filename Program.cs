using exam_system.Common.Middleware;
using exam_system.Domain.Entities.Diplomas;
using exam_system.Features;
using exam_system.Infrastructure;
using exam_system.Persistence;
using exam_system.Persistence.Context;
using exam_system.Persistence.DataAccess;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(opt =>
opt.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add services from different layers
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddFeatureServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddTransient<TransactionMiddleware>();
builder.Services.AddMapsterConfig();

//validation on Request
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


var app = builder.Build();

// Seed Database automatically on startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        await AppDbContextSeed.SeedAsync(context, logger);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred during database migration/seeding.");
    }
}

// Enable Swagger UI in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Examination System API v1");
        c.RoutePrefix = "swagger";
    });
}
//transaction middleware registeration 

app.UseHttpsRedirection();

app.UseMiddleware<TransactionMiddleware>();
app.UseAuthorization();


app.UseMiddleware<TransactionMiddleware>();
app.UseMiddleware<GlobalExceptionMiddleware>();


app.MapControllers();

app.Run();
