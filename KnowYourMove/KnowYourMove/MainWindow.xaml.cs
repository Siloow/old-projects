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
        ColorGenerator randColor = new ColorGenerator();
        CalcDistance distance = new CalcDistance();

        public MainWindow()
        {
            InitializeComponent();
            // Focus the map on the center points
            MapWithPolygon.Focus();
            LoadDataIntoChart();

            // Display a console onscreen for debugging purposes
            //outputter = new ConsoleBoxHandler(TestBox);
            //Console.SetOut(outputter);
            //Console.WriteLine("Started");

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

                    newPolygon.Fill = new SolidColorBrush(randColor.RandomColorOffSpeed(row.postcode));
                    newPolygon.Stroke = new SolidColorBrush(Colors.Green);
                    newPolygon.StrokeThickness = 3;
                    newPolygon.Opacity = 0.25;
                    MapWithPolygon.Focus();

                    /// <summary>
                    /// We used a script to get the longitude and latitude of 2 corners of a postalcode
                    /// We then switched the x-axis and y-axis of these points so that we had 4 points in total
                    /// These 4 points we then use to create squares on the map each representing a certain postalcode
                    /// </summary>

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
                    l.Content = data.snelheid + " kbs";
                    var a = context.SpeedLocations.Where(f => f.postcode == data.postcode).Select(c => c.cnlat).SingleOrDefault();
                    var b = context.SpeedLocations.Where(f => f.postcode == data.postcode).Select(c => c.cnlng).SingleOrDefault();

                    Location location = new Location((double)a, (double)b);
                    labelLayer.AddChild(l, location);
                }
            }
        }

        private void SetupNewWifiSpots() // Goes through every wifi hotspot in the database and adds it to the map
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

        private void GetDataFromPostal() // Run this to query a specific postalcode
        {
            using (SpeedApplicationDBEntities context = new SpeedApplicationDBEntities())
            {
                int num1;
                bool result = int.TryParse(textBox.Text, out num1);
                if (result == true && textBox.Text.Length == 4)
                {
                    int num2 = 3011; // These numbers represent Rotterdam's postalcodes
                    int num3 = 3089;
                    if (num3 > Int32.Parse(textBox.Text) && num2 < Int32.Parse(textBox.Text))
                    {
                        // Convert user input to an int to compare it to the database values
                        int userInput = Int32.Parse(textBox.Text);

                        // We query the user input and return the results and display them to the screen
                        var speed = context.SpeedData.Where(f => f.postcode == userInput).Select(c => c.snelheid).SingleOrDefault();
                        var tech = context.SpeedData.Where(f => f.postcode == userInput).Select(c => c.tech).SingleOrDefault();
                        var central = context.SpeedData.Where(f => f.postcode == userInput).Select(c => c.centrale).SingleOrDefault();
                        textBlock.Text = Convert.ToString(speed + " kbit");
                        textBlock1.Text = Convert.ToString(tech);
                        textBlock2.Text = Convert.ToString(central);

                        string checkCentral = Convert.ToString(central);
               
                        if (checkCentral.StartsWith("Rt-A")) // Based on the centrale we can say if someone has a high or low ping
                        {
                            textBlock3.Text = "Laag";
                            textBlock4.Text = "Hoog";
                        }
                        else
                        {
                            textBlock3.Text = "Hoog";
                            textBlock4.Text = "Laag";
                        }

                        // Part where we create and add the line distance to the mini map
                        // We query the longitude and latitude of the user input
                        var speedPostalLat = context.SpeedData.Where(f => f.postcode == userInput).Select(c => c.centralelat).SingleOrDefault();
                        var speedPostalLong = context.SpeedData.Where(f => f.postcode == userInput).Select(c => c.centralelong).SingleOrDefault();

                        var locationPostalLat = context.SpeedLocations.Where(f => f.postcode == userInput).Select(c => c.cnlat).SingleOrDefault();
                        var locationPostalLong = context.SpeedLocations.Where(f => f.postcode == userInput).Select(c => c.cnlng).SingleOrDefault();

                        // We create a polyline and add it to the mini map
                        MapPolyline polyline = new MapPolyline();
                        polyline.Stroke = new SolidColorBrush(Colors.Blue);
                        polyline.StrokeThickness = 5;
                        polyline.Opacity = 0.7;
                        polyline.Locations = new LocationCollection() {
                        new Location((double)speedPostalLat, (double)speedPostalLong),
                        new Location((double)locationPostalLat,(double)locationPostalLong) };

                        MiniMap.Children.Add(polyline);

                        // Here we calculate the distance of two points and show it on the screen
                        var distance = GenDistance.getDistance(userInput);

                        // We cut the result off at a certain decimal
                        string distanceString = Convert.ToString(distance);

                        string distanceStringCut = new string(distanceString.Take(5).ToArray());
                        textBlock5.Text = Convert.ToString(distanceStringCut + " km");

                        LoadDataIntoBarChart(userInput);

                    }
                    else // If the userinput is not in range
                    {
                        textBox.Text = "Postcode niet in Rotterdam.";
                        textBlock.Text = "n/a";
                        textBlock1.Text = "n/a";
                        textBlock2.Text = "n/a";
                        textBlock3.Text = "n/a";
                        textBlock4.Text = "n/a";
                    }
                }
                else // If the user input is not correct
                {
                    textBox.Text = "Vul een geldige postcode in";
                    textBlock.Text = "n/a";
                    textBlock1.Text = "n/a";
                    textBlock2.Text = "n/a";
                    textBlock3.Text = "n/a";
                    textBlock4.Text = "n/a";
                }
            }
        }

        private void LoadDataIntoChart() // Used to insert the data into the pie chart
        {
            using (SpeedApplicationDBEntities context = new SpeedApplicationDBEntities())
            {
                // We query the different kind of technologies, count them and then add them to the pie chart
                var tech = context.SpeedData.Where(c => c.tech == "Vdsl2_Pots").Count();
                var tech1 = context.SpeedData.Where(c => c.tech == "Adsl2_Pots").Count();
                var tech2 = context.SpeedData.Where(c => c.tech == "Vvdsl2_Pots").Count();

                ((PieSeries)pieChart.Series[0]).ItemsSource =
                new KeyValuePair<string, int>[]
                {
                    new KeyValuePair<string, int>("Vdsl2_Pots", tech),
                    new KeyValuePair<string, int>("Adsl2_Pots", tech1),
                    new KeyValuePair<string, int>("Vvdsl2_Pots", tech2)
                };
            }
        }
        private void LoadDataIntoBarChart(int userinput) // Uses userinput to insert the data into the bar chart
        {
            int userInput = userinput;

            using (SpeedApplicationDBEntities context = new SpeedApplicationDBEntities())
            {
                // Generate average speed from Rotterdam
                var avgSpeed = context.SpeedData.Select(c => c.snelheid).Average();
                var userSpeed = context.SpeedData.Where(f => f.postcode == userInput).Select(c => c.snelheid).SingleOrDefault();

                ((BarSeries)barChart.Series[0]).ItemsSource =
                new KeyValuePair<string, int>[]
                {
                    new KeyValuePair<string, int>("Gemiddelde", (int)avgSpeed),
                    new KeyValuePair<string, int>("Uw snelheid", userSpeed)
                };
            }
        }
        private void btnCreatePolygon_Click(object sender, RoutedEventArgs e) // If clicked, all the functions below are called
        {
            NewPolygonLayer.Children.Clear();
            labelLayer.Children.Clear();
            wifiLayer.Children.Clear();
            SetupNewPolygon();
            SetupNewData();
        }

        private void button1_Click(object sender, RoutedEventArgs e) // If clicked, all the functions below are called
        {
            labelLayer.Children.Clear();
            NewPolygonLayer.Children.Clear();
            wifiLayer.Children.Clear();
            SetupNewWifiSpots();

        }

        private void button_Click(object sender, RoutedEventArgs e) // If clicked, all the functions below are called
        {
            MiniMap.Children.Clear();
            GetDataFromPostal();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
