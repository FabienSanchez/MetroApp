using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroApp
{
    public class Line
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("longName")]
        public string LongName { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("textColor")]
        public string TextColor { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        public static List<Line> FromJson(string json) => JsonConvert.DeserializeObject<List<Line>>(json, Converter.Settings);

        public override string ToString()
        {
            return $@"
        -{ShortName}::{Id}---------------------
            Name : {LongName}
            color : {Color}
            textColor : {TextColor}
            Mode : {Mode}
            Type : {Type}
";
        }
    }
}
