using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using POSApi.Infrastructure.Data;
using POSApi.Domain.Interfaces;
using POSApi.WebApi.Mappings;
using POSApi.Infrastructure.Repositories;
using POSApi.Application.Services;
using NLog;
using NLog.Web;
using Microsoft.Extensions.Options;
using System.Reflection;
using POSApi.Application.Services.Implementations;
using POSApi.Application.Services.Interfaces;
using POSApi.Infrastructure.Data;
using POSApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting; // Add this using directive

var logger = NLog.LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProizvodiService, ProizvodiService>();
builder.Services.AddScoped<IKupciService, KupciService>();
builder.Services.AddScoped<IStavke_racunaService, Stavke_racunaService>();
builder.Services.AddScoped<IZaglavlje_racunaService, Zaglavlje_racunaService>();

builder.Services.AddAuthorization();
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();
builder.Services.AddSwaggerGen(c =>
{

    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Dokumentacija",
        Version = "v1",
        Description = "Ovo je API za moj projekt",
        Contact = new OpenApiContact
        {
            Name = "Petar Arlovic",
            Email = "petararlovic@gmail.com",
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

});

builder.Services.AddDbContext<AppDbContext>(options =>
{

    var connectionString = builder.Configuration.GetConnectionString("POSdb")!;
    if (string.IsNullOrEmpty(connectionString))
    {
        logger.Error("Connection string 'POSdb' nije pronađen u appsettings.json!");
        throw new InvalidOperationException("Connection string nije inicijaliziran.");
    }

    logger.Debug($"Connection string uspješno učitan: {connectionString}");
    options.UseSqlServer(connectionString);

});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

}
catch (Exception ex)
{
    logger.Error(ex, "Fatalna greška prilikom pokretanja aplikacije");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}