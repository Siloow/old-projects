using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Map;
using System.Data;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Map
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Boolean postcodeChanged = false;
        string postcode;
        string postcodeComplete;
        string query = "SELECT * FROM dsldata WHERE postcode LIKE ";
        public MainPage()
        {
            this.InitializeComponent();
        }



        private void textBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {   
            //Standaardtest is veranderd, dus variabele naar "TRUE"
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
                    textBlock.Text = "U heeft een ongeldige postcode ingevoerd, probeer aub opnieuw.";
                }
                //postcode versturen met query
                else
                {
                    query = query + postcode;
                    //Database code.....
                    
                }


            }
            //Als de gebruiker op de knop drukt zonder de standaardtekst te hebben veranderd.
            if (postcodeChanged == false)
            {
                textBlock.Text = "U heeft geen postcode ingevoerd, probeer aub opnieuw";
            }

            //string queryComplete = query + post;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

