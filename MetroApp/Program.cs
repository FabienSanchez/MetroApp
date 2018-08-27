using System;
using System.Net;

namespace MetroApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            Stops Stops = new Stops();
            var NearStops = Stops.GetNearStops();

            foreach (Stop Stop in NearStops)
            {
                Console.WriteLine(Stop.ToString());
            }

            Console.ReadKey();
        }
    }
}
