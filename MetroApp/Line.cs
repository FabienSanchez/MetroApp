using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroApp
{
    public partial class Lines
    {
        [JsonProperty("features")]
        public List<Line> LineCollection { get; set; }
    }

    public partial class Line
    {
        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("properties")]
        public Properties Properties { get; set; }
    }

    public partial class Geometry
    {
        [JsonProperty("coordinates")]
        public double[][][] Coordinates { get; set; }
    }

    public partial class Properties
    {
        [JsonProperty("NUMERO")]
        public string Numero { get; set; }

        [JsonProperty("CODE")]
        public string Code { get; set; }

        [JsonProperty("COULEUR")]
        public string Couleur { get; set; }

        [JsonProperty("COULEUR_TEXTE")]
        public string CouleurTexte { get; set; }

        [JsonProperty("PMR")]
        public long Pmr { get; set; }

        [JsonProperty("LIBELLE")]
        public string Libelle { get; set; }

        [JsonProperty("ZONES_ARRET")]
        public string[] ZonesArret { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public partial class Lines
    {
        public static Lines FromJson(string json) => JsonConvert.DeserializeObject<Lines>(json, Converter.Settings);
    }

    //    public class Line
    //    {
    //        [JsonProperty("id")]
    //        public string Id { get; set; }

    //        [JsonProperty("shortName")]
    //        public string ShortName { get; set; }

    //        [JsonProperty("longName")]
    //        public string LongName { get; set; }

    //        [JsonProperty("color")]
    //        public string Color { get; set; }

    //        [JsonProperty("textColor")]
    //        public string TextColor { get; set; }

    //        [JsonProperty("mode")]
    //        public string Mode { get; set; }

    //        [JsonProperty("type")]
    //        public string Type { get; set; }


    //        public static List<Line> FromJson(string json) => JsonConvert.DeserializeObject<List<Line>>(json, Converter.Settings);


    //        public override string ToString()
    //        {
    //            return $@"
    //        -{ShortName}::{Id}---------------------
    //            Name : {LongName}
    //            color : {Color}
    //            textColor : {TextColor}
    //            Mode : {Mode}
    //            Type : {Type}
    //";
    //        }
    //    }
}
