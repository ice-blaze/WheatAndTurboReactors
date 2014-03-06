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
using System.Drawing;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace WpfApplication1
{
    class Minimap
    {
        

        

        public static void initMinimap(Canvas canvas)
        {

            //replace this by reading from a file
            //-------------------------------------------------------------------------------------
            Planet lonelyPlanet = new Planet(20, 20, 0, 0, 0, 0, 0, 0);
            Planet farAwayPlanet = new Planet(200, 200, 0, 0, 0, 0, 0, 0);
            Planet thisGuy = new Planet(40, 30, 0, 0, 0, 0, 0, 0);

            List<Planet> planetList = new List<Planet>();

            planetList.Add(lonelyPlanet);
            planetList.Add(farAwayPlanet);
            planetList.Add(thisGuy);
            //-------------------------------------------------------------------------------------

            foreach (Planet planet in planetList)
            {
                Canvas.SetLeft(planet.planetImage, planet.x);
                Canvas.SetTop(planet.planetImage, planet.y);
                canvas.Children.Add(planet.planetImage);
            }
            
            
        }

    }
}
