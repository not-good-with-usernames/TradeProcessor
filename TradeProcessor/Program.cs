using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace TradeProcessor.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await using var fileStream = File.OpenRead("Trades.txt");
            using var streamReader = new StreamReader(fileStream);
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                var url = "https://localhost:44328/tradeprocessor";
                using var client = new HttpClient();

                try
                {
                    var pair = new KeyValuePair<string, string>("input", line);

                    var keyValuePairs = new List<KeyValuePair<string, string>>{pair};

                    await client.PostAsync(url, new FormUrlEncodedContent(keyValuePairs));
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e);
                }
            }
        }
    }
}
