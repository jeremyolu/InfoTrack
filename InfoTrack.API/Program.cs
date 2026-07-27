using InfoTrack.API.Interfaces.Providers;
using InfoTrack.API.Interfaces.Repositories;
using InfoTrack.API.Interfaces.Services;
using InfoTrack.API.Providers;
using InfoTrack.API.Repositories;
using InfoTrack.API.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddAuthorization();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ISolicitorService, SolicitorService>();
builder.Services.AddScoped<ISolicitorProvider, SolicitorScraperProvider>();

builder.Services.AddHttpClient<ISolicitorProvider, SolicitorScraperProvider>(client =>
{
    client.BaseAddress = new Uri("https://www.solicitors.com/");
    client.DefaultRequestHeaders.Add("User-Agent", "InfoTrack");
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "InfoTrack.Auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.None;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true; 

        options.LoginPath = "/infotrack/api/auth/login";
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return Task.CompletedTask;
        };
        options.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return Task.CompletedTask;
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("Cors");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
