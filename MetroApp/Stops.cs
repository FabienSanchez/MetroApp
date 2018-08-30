using System.Collections.Generic;
using MetroAppRequest;
using System.Linq;

namespace MetroApp
{

    public class Stops
    {
        public List<Stop> StopCollection;
        public List<Line> LineCollection;

        public static IStopsProvider StopsProvider { get; set; } = new MetroRequest();

        List<Stop> DistinctByLines(Stop[] stops)
        {
            List<string> finalLines = new List<string>();

            List<Stop> finalStops = stops.Aggregate(new List<Stop>(), (distinct, curStop) =>
         {
             Stop unique = distinct.Find(stop => stop.Name.Equals(curStop.Name));

             if (unique != null)
             {
                 unique.Lines = unique.Lines.Concat(curStop.Lines).ToList();
                 unique.DistinctLines();
                 finalLines.AddRange(unique.Lines);
             }
             else
             {
                 curStop.DistinctLines();
                 finalLines.AddRange(curStop.Lines);
                 distinct.Add(curStop);
             }

             return distinct;
         });

            finalLines = finalLines.Distinct().ToList();

            if (finalLines.Count > 0)
                LineCollection = Lines.FromJson(StopsProvider.Lines(finalLines)).LineCollection;

            return finalStops;
        }

        public List<Stop> GetNearStops()
        {
            StopCollection = DistinctByLines(Stop.FromJson(StopsProvider.NearStops()));

            return StopCollection;
        }
    }
}
