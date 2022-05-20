using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TCP_Client
{
    internal class GetAPi
    {
        static readonly HttpClient client = new HttpClient();

        public async Task ShowAsync()
        {
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage response = await client.GetAsync("http://api.nbp.pl/api/exchangerates/rates/c/usd/today/");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                
                Root? JsonRoot =
                 JsonSerializer.Deserialize<Root>(responseBody);
               
                
                Console.WriteLine($"Code: {JsonRoot.code}, Bid: {JsonRoot.rates[0].bid}, Ask: {JsonRoot.rates[0].ask}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
        public class Rate
        {
            public string no { get; set; }
            public string effectiveDate { get; set; }
            public double bid { get; set; }
            public double ask { get; set; }
        }

        public class Root
        {
            public string table { get; set; }
            public string currency { get; set; }
            public string code { get; set; }
            public List<Rate> rates { get; set; }
        }


    }

}

