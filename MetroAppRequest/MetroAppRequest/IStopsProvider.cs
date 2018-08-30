using System.Collections.Generic;

namespace MetroAppRequest
{
    public interface IStopsProvider
    {
        string NearStops();
        string Lines(List<string> codes);
    }
}
