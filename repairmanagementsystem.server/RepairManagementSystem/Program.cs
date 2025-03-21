using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1.0", new OpenApiInfo
    {
        Title = "RepairManagementSystemAPI",
        Version = "v1.0",
        Description = "ASP.NET Core Web API for RMSystem",
        Contact = new OpenApiContact
        {
            Name = "TAB",
            Email = "email@email.com",
        }
    });
});
var app = builder.Build();

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

app.Run();

