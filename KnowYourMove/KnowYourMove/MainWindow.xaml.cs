using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Design;
using System.Windows.Controls.DataVisualization.Charting;

namespace KnowYourMove
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Create instances of everything that is needed to be showed in the main screen
        /// </summary>
        ConsoleBoxHandler outputter;
        MapPolygon newPolygon = null;
        MapLayer polygonPointLayer = new MapLayer();
        MapLayer labelLayer = new MapLayer();
        MapLayer wifiLayer = new MapLayer();

        public MainWindow()
        {
            InitializeComponent();
            // Focus the map on the center points
            MapWithPolygon.Focus();
            LoadDataIntoChart();

            // Display a console onscreen for debugging purposes
            outputter = new ConsoleBoxHandler(TestBox);
            Console.SetOut(outputter);
            Console.WriteLine("Started");

            // Add the layer of internet speeds to the TextLayer xaml layer
            TextLayer.Children.Add(labelLayer);
            WifiLayer.Children.Add(wifiLayer);
        }
        private void SetupNewPolygon() //  Run this to add a layer the postalcode heatmap
        {
            // Create a disposeable connection to the database to query against it
            using (SpeedApplicationDBEntities context = new SpeedApplicationDBEntities())
            {
                foreach (var row in context.SpeedLocations)
                {
                    newPolygon = new MapPolygon();
                    // Defines the polygon fill details
                    newPolygon.Locations = new LocationCollection();
                    Color a = Color.FromRgb(255, 255, 255);
                    newPolygon.Fill = new SolidColorBrush(a);
                    newPolygon.Stroke = new SolidColorBrush(Colors.Green);
                    newPolygon.StrokeThickness = 3;
                    newPolygon.Opacity = 0.3;
                    MapWithPolygon.Focus();

                    { // Add the four points to create a square that covers a postalcode
                        newPolygon.Locations = new LocationCollection()
                        {
                         new Location((double)row.nelat, (double)row.swlng),
                         new Location((double)row.nelat, (double)row.nelng),
                         new Location((double)row.swlat, (double)row.nelng),
                         new Location((double)row.swlat, (double)row.swlng)
                        };
                        polygonPointLayer.Children.Clear();
                        // Add the points to the map
                        NewPolygonLayer.Children.Add(newPolygon);
                    }
                }
            }
        }

        private void SetupNewData() // Run this to add the speed data of each postal code to the map
        {
            using (SpeedApplicationDBEntities context = new SpeedApplicationDBEntities())
            {
                foreach (var data in context.SpeedData)
                {
                    Label l = new Label();
                    l.Content = data.snelheid;
                    var a = context.SpeedLocations.Where(f => f.postcode == data.postcode).Select(c => c.cnlat).SingleOrDefault();
                    var b = context.SpeedLocations.Where(f => f.postcode == data.postcode).Select(c => c.cnlng).SingleOrDefault();

                    Location location = new Location((double)a, (double)b);
                    labelLayer.AddChild(l, location);
                }
            }
        }

        private void SetupNewWifiSpots()
        {
            using (SpeedApplicationDBEntities context = new SpeedApplicationDBEntities())
            {
                foreach (var data in context.WifiData)
                {
                    Pushpin p = new Pushpin();
                    p.Content = data.aanbieder;

                    Location location = new Location((double)data.lat, (double)data.@long);
                    wifiLayer.AddChild(p, location);
                }
            }
        }
    

        private void GetData() // Run this to query a specific postalcode
        {
            using (SpeedApplicationDBEntities context = new SpeedApplicationDBEntities())
            {
                int num1;
                bool result = int.TryParse(textBox.Text, out num1);
                if (result == true && textBox.Text.Length == 4)
                {
                    // Convert user input to an int to compare it to the database values
                    int userInput = Int32.Parse(textBox.Text);

                    var speed = context.SpeedData.Where(f => f.postcode == userInput).Select(c => c.snelheid).SingleOrDefault();
                    var tech = context.SpeedData.Where(f => f.postcode == userInput).Select(c => c.tech).SingleOrDefault();
                    var central = context.SpeedData.Where(f => f.postcode == userInput).Select(c => c.centrale).SingleOrDefault();
                    textBlock.Text = Convert.ToString(speed);
                    textBlock1.Text = Convert.ToString(tech);
                    textBlock2.Text = Convert.ToString(central);
                }
                else
                {
                    textBox.Text = "Vul een geldige postcode in";
                    textBlock.Text = "n/a";
                    textBlock1.Text = "n/a";
                    textBlock2.Text = "n/a";
                }
            }
        }

        private void LoadDataIntoChart()
        {
            using (SpeedApplicationDBEntities context = new SpeedApplicationDBEntities())
            {
                //var name = context.SpeedData.Select(c => c.centrale).SingleOrDefault();
                //var speed = context.SpeedData.Select(c => c.snelheid).SingleOrDefault();
                var tech = context.SpeedData.Where(c => c.tech == "Vdsl2_Pots").Count();
                var tech1 = context.SpeedData.Where(c => c.tech == "Adsl2_Pots").Count();
                var tech2 = context.SpeedData.Where(c => c.tech == "Vvdsl2_Pots").Count();

                Console.WriteLine(tech);

                ((PieSeries)pieChart.Series[0]).ItemsSource =
                new KeyValuePair<string, int>[]
                {
                    new KeyValuePair<string, int>("Vdsl2_Pots", tech),
                    new KeyValuePair<string, int>("Adsl2_Pots", tech1),
                    new KeyValuePair<string, int>("Vvdsl2_Pots", tech2) };
            }

        } 
        
        private void btnCreatePolygon_Click(object sender, RoutedEventArgs e)
        {
            SetupNewPolygon();

            labelLayer.Children.Clear();
            SetupNewData();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            labelLayer.Children.Clear();
            NewPolygonLayer.Children.Clear();
            SetupNewWifiSpots();

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
