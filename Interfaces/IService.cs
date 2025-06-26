using MongoDbWebAppplication.Models;

namespace MongoDbWebApplication.Interfaces
{
    public interface IService
    {
        Task<IEnumerable<MongoDb>> GetAllAsync<MongoDb>();
        Task<MongoDb> GetByIdAsync<MongoDb>(string id);
        Task CreateAsync<MongoDb>(MongoDb item);
        Task UpdateAsync<MongoDb>(string id, MongoDb item);
        Task DeleteAsync<MongoDb>(string id);
    }
}
