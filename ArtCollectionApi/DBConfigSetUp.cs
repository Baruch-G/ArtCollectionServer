using ArtCollectionApi.Models;
using System.Configuration;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using ArtCollectionApi.Services;
namespace ArtCollectionApi
{
    public static class DBConfigSetUp {
        
        public static void ConfigureServices(IServiceCollection services, ArtCollectionDatabaseSettings settings)
        {
            ConfigureMongoDb(services, settings);
            services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing());
        }

        private static void ConfigureMongoDb(IServiceCollection services, ArtCollectionDatabaseSettings settings)
        {
            var db = CreateMongoDatabase(settings);
            AddMongoDbService<  UsersService, User>(settings.UsersCollectionName);
            AddMongoDbService<  PrintsService, Print>(settings.PrintsCollectionName);
            void AddMongoDbService<TService, TModel>(string collectionName)
            {
                services.AddSingleton(db.GetCollection<TModel>(collectionName));
                services.AddSingleton(typeof(TService));
            }
        }

        private static IMongoDatabase CreateMongoDatabase(ArtCollectionDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            return client.GetDatabase(settings.DatabaseName);
        }
    }
}