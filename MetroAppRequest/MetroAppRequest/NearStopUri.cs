using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maps.MapControl.WPF;

namespace MetroAppRequest
{
    public class NearStopUri
    {
        private UriBuilder UriBuilder = new UriBuilder(MetroRequest.BaseUrl);

        private readonly string Path = "api/linesNear/json";

        private string Query => $"x={LngStr}&y={LatStr}&dist={Dist}&details={Details}";

        //Params
        public double Lat { get; set; } = 45.18547757558403;
        public string LatStr => Lat.ToString().Replace(',', '.');

        public double Lng { get; set; } = 5.727771520614624;
        public string LngStr => Lng.ToString().Replace(',', '.');

        public int Dist { get; set; } = 1000;
        public bool Details { get; set; } = false;
        public Location CenterLocation => new Location(Lat, Lng);

        public Uri Uri => BuildUri();

        private Uri BuildUri()
        {
            UriBuilder.Path = Path;
            UriBuilder.Query = Query;
            return UriBuilder.Uri;
        }
    }
}
