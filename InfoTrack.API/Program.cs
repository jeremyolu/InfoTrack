using InfoTrack.API.Interfaces.Providers;
using InfoTrack.API.Interfaces.Repositories;
using InfoTrack.API.Interfaces.Services;
using InfoTrack.API.Providers;
using InfoTrack.API.Repositories;
using InfoTrack.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<ILocationRepository, LocationStaticRepository>();
builder.Services.AddScoped<ILocationService, LocationService>();

builder.Services.AddScoped<ISolicitorProvider, SolicitorScraperProvider>();
builder.Services.AddScoped<ISolicitorService, SolicitorService>();

builder.Services.AddHttpClient<ISolicitorProvider, SolicitorScraperProvider>(client =>
{
    client.BaseAddress = new Uri("https://www.solicitors.com/");
    client.DefaultRequestHeaders.Add("User-Agent", "InfoTrack");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("Cors");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
