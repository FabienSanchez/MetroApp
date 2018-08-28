using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MetroAppRequest
{
    public class NearStopUri
    {
        private UriBuilder UriBuilder = new UriBuilder(MetroRequest.BaseUrl);

        private readonly string Path = "api/linesNear/json";

        private string Query => $"x={Lng}&y={Lat}&dist={Dist}&details={Details}";

        //Params
        public string Lat { get; set; } = "45.18547757558403";
        public string Lng { get; set; } = "5.727771520614624";
        public int Dist { get; set; } = 1000;
        public bool Details { get; set; } = false;

        public Uri Uri => BuildUri();

        private Uri BuildUri()
        {
            UriBuilder.Path = Path;
            UriBuilder.Query = Query;
            return UriBuilder.Uri;
        }
    }
}
