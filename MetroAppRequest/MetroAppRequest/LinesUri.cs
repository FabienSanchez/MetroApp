using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MetroAppRequest
{
    public class LinesUri
    {
        private UriBuilder UriBuilder = new UriBuilder(MetroRequest.BaseUrl);

        private readonly string Path = "api/lines/json";

        private string Query => $"types=ligne&codes={DashedCodes}";

        public Uri Uri { get { BuildUri(); return UriBuilder.Uri; } }

        private string Codes { get; set; }
        private string DashedCodes => Codes.Replace(':', '_');

        public void Params(List<string> codes)
        {
            string strCodes = string.Join(",", codes);
            Codes = strCodes ?? Codes;
        }

        private void BuildUri()
        {
            UriBuilder.Path = Path;
            UriBuilder.Query = Query;
        }
    }
}