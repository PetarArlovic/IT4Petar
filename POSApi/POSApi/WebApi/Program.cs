using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using POSApi.Infrastructure.Data;
using POSApi.Domain.Interfaces;
using POSApi.WebApi.Mappings;
using POSApi.Infrastructure.Repositories;
using NLog.Web;
using System.Reflection;
using POSApi.Application.Services.Implementations;
using POSApi.Application.Services.Interfaces;
using Swashbuckle.AspNetCore.Filters;
using POSApi.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using POSApi.Infrastructure.Extensions;
using Microsoft.Extensions.FileProviders;

{
    var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProizvodiRepository, ProizvodiRepository>();
builder.Services.AddScoped<IKupciRepository, KupciRepository>();
builder.Services.AddScoped<IStavke_racunaRepository, Stavke_racunaRepository>();
builder.Services.AddScoped<IZaglavlje_racunaRepository, Zaglavlje_racunaRepository>();

builder.Services.AddScoped<IKupciAdminService, KupciAdminService>();
builder.Services.AddScoped<IProizvodiAdminService, ProizvodiAdminService>();
builder.Services.AddScoped<IStavke_racunaAdminService, Stavke_racunaAdminService>();
builder.Services.AddScoped<IZaglavlje_racunaAdminService, Zaglavlje_racunaAdminService>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IProizvodiService, ProizvodiService>();
builder.Services.AddScoped<IKupciService, KupciService>();
builder.Services.AddScoped<IStavke_racunaService, Stavke_racunaService>();
builder.Services.AddScoped<IZaglavlje_racunaService, Zaglavlje_racunaService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendApp",
        builder =>
        {
            builder.WithOrigins("https://localhost:4200", "http://localhost:4200", "https://localhost:4000;", "http://localhost:4001") // Angular app address
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"TOKEN ERROR: {context.Exception.Message}");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine($"TOKEN VALIDATED: {context.SecurityToken}");
            return Task.CompletedTask;
        }
    };
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Please enter your token",
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();

    options.EnableAnnotations();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Dokumentacija",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Petar Arlovic",
            Email = "petararlovic@gmail.com",
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

});


builder.Services.AddDbContext<AppDbContext>(options =>
{

    var connectionString = builder.Configuration.GetConnectionString("POSdb")!;
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Connection string nije inicijaliziran.");
    }

    options.UseSqlServer(connectionString);

});

//BUILD
var app = builder.Build();

app.Use(async (context, next) => {
    Console.WriteLine($"Request received for: {context.Request.Path}");
    await next();
});

using (var scope = app.Services.CreateScope()) //DataSeed
{
    var services = scope.ServiceProvider;
    await SeedDataExtensions.SeedData(services);
}

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors("AllowFrontendApp");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

var angularFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "API", "publish", "wwwroot", "browser");

if (!Directory.Exists(angularFilesPath))
{
    angularFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "API", "wwwroot", "browser");
}

app.UseDefaultFiles(new DefaultFilesOptions
{
    FileProvider = new PhysicalFileProvider(angularFilesPath),
    RequestPath = ""
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(angularFilesPath),
    RequestPath = ""
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapFallbackToFile("index.html", new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(angularFilesPath)
    });
});

app.Run();

}
