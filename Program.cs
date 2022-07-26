using System;
using System.Net.Http;

namespace OneListClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();

            var responseAsString = await client.GetStringAsync("https://one-list-api.herokuapp.com/items?access_token=sdg-handbook");
            Console.WriteLine(responseAsString);
        }
    }
}
