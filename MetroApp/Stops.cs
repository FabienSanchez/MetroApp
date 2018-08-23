using System;
using System.Net;
using System.IO;
using MoreLinq;
using System.Collections.Generic;
using MetroAppRequest;

namespace MetroApp
{
	class Stops
	{
		public Stop[] StopCollection;

		MetroRequest MetroRequest = new MetroRequest();

		public IEnumerable<Stop> DistinctStops => StopCollection.DistinctBy(Stop => Stop.Name);

		public Stop[] GetNearStops()
		{
			return StopCollection = Stop.FromJson(MetroRequest.NearLines());
		}
	}
}
