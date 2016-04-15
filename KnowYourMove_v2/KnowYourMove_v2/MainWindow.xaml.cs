using System;
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

namespace KnowYourMove_v2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ConsoleBoxHandler outputter;
        MapPolygon newPolygon;
        MapLayer polygonPointLayer = new MapLayer();
        public MainWindow()
        {
            InitializeComponent();
            //MapWithPolygon.Focus();
            SetupNewPolygon();

            outputter = new ConsoleBoxHandler(TestBox);
            Console.SetOut(outputter);
            Console.WriteLine("Started");

            using (SpeedApplicationDBEntities context = new SpeedApplicationDBEntities())
            {
                //SpeedData data = context.SpeedData.FirstOrDefault(r => r.centrale);

                foreach (var row in context.SpeedData)
                    Console.WriteLine(row.centrale);

                foreach (var row in context.SpeedData)
                    Console.WriteLine(row.postcode);
            }
        }
        private void SetupNewPolygon()
        {
            newPolygon = new MapPolygon();
            // Defines the polygon fill details
            newPolygon.Locations = new LocationCollection();
            newPolygon.Fill = new SolidColorBrush(Colors.Blue);
            newPolygon.Stroke = new SolidColorBrush(Colors.Green);
            newPolygon.StrokeThickness = 3;
            newPolygon.Opacity = 0.8;
            //Set focus back to the map so that +/- work for zoom in/out
            //MapWithPolygon.Focus();
            newPolygon.Locations = new LocationCollection()
                {
                    new Location(51.9056826, 4.5130952),
                    new Location(51.9056826, 4.576203),
                    new Location(51.8781267, 4.576203),
                    new Location(51.8781267, 4.5130952)};

            MyMap.Children.Add(newPolygon);

            //    MapPolygon polygon = new MapPolygon();
            //    polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue);
            //    polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green);
            //    polygon.StrokeThickness = 5;
            //    polygon.Opacity = 0.7;
            //    polygon.Locations = new LocationCollection()
            //    {
            //        new Location(51.9056826, 4.5130952),
            //        new Location(51.9056826, 4.576203),
            //        new Location(51.8781267, 4.576203),
            //        new Location(51.8781267, 4.5130952)};

            //    MyMap.Children.Add(polygon);
        }

        //private void MapWithPolygon_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    e.Handled = true;
        //    // Creates a location for a single polygon point and adds it to
        //    // the polygon's point location list.
        //    Point mousePosition = e.GetPosition(this);
        //    //Convert the mouse coordinates to a location on the map
        //    Location polygonPointLocation = MapWithPolygon.ViewportPointToLocation(
        //        mousePosition);
        //    newPolygon.Locations.Add(polygonPointLocation);

        //    // A visual representation of a polygon point.
        //    Rectangle r = new Rectangle();
        //    r.Fill = new SolidColorBrush(Colors.Red);
        //    r.Stroke = new SolidColorBrush(Colors.Yellow);
        //    r.StrokeThickness = 1;
        //    r.Width = 8;
        //    r.Height = 8;

        //    // Adds a small square where the user clicked, to mark the polygon point.
        //    polygonPointLayer.AddChild(r, polygonPointLocation);
        //    //Set focus back to the map so that +/- work for zoom in/out
        //    MapWithPolygon.Focus();

        //}

        //private void btnCreatePolygon_Click(object sender, RoutedEventArgs e)
        //{
        //    //If there are two or more points, add the polygon layer to the map
        //    if (newPolygon.Locations.Count >= 2)
        //    {
        //        // Removes the polygon points layer.
        //        polygonPointLayer.Children.Clear();

        //        if (!polygonPointLayer.Children.Contains(newPolygon))
        //        {
        //            // Adds the filled polygon layer to the map.
        //            NewPolygonLayer.Children.Add(newPolygon);
        //            SetupNewPolygon();
        //        }
        //    }
        //}

        private void btnRemovePolygon_Click(object sender, RoutedEventArgs e)
        {
            if (MyMap.Children.Contains(newPolygon))
            {
                MyMap.Children.Remove(newPolygon);
            }
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
