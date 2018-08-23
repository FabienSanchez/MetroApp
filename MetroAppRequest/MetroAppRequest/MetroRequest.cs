using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace MetroAppRequest
{
    public class MetroRequest
    {
		private UriBuilder MetroUriBuilder = new UriBuilder("https://data.metromobilite.fr");

		public Uri NearStopsUri
		{
			get
			{
				string lat = "45.18547757558403";
				string lng = "5.727771520614624";
				int dist = 1000;
				bool details = false;

				string Query = $"x={lng}&y={lat}&dist={dist}&details={details}";

				MetroUriBuilder.Path = "api/linesNear/json";
				MetroUriBuilder.Query = Query;

				return MetroUriBuilder.Uri;
			}
		}

		public string NearLines()
		{
			return Get(NearStopsUri);
		}

		string Get(Uri uri)
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
