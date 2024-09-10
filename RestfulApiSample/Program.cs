using Microsoft.Extensions.DependencyInjection;
using RestfulApiSample.Extentions;
using RestfulApiSample.Extentions.ErrorHandling.ByConventions;
using RestfulApiSample.Settings;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true)
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var configuration = configurationBuilder.Build();

//Not good way
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Add services to the container.

builder.Services.Configure<MySettings>(builder.Configuration.GetSection(nameof(MySettings)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseLogUrl();

app.UseLogUserAgent();

app.Run();


