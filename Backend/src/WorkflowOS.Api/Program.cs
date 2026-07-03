using FluentValidation;
using MediatR;
using WorkflowOS.Api.Extensions;
using WorkflowOS.Application;
using WorkflowOS.Application.Behaviors;
using WorkflowOS.Application.Common.Behaviors;
using WorkflowOS.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);

// MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(WorkflowOS.Application.AssemblyReference).Assembly);
});

// FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(WorkflowOS.Application.AssemblyReference).Assembly);



builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>));

builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(LoggingBehavior<,>));

builder.Services.AddValidatorsFromAssembly(
    typeof(AssemblyReference).Assembly);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(
        typeof(AssemblyReference).Assembly);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();