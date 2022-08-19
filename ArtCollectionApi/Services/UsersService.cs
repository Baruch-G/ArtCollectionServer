using ArtCollectionApi.Models;
using MongoDB.Driver;

namespace ArtCollectionApi.Services
{
    public class UsersService
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UsersService(IMongoCollection<User> users)
        {
            _usersCollection = users;
        }

        public async Task<List<User>> GetAsync() =>
            await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<User?> GetAsync(string id) =>
            await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(User newUser) {
            newUser.CreationDate = DateTime.Now;
            await _usersCollection.InsertOneAsync(newUser);
        }
        
        public async Task UpdateAsync(string id, User updatedUser) =>
            await _usersCollection
                .ReplaceOneAsync(x => x.Id == id, updatedUser);

        public async Task RemoveAsync(string id) =>
            await _usersCollection.DeleteOneAsync(x => x.Id == id);
    }
}
