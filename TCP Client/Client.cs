using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace TCP_Client
{
    public class JsonSerialize
    {
        public DateTime Date { get; set; }
        public uint Id { get; set; }
        public decimal? Open { get; set; }
        public decimal? Close { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
    }
    public class Client
    {
        public static Client Instance = new Client();
        TcpClient tcp = new TcpClient();

        public async Task StartAsync(string server, int port)
        {
            Console.WriteLine($"Start connecting with server: {server}, port: {port}");
            await tcp.ConnectAsync(server, port);
            var readThread = new Thread(ReadDataAsync)
            {
                Priority = ThreadPriority.BelowNormal,
            };
            readThread.Start();
        }

        private async void ReadDataAsync()
        {
            var stream = tcp.GetStream();
            var pool = ArrayPool<byte>.Create();

            while (true)
            {
                var data = pool.Rent(1024);
                try
                {
                    await stream.ReadAsync(data);
                    var responseData = Encoding.ASCII.GetString(data);
                    Console.WriteLine(responseData);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    pool.Return(data);
                }
            }
        }

        public void Stop()
        {
            Console.WriteLine("Connection has ended");
            //tcp.Close();
        }

        public void SendMessage(string message)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
            NetworkStream stream = tcp.GetStream();

            // Send the message to the connected TcpServer.
            stream.WriteAsync(data, 0, data.Length);

            




            //tcp.GetStream().WriteAsync(Encoding.ASCII.GetBytes("hello"));
            Console.WriteLine("Sent: {0}", message);
        }
    }
}
