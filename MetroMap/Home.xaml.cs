using MetroApp;
using MetroAppRequest;
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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        Stops Stops = new Stops();

        public static NearStopUri NearStopsUri = MetroRequest.NearStopsUri;

        public Home()
        {
            InitializeComponent();
            NearStopsForm.DataContext = NearStopsUri;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            {
                Results StopsResults = new Results(Stops.GetNearStops());
                this.NavigationService.Navigate(StopsResults);
            }
        }


    }
}
