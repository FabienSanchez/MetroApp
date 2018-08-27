using System;
using System.Runtime.InteropServices;

namespace MetroAppRequest
{
    class LinesUri
    {
        private UriBuilder UriBuilder = new UriBuilder(MetroRequest.BaseUrl);

        private readonly string Path = "api/routers/default/index/routes";

        private string Query => $"codes={Codes}";

        public Uri Uri => UriBuilder.Uri;

        private string Codes { get; set; }

        public void Params([Optional] string[] codes)
        {
            string strCodes = string.Join(",", codes);
            Codes = strCodes ?? Codes;

            BuildUri();
        }

        private void BuildUri()
        {
            UriBuilder.Path = Path;
            UriBuilder.Query = Query;
        }

        public LinesUri()
        {
            BuildUri();
        }
    }
}