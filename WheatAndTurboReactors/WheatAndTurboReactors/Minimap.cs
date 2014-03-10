using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace WheatAndTurboReactors
{
    class Minimap
    {
        public List<Planet> planetList;


        public void initMinimap(Canvas canvas)
        {

            //replace this by reading from a file
            //-------------------------------------------------------------------------------------
            Planet lonelyPlanet = new Planet(20, 20, 0, 0, 0, 0, 0, 0);
            Planet farAwayPlanet = new Planet(200, 200, 0, 0, 0, 0, 0, 0);
            Planet thisGuy = new Planet(40, 30, 0, 0, 0, 0, 0, 0);

            planetList = new List<Planet>();

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

        public void checkIfPlanetIsClicked(MouseButtonEventArgs e, Canvas canvas)
        {
            foreach (Planet planet in planetList)
            {
                if (e.GetPosition(canvas).X == planet.x)
                {
                    Console.WriteLine("planet is clicked");
                }
            }
        }
    }
}
