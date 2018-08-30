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

        MapLayer pinLayer = new MapLayer();

        MapLayer lineLayer = new MapLayer()
        {
        };

        public MapHome()
        {
            InitializeComponent();
            MetroMap.Center = NearStopsUri.CenterLocation;
            DataContext = NearStopsUri;
            MetroMap.Children.Add(lineLayer);
            MetroMap.Children.Add(pinLayer);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pinLayer.Children.Clear();
            lineLayer.Children.Clear();

            MetroMap.Center = NearStopsUri.CenterLocation;
            DrawCircle(NearStopsUri.CenterLocation, NearStopsUri.Dist);

            List<Stop> StopsResults = Stops.GetNearStops();

            if (StopsResults.Count > 0)
            {

                foreach (Stop stop in StopsResults)
                {
                    Location pinLocation = new Location(stop.Lat, stop.Lon);

                    Pushpin pin = new Pushpin
                    {
                        Location = pinLocation,
                    };

                    ToolTipService.SetToolTip(pin, stop.Name);

                    pinLayer.Children.Add(pin);
                }


                foreach (MetroApp.Line line in Stops.LineCollection)
                {
                    DrawLine(line);
                }
            }
        }

        Color RgbFromString(string str)
        {
            var strColor = str.Split(',').Select(item => byte.Parse(item)).ToArray();
            return Color.FromRgb(strColor[0], strColor[1], strColor[2]);
        }

        private void DrawLine(MetroApp.Line line)
        {
            var coords = line.Geometry.Coordinates[0];
            Color lineColor = RgbFromString(line.Properties.Couleur);
            Color textColor = RgbFromString(line.Properties.CouleurTexte);

            MapPolyline polyline = new MapPolyline
            {
                Stroke = new SolidColorBrush(lineColor),
                StrokeThickness = 5,
                Opacity = 0.7,
                Locations = LocationsFromCoords(coords),
            };

            polyline.MouseEnter += new MouseEventHandler(lineMouseEnter);
            polyline.MouseLeave += new MouseEventHandler(lineMouseLeave);

            TextBlock label = new TextBlock
            {
                Text = $"{line.Properties.Numero} - {line.Properties.Libelle}",
                Foreground = new SolidColorBrush(textColor),
                Background = new SolidColorBrush(lineColor),
                FontSize = 20,
                //Visibility = Collapse;
            };

            lineLayer.Children.Add(label);

            ToolTipService.SetToolTip(polyline, $"{line.Properties.Numero} - {line.Properties.Libelle}");

            lineLayer.Children.Add(polyline);
        }

        private void lineMouseLeave(object sender, MouseEventArgs e)
        {

            //MapLayer.SetPosition(label, new Location(coords[0][1], coords[0][0]));
        }

        private void lineMouseEnter(object sender, MouseEventArgs e)
        {

        }

        private LocationCollection LocationsFromCoords(double[][] coords)
        {
            LocationCollection locations = new LocationCollection();

            for (int i = 0; i < coords.Length; i++)
            {
                var curCoord = coords[i];
                Location loc = new Location(curCoord[1], curCoord[0]);
                locations.Add(loc);
            }

            return locations;
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
            pinLayer.Children.Add(Circle);
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
