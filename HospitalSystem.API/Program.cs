using HospitalSystem.Application;
using HospitalSystem.Persistence;
using HospitalSystem.Infrastructure;
using HospitalSystem.Mapper;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.Authorization;
using HospitalSystem.Application.Exceptions;
using Serilog;
using HospitalSystem.Infrastructure.ServiceDiscovery;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter());
}); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddCustomMapper();

var corsPolicy = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy,
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Hospital API",
        Version = "v1",
        Description = "Hospital API swagger client."
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "After typing 'Bearer' and leaving a space, you can enter the Token \r\n\r\n For example: \'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\'"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Host.UseSerilog();

var app = builder.Build();

app.UseCors(corsPolicy);

app.UseMiddleware<ExceptionMiddleware>();
//app.UseMiddleware<TokenBlacklistMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseSerilogRequestLogging();

app.RegisterWithConsul(app.Lifetime);

app.Run();
