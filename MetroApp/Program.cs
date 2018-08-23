using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace MetroApp
{
	class Program
	{
		static void Main(string[] args)
		{
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

			Stops Stops = new Stops();
			Stops.GetNearStops();

			List<Stop> colection = Stops.StopCollection.Aggregate(new List<Stop>(),
				(distinct, curStop) =>
				{
					curStop.Lines = curStop.Lines.Distinct().ToArray();
					Stop duplicate = distinct.Find(stop => stop.Name.Equals(curStop.Name));

					if (duplicate != null)
						duplicate.Lines = new HashSet<string>(duplicate.Lines.Concat(curStop.Lines)).ToArray();
					else
						distinct.Add(curStop);

					return distinct;
				});

			foreach (Stop Stop in colection)
			{
				Console.WriteLine(Stop.ToString());
			}

			Console.ReadKey();
		}
	}
}
