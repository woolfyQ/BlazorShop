using MyRoof.API.Services;
using MyRoof.API.Models;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Настраиваем EmailSettings из конфигурации
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// Регистрируем EmailService
builder.Services.AddScoped<IEmailService, EmailServiceAlternative>();

// Настраиваем CORS для Blazor WASM
builder.Services.AddCors(options =>
{
    // Development CORS
    options.AddPolicy("AllowBlazorWasm",
        policy =>
        {
            policy.WithOrigins(
                    "http://localhost:5003", 
                    "https://localhost:5003",
                    "http://localhost:7184",
                    "https://localhost:7184",
                    "http://localhost:52291",
                    "https://localhost:44343"
                  )
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
    
    // Production CORS
    options.AddPolicy("AllowProduction",
        policy =>
        {
            policy.WithOrigins(
                    "https://your-domain.com",
                    "https://www.your-domain.com"
                  )
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Настраиваем CORS в зависимости от окружения
if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowBlazorWasm");
}
else
{
    app.UseCors("AllowProduction");
}

app.UseAuthorization();

app.MapControllers();

app.Run();
