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
       public int Id;
       public string Data;
    }
    public class CollectData
    {
        List<DataFromServer> Data = new List<DataFromServer> ();

        public CollectData()
        {
            Data.Add (new DataFromServer {Date= DateTime.Now, Id=0, Data="test"});
        }

        public void AddToList(DateTime date, int id , string data)
        {
            Data.Add(new DataFromServer { Date = date, Id = id, Data = data });
        }
        public void ShowList()
        {
            foreach (DataFromServer data in Data)
            {
                Console.WriteLine($"Date: {data.Date}, Id: {data.Id}, Data: {data.Data}");
            }
        }

    }
}
