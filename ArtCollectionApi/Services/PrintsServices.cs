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

        public async Task<List<string>> GetPrintKindsAsync()
        {
            var fieldsBuilder = Builders<Print>.Projection;
            var fields = fieldsBuilder.Include(d => d.PrintKind);

            return await _printsCollection
                .Distinct<string>("PrintKind", new BsonDocument())
                .ToListAsync(); //Find(_ => true).Project<Print>(fields).ToListAsync();
        }

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
