using InfoTrack.API.Interfaces.Repositories;
using InfoTrack.API.Interfaces.Services;
using InfoTrack.API.Repositories;
using InfoTrack.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<ILocationRepository, LocationStaticRepository>();
builder.Services.AddScoped<ILocationService, LocationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
