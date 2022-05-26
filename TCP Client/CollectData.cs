using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TCP_Client
{
    public class JsonString
    {
        public DateTime Date { get; set; }
        public uint Id { get; set; }
        public decimal? Open { get; set; }
        public decimal? Close { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
    }
    struct DataFromServer
    {
        public DateTime Date;
        public uint Id;
        public string Symbol;
        public decimal Price;
    }

    struct DataValues
    {
        public DateTime Date;
        public uint Id;
        public decimal? Open;
        public decimal? Close;
        public decimal? High;
        public decimal? Low;
    }
    public class CollectData
    {
        List<DataFromServer> DataSecond = new List<DataFromServer>();
        List<DataValues> DataMinutes = new List<DataValues>();
        uint id = 0;
        bool flaga = false;
        

        public static CollectData Instance = new CollectData();


        public void AddToList(DateTime date, string symbol, string price)
        {
            
            if (date.Second == 0) flaga = true;
            if (id < 60 && flaga == true)
            {


                id++;
                
                if (decimal.TryParse(price.Replace(".",",").Trim(), out decimal pricedec))
                   
                    

                DataSecond.Add(new DataFromServer { Date = date, Id = id, Symbol = symbol, Price = pricedec });


                TakeValue();
                
            }
        }
        public void ShowList()
        {
            Console.WriteLine("Calling ShowList");
            foreach (DataFromServer data in DataSecond)
            {
                Console.WriteLine($"Date: {data.Date}, Id: {data.Id}, Symbol: {data.Symbol}, Price: {data.Price}");
            }
        }

        public void TakeValue()
        {
            if (id == 60)
            {
                decimal? lowestUnitPrice =
                (from prod in DataSecond
                 select prod.Price)
                .Min();

                decimal? HighestUnitPrice =
                (from prod in DataSecond
                 select prod.Price)
                .Max();

                decimal? OpenUnitPrice = DataSecond[0].Price;
                decimal? CloseUnitPrice = DataSecond[DataSecond.Count - 1].Price;
                DateTime DateList = DataSecond[0].Date;
                DataMinutes.Add(new DataValues
                {
                    Date = DateList,
                    Id = 0,
                    Open = OpenUnitPrice,
                    Close = CloseUnitPrice,
                    High= HighestUnitPrice,
                    Low = lowestUnitPrice
                });
                DataSecond.Clear();
                id = 0;
                SendTCP();
                //Console.WriteLine($"Low: {lowestUnitPrice}, High: {HighestUnitPrice}, Open: {OpenUnitPrice}, Close: {CloseUnitPrice}, List count: {DataMinutes.Count}");
                
            }
        }
       public void ShowValue()
        {
            foreach (DataValues data in DataMinutes)
            {
                Console.WriteLine($"Date: {data.Date}, Id: {data.Id}, Low: {data.Low}, High: {data.High}, Open: {data.Open}, Close: {data.Close}, List count: {DataMinutes.Count}");

            }
        }
        public void SendTCP()
        {
            foreach (DataValues data in DataMinutes)
            {
                var JsonSend = new JsonString
                {
                    Date = data.Date,
                    Id = data.Id,
                    Low = data.Low,
                    High = data.High,
                    Open = data.Open,
                    Close = data.Close,
                };
                string jsonString = JsonSerializer.Serialize(JsonSend);
                Client.Instance.SendMessage(jsonString);
                //Console.WriteLine($"Date: {data.Date}, Id: {data.Id}, Low: {data.Low}, High: {data.High}, Open: {data.Open}, Close: {data.Close}, List count: {DataMinutes.Count}");

            }
        }

    }
}
