using System.Linq;
using ArtCollectionApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ArtCollectionApi.Services
{
    public class PrintsService
    {
        private readonly IMongoCollection<Print> _printsCollection;

        public PrintsService(IMongoCollection<Print> prints)
        {
            _printsCollection = prints;
        }

        public async Task<List<Print>> GetAsync() =>
            await _printsCollection.Find(_ => true).ToListAsync();

        public async Task<List<string>> GetPrintKindsAsync() =>
            await GetFielsListAsync("PrintKind");

        public async Task<List<string>> GetGetArtistsAsync() =>
            await GetFielsListAsync("ArtistName");

        public async Task<List<string>> GetSourcesAsync() =>
            await GetFielsListAsync("Source");

        public async Task<List<string>> GetFielsListAsync(string field)
        {
            return await _printsCollection
                .Distinct<string>(field, new BsonDocument())
                .ToListAsync();
        }

        public ProjectionDefinitionBuilder<Print> PrintProjection() =>
            Builders<Print>.Projection;

        public async Task<Print?> GetAsync(string id) =>
            await _printsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Print newPrint)
        {
            newPrint.LastUpdateTime = DateTime.Now;
            await _printsCollection.InsertOneAsync(newPrint);
        }

        public async Task UpdateAsync(string id, Print updatedPrint)
        {
            updatedPrint.LastUpdateTime = DateTime.Now;
            await _printsCollection
                .ReplaceOneAsync(x => x.Id == id, updatedPrint);
        }

        public async Task RemoveAsync(string id) =>
            await _printsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
