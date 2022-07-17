using ArtCollectionApi.Models;
using MongoDB.Driver;

namespace ArtCollectionApi.Services
{
    public class PrintKindsService      
    {
        private readonly IMongoCollection<PrintKind> _printKindsCollection;
        public PrintKindsService(IMongoCollection<PrintKind> printKinds)
        {
            _printKindsCollection = printKinds;
        }
        public async Task<List<PrintKind>> GetAsync() =>
            await _printKindsCollection.Find(_ => true).ToListAsync();

        public async Task<PrintKind?> GetAsync(string id) =>
            await _printKindsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(PrintKind newPrint) =>
            await _printKindsCollection.InsertOneAsync(newPrint);

        public async Task UpdateAsync(string id, PrintKind updatedPrint) =>
            await _printKindsCollection
                .ReplaceOneAsync(x => x.Id == id, updatedPrint);

        public async Task RemoveAsync(string id) =>
            await _printKindsCollection.DeleteOneAsync(x => x.Id == id);

    }
}