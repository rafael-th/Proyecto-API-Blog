using Blazored.LocalStorage;
using ClienteBlazorWASM;
using ClienteBlazorWASM.Services;
using ClienteBlazorWASM.Services.IServices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//Agregar servicios aqui
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IServiceAuthentication, ServiceAuthentication>();

//Para usar el Local Storage del navegador
builder.Services.AddBlazoredLocalStorage();

//Agregar para la autenticación y autorización
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>( s => s.GetRequiredService<AuthStateProvider>() );

await builder.Build().RunAsync();
