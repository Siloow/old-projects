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

namespace KnowYourMove
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ConsoleBoxHandler outputter;
        MapPolygon newPolygon = null;
        MapLayer polygonPointLayer = new MapLayer();
        MapLayer labelLayer = new MapLayer();

        public MainWindow()
        {
            InitializeComponent();
            MapWithPolygon.Focus();
            //SetupNewPolygon();

            outputter = new ConsoleBoxHandler(TestBox);
            Console.SetOut(outputter);
            Console.WriteLine("Started");
            TextLayer.Children.Add(labelLayer);
        }
        private void SetupNewPolygon()
        {
            using (SpeedApplicationDBEntities context = new SpeedApplicationDBEntities())
            {
                foreach (var row in context.SpeedLocations)
                {
                    newPolygon = new MapPolygon();
                    // Defines the polygon fill details
                    newPolygon.Locations = new LocationCollection();
                    newPolygon.Fill = new SolidColorBrush(Colors.BlueViolet);
                    newPolygon.Stroke = new SolidColorBrush(Colors.Green);
                    newPolygon.StrokeThickness = 3;
                    newPolygon.Opacity = 0.3;
                    //Set focus back to the map so that +/- work for zoom in/out
                    MapWithPolygon.Focus();
                    Label label = new Label();

                    List<int> intList = new List<int>();
                    intList.Add(row.postcode);
                    {
                        newPolygon.Locations = new LocationCollection()
                        {
                         new Location((double)row.nelat, (double)row.swlng),
                         new Location((double)row.nelat, (double)row.nelng),
                         new Location((double)row.swlat, (double)row.nelng),
                         new Location((double)row.swlat, (double)row.swlng)
                        };
                        polygonPointLayer.Children.Clear();
                        NewPolygonLayer.Children.Add(newPolygon);
                    }
                }
            }

        }

        private void SetupNewData()
        {
            using (SpeedApplicationDBEntities context = new SpeedApplicationDBEntities())
            {
                foreach (var row in context.SpeedLocations)
                {
                    foreach (var data in context.SpeedData)
                    {
                        Label l = new Label();
                        l.Content = data.snelheid;
                        Location location = new Location((double)row.cnlat, (double)row.cnlng);
                        labelLayer.AddChild(l, location);
                    }
                }

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

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
