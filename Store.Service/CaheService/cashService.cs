using StackExchange.Redis;
using Store.Service.CaheSeervice;
using System.Text.Json;

namespace Store.Service.CaheService
{


    public class cashService : IcashService
    {
        private readonly IDatabase _database;
        public cashService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<string> GetCashResponeAsync(string Key)
        {
            var cashedResponse = await _database.StringGetAsync(Key);

            if (cashedResponse.IsNullOrEmpty)
                return null;

            return cashedResponse.ToString();
        }

        public async Task SetCashResponseAsync(string Key, object response, TimeSpan TimeToLive)
        {
            if (response == null)
                return;

            var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var SerializeedResponse = JsonSerializer.Serialize(response, option);

            await _database.StringSetAsync(Key, SerializeedResponse, TimeToLive );


        }
    }
}
