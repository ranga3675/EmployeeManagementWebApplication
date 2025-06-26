using MongoDbWebApplication.Interfaces;
using System.Collections.Generic;

namespace MongoDbWebApplication.Services
{
    public class Service : IService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public Service(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MongoDbApi") ?? throw new InvalidOperationException("Connection string 'MongoDbConnection' not found.");
        }
        public async Task<IEnumerable<MongoDb>> GetAllAsync<MongoDb>()
        {
            try
            {            
                HttpResponseMessage response = await _httpClient.GetAsync(_connectionString);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<MongoDb>>(data, new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                // Fix for CS8603: Ensure result is not null before returning
                return result ?? new List<MongoDb>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<MongoDb> GetByIdAsync<MongoDb>(string id)
        {
            // Implementation logic here  
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_connectionString}/{id}");
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                var result = System.Text.Json.JsonSerializer.Deserialize<MongoDb>(data, new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                // Fix for CS8603: Ensure result is not null before returning
                return result ?? throw new InvalidOperationException("Item not found.");

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task CreateAsync<MongoDb>(MongoDb item)
        {
            try
            {
                var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(item), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(_connectionString, jsonContent);
                response.EnsureSuccessStatusCode();
                await response.Content.ReadAsStringAsync(); // Removed the return statement as the method returns Task.
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateAsync<MongoDb>(string id, MongoDb item)
        {
            // Implementation logic here
            try
            {
                var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(item), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync($"{_connectionString}/{id}", jsonContent);
                response.EnsureSuccessStatusCode();
                await response.Content.ReadAsStringAsync(); // Removed the return statement as the method returns Task.

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteAsync<MongoDb>(string id)
        {
            // Implementation logic here
            try
            {
                
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_connectionString}/{id}");
                response.EnsureSuccessStatusCode();
                await response.Content.ReadAsStringAsync(); // Removed the return statement as the method returns Task.
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
