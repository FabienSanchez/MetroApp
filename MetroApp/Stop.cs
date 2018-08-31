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
        public List<string> Lines { get; set; }

        public static IStopsProvider LinesProvider { get; set; } = Stops.StopsProvider;

        public static Stop[] FromJson(string json) => JsonConvert.DeserializeObject<Stop[]>(json, Converter.Settings);

        public void DistinctLines()
        {
            Lines = Lines.Distinct().ToList();
        }

        public override string ToString()
        {
            string tmp = $@"{Name}
Lignes :";

            Lines.ForEach((string line) => tmp += Environment.NewLine + "\t" + line.Split(':')[1]);

            return tmp;
        }
    }
}