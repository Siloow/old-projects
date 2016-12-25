using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KnowYourMove
{
    public class ColorGenerator
    {
        Random rnd = new Random(DateTime.Now.Millisecond);
        public ColorGenerator()
        {
        }
        public Color RandomColor() // Random color generator ( not in use )
        {
            byte red = (byte)rnd.Next(0, 255);
            byte green = (byte)rnd.Next(0, 255);
            byte blue = (byte)rnd.Next(0, 255);

            return Color.FromRgb(red, green, blue);
        }
        public Color RandomColorOffSpeed(int postal) // Used to generate different colors for the heatmap based of the speeds available
        {
            int Postal = postal;
            using (SpeedApplicationDBEntities context = new SpeedApplicationDBEntities())
            {

                var speed = context.SpeedData.Where(f => f.postcode == Postal).Select(c => c.snelheid).Distinct().SingleOrDefault();

                int speedResult = Convert.ToInt32(speed);

                if (speedResult <= 5000)
                {
                    return Colors.Red;
                }
                else if (speedResult >= 5000 && speedResult < 9999)
                {
                    return Colors.OrangeRed;
                }
                else if (speedResult >= 10000 && speedResult < 29999)
                {
                    return Colors.SandyBrown;
                }
                else if (speedResult >= 30000 && speedResult < 39999)
                {
                    return Colors.DarkOrange;
                }
                else if (speedResult >= 40000 && speedResult < 49999)
                {
                    return Colors.Orange;
                }
                else if (speedResult >= 50000 && speedResult < 64999)
                {
                    return Colors.Gold;
                }
                else if (speedResult >= 65000 && speedResult < 74999)
                {
                    return Colors.Yellow;
                }
                else if (speedResult >= 75000 && speedResult < 89999)
                {
                    return Colors.GreenYellow;
                }
                else if (speedResult >= 90000 && speedResult < 100001)
                {
                    return Colors.Green;
                }
                else
                    return Colors.WhiteSmoke;
            }
                     
        }
    }
}
