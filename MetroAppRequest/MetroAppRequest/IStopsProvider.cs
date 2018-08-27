namespace MetroAppRequest
{
    public interface IStopsProvider
    {
        string NearStops();
        string Lines(string[] codes);
    }
}
