using MetroAppRequest;
using System.Runtime.InteropServices;

namespace MetroAppTests
{
    class FakeStopsProvider : IStopsProvider
    {
        public string StopsToReturn { get; set; } = "";
        public string LinesToReturn { get; set; } = "";

        public string Lines([Optional] string[] codes)
        {
            return LinesToReturn;
        }

        public string NearStops()
        {
            return StopsToReturn;
        }
    }
}
