using ArtCollectionApi.Models;
using ArtCollectionApi.Services;
using ArtCollectionApi;
using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ArtCollectionDatabaseSettings>(
    builder.Configuration.GetSection("ArtCollectionDatabase"));

DBConfigSetUp.ConfigureServices(builder.Services, builder.Configuration.GetSection("ArtCollectionDatabase").Get<ArtCollectionDatabaseSettings>());

builder.Services
    .AddCors(options =>
    {
        options
            .AddPolicy(name: "CorsPolicy",
            policy =>
            {
                policy.WithOrigins("http://localhost:3001", "http://localhost:3000", "https://art-collection-dev.herokuapp.com/", 
                "https://art-collection-tatte.herokuapp.com/").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            });
    });
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

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapControllers();
app.Run();
