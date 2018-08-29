using MetroApp;
using MetroAppRequest;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MetroMap
{
    /// <summary>
    /// Interaction logic for MapHome.xaml
    /// </summary>
    public partial class MapHome : Page
    {
        Stops Stops = new Stops();

        public static NearStopUri NearStopsUri = MetroRequest.NearStopsUri;

        public MapHome()
        {
            InitializeComponent();
            NearStopsForm.DataContext = NearStopsUri;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MetroMap.Children.Clear();
            DrawCircle(NearStopsUri.CenterLocation, NearStopsUri.Dist);

            List<Stop> StopsResults = Stops.GetNearStops();

            foreach (Stop stop in StopsResults)
            {
                Location pinLocation = new Location(stop.Lat, stop.Lon);

                Pushpin pin = new Pushpin
                {
                    Location = pinLocation
                };

                ToolTipService.SetToolTip(pin, stop.Name);

                MetroMap.Children.Add(pin);
            }
        }

        private void DrawCircle(Location CenterPosition, int Radius)
        {
            Brush FillColor = new SolidColorBrush(Colors.Purple);
            FillColor.Opacity = 0.2;
            var Circle = new MapPolygon
            {
                Fill = FillColor,
                Locations = CalculateCircle(CenterPosition, Radius)
            };
            MetroMap.Children.Add(Circle);
        }

        const double earthRadius = 6371000D;
        const double Circumference = 2D * Math.PI * earthRadius;

        public static LocationCollection CalculateCircle(Location Position, double Radius)
        {
            LocationCollection GeoPositions = new LocationCollection();

            for (int i = 0; i <= 360; i++)
            {
                double Bearing = ToRad(i);
                double CircumferenceLatitudeCorrected = 2D * Math.PI * Math.Cos(ToRad(Position.Latitude)) * earthRadius;
                double lat1 = Circumference / 360D * Position.Latitude;
                double lon1 = CircumferenceLatitudeCorrected / 360D * Position.Longitude;
                double lat2 = lat1 + Math.Sin(Bearing) * Radius;
                double lon2 = lon1 + Math.Cos(Bearing) * Radius;
                Location NewBasicPosition = new Location
                {
                    Latitude = lat2 / (Circumference / 360D),
                    Longitude = lon2 / (CircumferenceLatitudeCorrected / 360D)
                };
                GeoPositions.Add(NewBasicPosition);
            }
            return GeoPositions;
        }

        private static double ToRad(double degrees)
        {
            return degrees * (Math.PI / 180D);
        }


    }
}
