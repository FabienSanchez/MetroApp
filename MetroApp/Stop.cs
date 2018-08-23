using Newtonsoft.Json;

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
		public string[] Lines { get; set; }

		public static Stop[] FromJson(string json) => JsonConvert.DeserializeObject<Stop[]>(json, Converter.Settings);

		public override string ToString()
		{
			string tmp = $"{Name}::{Id}\r\n";
			tmp += $"├Lon : {Lon}\r\n";
			tmp += $"├Lat : {Lat}\r\n";
			tmp += $"└Lines\r\n";
			for (int i = 0; i < Lines.Length; i++)
			{
				if (i == Lines.Length - 1)
				{
					tmp += $"{"",5}└{Lines[i]}\r\n";
				}
				else
				{
					tmp += $"{"",5}├{Lines[i]}\r\n";
				}
			}
			return tmp;
		}
	}
}