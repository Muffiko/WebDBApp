using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepairManagementSystem.Data;
using RepairManagementSystem.Extensions;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
string? _publicKeyBase64 = Env.GetString("JWT_PUBLIC_KEY");
byte[] _publicKeyBytes = Convert.FromBase64String(_publicKeyBase64);
RSA rsa = RSA.Create();
rsa.ImportSubjectPublicKeyInfo(_publicKeyBytes, out _);
var _publicKey = new RsaSecurityKey(rsa);

builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Env.GetString("JWT_ISSUER"),
            ValidAudience = Env.GetString("JWT_AUDIENCE"),
            IssuerSigningKey = _publicKey,
        };
    });

builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddAutoMapper(
    AutoMapperConfig.RegisterMappings,
    typeof(Program).Assembly
);

builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        }
    );
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1.0",
        new OpenApiInfo
        {
            Title = "RepairManagementSystemAPI",
            Version = "v1.0",
            Description = "ASP.NET Core Web API for RMSystem",
            Contact = new OpenApiContact { Name = "TAB", Email = "email@email.com" },
        }
    );
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            Description = "Please enter your Bearer token in the format **'Bearer &lt;token&gt;'**",
        }
    );

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                },
                new string[] { }
            },
        }
    );
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Computer Name (default local database Name)
    string machineName = Environment.MachineName;
    options.UseSqlServer($"Server={machineName};Database=RepairManagementDB;Trusted_Connection=True;TrustServerCertificate=True;");
});

// builder.Services.AddDbContext<ApplicationDbContext>(options =>
// {
//     options.UseInMemoryDatabase("DONTUSETHIS");
// });
var app = builder.Build();

// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     await RepairManagementSystem.DevDataSeeder.SeedAdminUserAsync(services);
// }

//using (var scope = app.Services.CreateScope())
//{
//   var services = scope.ServiceProvider;
//   try
//   {
//       var context = services.GetRequiredService<ApplicationDbContext>();
//       context.Database.Migrate();
//   }
//   catch (Exception ex)
//   {
//       var logger = services.GetRequiredService<ILogger<Program>>();
//       logger.LogError(ex, "Error while database data migration!");
//   }
//}

app.UseCors("AllowFrontend");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1.0/swagger.json", "RMS");
    });
}
app.MapControllers();

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
