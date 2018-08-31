using MetroAppRequest;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MetroAppTests
{
    class FakeStopsProvider : IStopsProvider
    {
        public string StopsToReturn { get; set; } = "";
        public string LinesToReturn { get; set; } = "";

        public string Lines([Optional] List<string> codes)
        {
            return LinesToReturn;
        }

        public string NearStops()
        {
            return StopsToReturn;
        }
    }
}
