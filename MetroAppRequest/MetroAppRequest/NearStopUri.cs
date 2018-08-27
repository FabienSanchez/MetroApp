using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MetroAppRequest
{
    class NearStopUri
    {
        private UriBuilder UriBuilder = new UriBuilder(MetroRequest.BaseUrl);

        private readonly string Path = "api/linesNear/json";

        private string Query => $"x={Lng}&y={Lat}&dist={Dist}&details={Details}";

        public Uri Uri => UriBuilder.Uri;

        private string Lat { get; set; } = "45.18547757558403";
        private string Lng { get; set; } = "5.727771520614624";
        private int Dist { get; set; } = 1000;
        private bool Details { get; set; } = false;

        public void Params([Optional] string lat, [Optional] string lng, [Optional] int? dist, [Optional]  bool? details)
        {
            Lat = lat ?? Lat;
            Lng = lng ?? Lng;
            Dist = dist ?? Dist;
            Details = details ?? Details;

            BuildUri();
        }

        private void BuildUri()
        {
            UriBuilder.Path = Path;
            UriBuilder.Query = Query;
        }

        public NearStopUri()
        {
            BuildUri();
        }
    }
}
