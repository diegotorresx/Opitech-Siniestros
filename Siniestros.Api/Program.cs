using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Siniestros.Application.Accidents.Abstractions;
using Siniestros.Application.Common.Behaviors;
using Siniestros.Infrastructure.Persistence;
using Siniestros.Infrastructure.Repositories;
using Siniestros.Domain.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MediatR + Validators (Application assembly)
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Siniestros.Application.Accidents.Create.CreateAccidentCommand).Assembly));

builder.Services.AddValidatorsFromAssembly(typeof(Siniestros.Application.Accidents.Create.CreateAccidentCommand).Assembly);

// Pipeline validation
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// EF Core SQL Server
builder.Services.AddDbContext<SiniestrosDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository
builder.Services.AddScoped<IAccidentRepository, AccidentRepository>();

builder.Services.AddProblemDetails();

var app = builder.Build();

// Global exception handling -> ProblemDetails
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        ProblemDetails problem;
        int status;

        switch (ex)
        {
            case ValidationException vex:
                status = StatusCodes.Status400BadRequest;
                problem = new ProblemDetails
                {
                    Title = "Validation error",
                    Status = status,
                    Detail = "One or more validation errors occurred."
                };
                problem.Extensions["errors"] = vex.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
                break;
            case DomainException dex:
                status = StatusCodes.Status400BadRequest;
                problem = new ProblemDetails
                {
                    Title = "Domain rule violation",
                    Status = status,
                    Detail = dex.Message
                };
                break;
            default:
                status = StatusCodes.Status500InternalServerError;
                problem = new ProblemDetails
                {
                    Title = "Unhandled error",
                    Status = status,
                    Detail = ex?.Message ?? "Unexpected error."
                };
                break;
        }

        context.Response.StatusCode = status;
        await context.Response.WriteAsJsonAsync(problem);
    });
});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Siniestros API v1");
    c.RoutePrefix = "swagger";
});

app.MapControllers();
app.Run();

public partial class Program { }
