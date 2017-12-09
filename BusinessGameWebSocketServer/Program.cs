using System;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace BusinessGameWebSocketServer
{
    class Program
    {

        public static Database database;

        static void Main(string[] args)
        {

            database = new Database(true);
            //Console.WriteLine(database.cities["Manger"]["properties"]["Lervikvegen 98"].ToString());

            var wssv = new WebSocketServer(4649);
            wssv.AddWebSocketService<Echo>("/Echo");
            wssv.AddWebSocketService<InGame>("/InGame");

            wssv.Start();
            if (wssv.IsListening)
            {
                Console.WriteLine("Listening on port {0}, and providing WebSocket services:", wssv.Port);
                foreach (var path in wssv.WebSocketServices.Paths)
                    Console.WriteLine("- {0}", path);
            }

            string com = "";
            do
            {
                Console.WriteLine("\nCommand me >");
                com = Console.ReadLine();
            }
            while (!com.Equals("stop"));

            wssv.Stop();
        }

        public static void SaveDatabase()
        {
            database.SaveDatabase();
        }
    }
}
