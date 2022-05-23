using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TCP_Client
{

    public class Root
    {
        public string symbol { get; set; }
        public string price { get; set; }

    }
    internal class GetAPi
    {
        static readonly HttpClient client = new HttpClient();

        public async Task ShowAsync(DateTime date)
        {
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage response = await client.GetAsync("https://api.binance.com/api/v3/ticker/price?symbol=BTCUSDT");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Root? JsonRoot = JsonSerializer.Deserialize<Root>(responseBody);
                
                CollectData.Instance.AddToList(date, JsonRoot.symbol, JsonRoot.price);

                // Console.WriteLine($"Symbol: {JsonRoot.symbol}, Price: {JsonRoot.price}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

        }
        
    }

}

