using System.Collections.Generic;
using MetroAppRequest;
using System.Linq;

namespace MetroApp
{
    public class Stops
    {
        public List<Stop> StopCollection;

        public static IStopsProvider StopsProvider { get; set; } = new MetroRequest();

        List<Stop> DistinctByLines(Stop[] stops)
        {
            return stops.Aggregate(new List<Stop>(), (distinct, curStop) =>
            {
                Stop unique = distinct.Find(stop => stop.Name.Equals(curStop.Name));

                if (unique != null)
                {
                    unique.LinesId = unique.LinesId.Concat(curStop.LinesId).ToArray();
                    unique.Lines = unique.Lines.Concat(curStop.Lines).ToList();
                    unique.DistinctLines();
                }
                else
                {
                    curStop.DistinctLines();
                    distinct.Add(curStop);
                }

                return distinct;
            });
        }

        public List<Stop> GetNearStops()
        {
            StopCollection = DistinctByLines(Stop.FromJson(StopsProvider.NearStops()));

            return StopCollection;
        }
    }
}
