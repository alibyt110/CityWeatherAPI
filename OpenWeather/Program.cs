using OpenWeather.Models;
using OpenWeather.Services;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddHttpClient<OpenWeatherHttpService>(config =>
{
    config.BaseAddress = new Uri("http://api.openweathermap.org/");
});

var apiKey = builder.Configuration["OpenWeather:ApiKey"];

builder.Services.Configure<OpenWeatherOptions>(builder.Configuration.GetSection("OpenWeather"));
builder.Services.AddScoped<IOpenWeatherHttpService, OpenWeatherHttpService>();
builder.Services.AddScoped<IOpenWeatherService, OpenWeatherService>();

builder.Host.UseSerilog();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
