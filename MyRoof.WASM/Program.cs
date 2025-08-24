using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyRoof.WASM;
using Core.Services;
using Core.InterFaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Регистрируем сервисы
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri("https://localhost:7239/") });

// Настраиваем HttpClient для API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7239/") });

await builder.Build().RunAsync();
