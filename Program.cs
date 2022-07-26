using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ConsoleTables;

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

            var token = "";
            if (args.Length == 0)
            {
                Console.Write("What list would you like? ");
                token = Console.ReadLine();
            }
            else
            {
                token = args[0];
            }
            // token be the first argument of the program (not sure what that means)

            var url = $"https://one-list-api.herokuapp.com/items?access_token={token}";

            var responseAsStream = await client.GetStreamAsync(url);

            var items = await JsonSerializer.DeserializeAsync<List<Item>>(responseAsStream);

            var table = new ConsoleTable("Description", "Created At", "Completed");
            foreach (var item in items)
            {
                // Console.WriteLine($"The task {item.Text}, was created on {item.CreatedAt} and has a completion of: {item.CompletedStatus}");
                table.AddRow(item.Text, item.CreatedAt, item.CompletedStatus);
            }
            table.Write();
        }
    }
}
