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
    public class CollectData
    {
        List<DataFromServer> Data = new List<DataFromServer> ();
        uint id = 0;
        public CollectData()
        {
            //Data.Add (new DataFromServer {Date= DateTime.Now, Id=0, Symbol="test", Price=0});
        }

        public void AddToList(DateTime date, string symbol, string price)
        {
            if (id < 60)
            {
                id++;
                decimal pricedec = Convert.ToDecimal(price);
                Data.Add(new DataFromServer { Date = date, Id = id, Symbol = symbol, Price = pricedec });

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
            if(id==60)
            {
                System.Nullable<Decimal> lowestUnitPrice =
                (from prod in Data
                select prod.Price)
                .Min();

                System.Nullable<Decimal> HighestUnitPrice =
                (from prod in Data
                 select prod.Price)
                .Max();
                
                Decimal OpenUnitPrice = Data[0].Price;
                Decimal CloseUnitPrice = Data[Data.Count - 1].Price;
                

                Console.WriteLine($"Low: {lowestUnitPrice}, High: {HighestUnitPrice}, Open: {OpenUnitPrice}, Close: {CloseUnitPrice}");
            }
            else
            {
                Console.WriteLine("The minute interval has not ended");
            }
            
        }

    }
}
