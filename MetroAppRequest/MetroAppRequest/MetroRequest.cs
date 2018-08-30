using System;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace MetroAppRequest
{
    public class MetroRequest : IStopsProvider
    {
        public static readonly string BaseUrl = "https://data.metromobilite.fr";

        public static NearStopUri NearStopsUri = new NearStopUri();

        public static LinesUri LinesUri = new LinesUri();

        public string NearStops() => Get(NearStopsUri.Uri);

        public string Lines(List<string> codes)
        {
            LinesUri.Params(codes);
            return Get(LinesUri.Uri);
        }

        static string Get(Uri uri)
        {
            WebRequest request = WebRequest.Create(uri);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }
    }
}
