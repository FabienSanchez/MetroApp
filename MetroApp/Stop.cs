using MetroAppRequest;
using MoreLinq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace MetroApp
{
    public class Stop
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lines")]
        public string[] LinesId { get; set; }

        [JsonIgnore]
        public List<Line> Lines { get; set; }

        public static IStopsProvider LinesProvider { get; set; } = Stops.StopsProvider;

        [OnDeserialized]
        public void InitLines(StreamingContext context) => Lines = Line.FromJson(LinesProvider.Lines(LinesId));

        public static Stop[] FromJson(string json) => JsonConvert.DeserializeObject<Stop[]>(json, Converter.Settings);

        public void DistinctLines()
        {
            LinesId = LinesId.Distinct().ToArray();
            Lines = Lines.DistinctBy(line => line.Id).ToList();
        }

        public override string ToString()
        {
            string tmp = $@"{Name}::{Id}
    Lon : {Lon}
    Lat : {Lat}
    Lines :";

            Lines.ForEach((Line line) => tmp += line.ToString());

            return tmp;
        }
    }
}