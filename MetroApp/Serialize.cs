using Newtonsoft.Json;

namespace MetroApp
{
	public static class Serialize
	{
		public static string ToJson(this Stop[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
	}
}