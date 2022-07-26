using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OneListClient
{
    class Program
    {
        class Item
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }
            [JsonPropertyName("text")]
            public string Text { get; set; }
            [JsonPropertyName("complete")]
            public bool Complete { get; set; }
            [JsonPropertyName("created_at")]
            public DateTime CreatedAt { get; set; }
            [JsonPropertyName("updated_at")]
            public DateTime UpdatedAt { get; set; }

            public string CompletedStatus
            {
                get
                {
                    // works assigning a variable with if/else
                    // returning something
                    return Complete ? "completed" : "not completed";
                }
            }
        }

        static async Task Main(string[] args)
        {
            var client = new HttpClient();

            var responseAsStream = await client.GetStreamAsync("https://one-list-api.herokuapp.com/items?access_token=sdg-handbook");

            var items = await JsonSerializer.DeserializeAsync<List<Item>>(responseAsStream);

            foreach (var item in items)
            {
                Console.WriteLine($"The task {item.Text}, was created on {item.CreatedAt} and has a completion of: {item.CompletedStatus}");
            }
        }
    }
}
