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

        MapLayer lineLayer = new MapLayer();

        MapLayer lineLabelLayer = new MapLayer();

        MapLayer stopLabelLayer = new MapLayer();

        MapLayer circleLayer = new MapLayer();


        public MapHome()
        {
            InitializeComponent();
            MetroMap.Center = NearStopsUri.CenterLocation;
            DataContext = NearStopsUri;
            MetroMap.Children.Add(circleLayer);
            MetroMap.Children.Add(lineLayer);
            MetroMap.Children.Add(pinLayer);
            MetroMap.Children.Add(lineLabelLayer);
            MetroMap.Children.Add(stopLabelLayer);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pinLayer.Children.Clear();
            lineLayer.Children.Clear();
            lineLabelLayer.Children.Clear();
            stopLabelLayer.Children.Clear();
            circleLayer.Children.Clear();

            MetroMap.Center = NearStopsUri.CenterLocation;
            DrawCircle(NearStopsUri.CenterLocation, NearStopsUri.Dist);

            List<Stop> StopsResults = Stops.GetNearStops();

            if (StopsResults.Count > 0)
            {

                foreach (Stop stop in StopsResults)
                {
                    Location pinLocation = new Location(stop.Lat, stop.Lon);

                    TextBlock label = new TextBlock()
                    {
                        Text = stop.ToString(),
                        Background = new SolidColorBrush(Colors.AliceBlue),
                        FontSize = 12,
                        Padding = new Thickness(5),
                    };

                    Image busIcon = new Image()
                    {
                        Source = new BitmapImage(new Uri("./images/bus_symbol.png", UriKind.Relative))
                    };

                    Pushpin pin = new Pushpin
                    {
                        Location = pinLocation,
                        DataContext = label,
                        Content = busIcon,
                    };

                    pin.MouseEnter += new MouseEventHandler(StopMouseEnter);
                    pin.MouseLeave += new MouseEventHandler(StopMouseLeave);

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
            if (line.Geometry.Coordinates != null)
            {
                var coords = line.Geometry.Coordinates[0];
                Color lineColor = RgbFromString(line.Properties.Couleur);
                Color textColor = RgbFromString(line.Properties.CouleurTexte);

                TextBlock label = new TextBlock
                {
                    Text = $"{line.Properties.Numero} - {line.Properties.Libelle}",
                    Foreground = new SolidColorBrush(textColor),
                    Background = new SolidColorBrush(lineColor),
                    FontSize = 15,
                };

                MapPolyline polyline = new MapPolyline
                {
                    Stroke = new SolidColorBrush(lineColor),
                    StrokeThickness = 5,
                    Opacity = 0.7,
                    Locations = LocationsFromCoords(coords),
                    DataContext = label,
                };

                polyline.MouseEnter += new MouseEventHandler(LineMouseEnter);
                polyline.MouseLeave += new MouseEventHandler(LineMouseLeave);

                lineLayer.Children.Add(polyline);
            }
        }

        private void LineMouseLeave(object sender, MouseEventArgs e)
        {
            MapPolyline line = e.OriginalSource as MapPolyline;
            lineLabelLayer.Children.Remove(line.DataContext as FrameworkElement);
        }

        private void LineMouseEnter(object sender, MouseEventArgs e)
        {
            MapPolyline line = e.OriginalSource as MapPolyline;
            UIElement label = line.DataContext as UIElement;

            Point mousePosition = e.GetPosition((IInputElement)sender);
            mousePosition.X += 12;
            mousePosition.Y += 12;

            Location loc = MetroMap.ViewportPointToLocation(mousePosition);
            lineLabelLayer.AddChild(label, loc);
        }

        private void StopMouseLeave(object sender, MouseEventArgs e)
        {
            Pushpin stop = e.Source as Pushpin;
            stopLabelLayer.Children.Remove(stop.DataContext as FrameworkElement);
        }

        private void StopMouseEnter(object sender, MouseEventArgs e)
        {
            Pushpin stop = e.Source as Pushpin;
            TextBlock label = stop.DataContext as TextBlock;

            Point position = e.GetPosition(MetroMap);
            position.X += 12;
            position.Y -= 50;

            Location loc = MetroMap.ViewportPointToLocation(position);
            stopLabelLayer.AddChild(label, loc);
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
                Locations = CalculateCircle(CenterPosition, Radius),
            };

            var Center = new MapPolygon
            {
                Fill = new SolidColorBrush(Colors.Yellow),
                Locations = CalculateCircle(CenterPosition, 10),
            };

            circleLayer.Children.Add(Circle);
            circleLayer.Children.Add(Center);
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
