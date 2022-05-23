using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP_Client
{
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
        List<DataFromServer> Data = new List<DataFromServer>();
        List<DataValues> dataValues = new List<DataValues>();
        uint id = 0;
        bool flaga = false;
        public CollectData()
        {
            //Data.Add (new DataFromServer {Date= DateTime.Now, Id=0, Symbol="test", Price=0});
        }

        public static CollectData Instance = new CollectData();


        public void AddToList(DateTime date, string symbol, string price)
        {
            if (date.Second == 0) flaga = true;
            if (id < 60 && flaga == true)
            {


                id++;
                Console.WriteLine(price);
                if (decimal.TryParse(price.Replace(".",",").Trim(), out decimal pricedec))
                    Console.WriteLine(pricedec);


                Data.Add(new DataFromServer { Date = DateTime.Now, Id = id, Symbol = symbol, Price = pricedec });



                TakeValue();
            }
        }
        public void ShowList()
        {
            Console.WriteLine("Calling ShowList");
            foreach (DataFromServer data in Data)
            {
                Console.WriteLine($"Date: {data.Date}, Id: {data.Id}, Symbol: {data.Symbol}, Price: {data.Price}");
            }
        }

        public void TakeValue()
        {
            if (id == 60)
            {
                decimal? lowestUnitPrice =
                (from prod in Data
                 select prod.Price)
                .Min();

                decimal? HighestUnitPrice =
                (from prod in Data
                 select prod.Price)
                .Max();

                decimal? OpenUnitPrice = Data[0].Price;
                decimal? CloseUnitPrice = Data[Data.Count - 1].Price;
                DateTime DateList = Data[0].Date;
                dataValues.Add(new DataValues
                {
                    Date = DateList,
                    Id = 0,
                    Open = OpenUnitPrice,
                    Close = CloseUnitPrice,
                    High= HighestUnitPrice,
                    Low = lowestUnitPrice
                });
                Data.Clear();
                id = 0;
                Console.WriteLine($"Low: {lowestUnitPrice}, High: {HighestUnitPrice}, Open: {OpenUnitPrice}, Close: {CloseUnitPrice}, List count: {dataValues.Count}");
                
            }
            else
            {
                Console.WriteLine("The minute interval has not ended");
            }

        }

    }
}
