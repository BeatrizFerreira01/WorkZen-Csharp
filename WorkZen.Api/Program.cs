using System.Reflection;
using System.Text.Json.Serialization;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WorkZen.Api.Data;
using WorkZen.Api.Infrastructure.Filters;
using WorkZen.Api.Infrastructure.Swagger;
using WorkZen.Api.Services;
using WorkZen.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Controllers + JSON config
builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add<ApiExceptionFilter>();
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition =
            JsonIgnoreCondition.WhenWritingNull;
    });

// DbContext (SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddHttpContextAccessor();

// API Versioning
builder.Services
    .AddApiVersioning(options =>
    {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.ReportApiVersions = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WorkZen API",
        Version = "v1",
        Description = "API para bem-estar mental (FIAP – WorkZen)"
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath))
        opt.IncludeXmlComments(xmlPath);
});

// Health checks
builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>("database");

// Services
builder.Services.AddScoped<IMeditationService, MeditationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                $"WorkZen API {description.GroupName.ToUpperInvariant()}"
            );
        }
    });
}

app.UseHttpsRedirection();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

public partial class Program { }