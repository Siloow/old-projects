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

namespace KnowYourMove
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Boolean postcodeChanged = false;
        string postcode;
        string postcodeComplete;
        string query = "SELECT * FROM dsldata WHERE postcode LIKE ";
        string connectionString = @"Data Source = ServerName; user id=UserName; password=P@sswd!; Initial Catalog = DatabaseName;";

        public MainWindow()
        {
            InitializeComponent();

            using (SpeedApplicationDBEntities context = new SpeedApplicationDBEntities())
            {

            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            postcodeChanged = true;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //Als de gebruiker de standaardtest in het tekstveld veranderd heeft.
            if (postcodeChanged == true)
            {
                //postcode ophalen uit tekstveld
                postcode = textBox.Text;
                //Controle of postcode aan format voldoet
                if (postcode.Length > 4 || postcode.Length == 0)
                {
                    textBlock1.Text = "U heeft een ongeldige postcode ingevoerd, probeer aub opnieuw.";
                }
                //postcode versturen met query
                else
                {
                    query = query + postcode + ";";

                }
            }

            //Als de gebruiker op de knop drukt zonder de standaardtekst te hebben veranderd.
            if (postcodeChanged == false)
            {
                textBlock1.Text = "U heeft geen postcode ingevoerd, probeer aub opnieuw";
            }
        }


    }
}
