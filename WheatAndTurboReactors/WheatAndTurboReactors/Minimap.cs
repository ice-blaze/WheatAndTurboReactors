using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WheatAndTurboReactors
{
    class Minimap
    {
        public List<Planet> planetList;
        public int planetSize = 20;
        private MainWindow parent;
        private MotherPlanet motherPlanet;

        public void initMinimap(Canvas canvas, MainWindow _parent)
        {
            parent = _parent;

            //replace this by reading from a file
            //-------------------------------------------------------------------------------------
            ForeignPlanet lonelyPlanet = new ForeignPlanet("lonely planet",150, 20, 0, 0, 0, 0, 0, 0);
            ForeignPlanet farAwayPlanet = new ForeignPlanet("dark planet", 200, 200, 0, 0, 0, 0, 0, 0);
            ForeignPlanet thisGuy = new ForeignPlanet("anus", 20, 35, 0, 0, 0, 0, 0, 0);
            ForeignPlanet loPlanet = new ForeignPlanet("pissus", 100, 120, 0, 0, 0, 0, 0, 0);
            ForeignPlanet farPlanet = new ForeignPlanet("couillus", 200, 10, 0, 0, 0, 0, 0, 0);
            ForeignPlanet guy = new ForeignPlanet("jameder", 60, 300, 0, 0, 0, 0, 0, 0);
            ForeignPlanet et = new ForeignPlanet("cokandballs", 100, 200, 0, 0, 0, 0, 0, 0);
            ForeignPlanet lanet = new ForeignPlanet("bitchslap", 150, 310, 0, 0, 0, 0, 0, 0);
            ForeignPlanet uy = new ForeignPlanet("crashed", 40, 100, 0, 0, 0, 0, 0, 0);
            ForeignPlanet laneti = new ForeignPlanet("or not", 100, 350, 0, 0, 0, 0, 0, 0);
            ForeignPlanet faret = new ForeignPlanet("speedimus", 10, 120, 0, 0, 0, 0, 0, 0);
            ForeignPlanet guydqw = new ForeignPlanet("broken", 40, 360, 0, 0, 0, 0, 0, 0);

            motherPlanet = new MotherPlanet("mother planet", 10, 10, 0, 0, 0);

            planetList = new List<Planet>();

            planetList.Add(lonelyPlanet);
            planetList.Add(farAwayPlanet);
            planetList.Add(thisGuy);
            planetList.Add(loPlanet);
            planetList.Add(farPlanet);
            planetList.Add(guy);
            planetList.Add(motherPlanet);
            planetList.Add(et);
            planetList.Add(lanet);
            planetList.Add(uy);
            planetList.Add(laneti);
            planetList.Add(faret);
            planetList.Add(guydqw);


            motherPlanet.addPlanetToLinks(thisGuy);
            motherPlanet.addPlanetToLinks(lonelyPlanet);
            thisGuy.addPlanetToLinks(guy);
            thisGuy.addPlanetToLinks(loPlanet);
            thisGuy.addPlanetToLinks(uy);
            lonelyPlanet.addPlanetToLinks(farAwayPlanet);
            farAwayPlanet.addPlanetToLinks(lanet);
            lonelyPlanet.addPlanetToLinks(farPlanet);
            guy.addPlanetToLinks(guydqw);
            uy.addPlanetToLinks(faret);
            guy.addPlanetToLinks(laneti);
            loPlanet.addPlanetToLinks(et);


            motherPlanet.showDiscovered(canvas);

            //-------------------------------------------------------------------------------------

            
            foreach (Planet planet in planetList)
            {
                Canvas.SetLeft(planet.planetImage, planet.x);
                Canvas.SetTop(planet.planetImage, planet.y);


                if (planet.isDiscovered())
                {
                    canvas.Children.Add(planet.planetImage);
                    planet.drawLinks(canvas);
                }
                
                
                

            }

        }

        public void checkIfPlanetIsClicked(MouseButtonEventArgs e, Canvas canvas)
        {
            foreach (Planet planet in planetList)
            {
                if (e.GetPosition(canvas).X >= planet.x && e.GetPosition(canvas).X <= planet.x + planetSize && e.GetPosition(canvas).Y >= planet.y && e.GetPosition(canvas).Y <= planet.y + planetSize)
                {
                    Console.WriteLine("planet is clicked");
                    setPlanetHighlight(planet);
                    updateInformation(planet);
                    planet.setDiscovered(true);
                    drawDiscoveredPlanets(canvas);
                    
                }
                

            }
        }

        private void updateInformation(Planet planet)
        {
            Label label = (Label)parent.FindName("titleLabel");
            label.Content = planet.getName();
            
        }

        private void setPlanetHighlight(Planet _planet)
        {
            foreach (Planet planet in planetList)
            {
                planet.planetImage.StrokeThickness = 0;
            }


            _planet.planetImage.StrokeThickness = 4;
            _planet.planetImage.Stroke = new SolidColorBrush(Colors.LightBlue);
        }

        public void drawDiscoveredPlanets(Canvas canvas)
        {
            canvas.Children.Clear();
            foreach (Planet planet in planetList)
            {
                if(planet.isDiscovered())
                {
                    canvas.Children.Add(planet.planetImage);
                    planet.drawLinks(canvas);
                    foreach(Planet extremePlanet in planet.getLinkedPlanets())
                    {
                        if(!extremePlanet.isDiscovered())
                        {
                            canvas.Children.Add(extremePlanet.planetImage);
                        }
                    }
                }

            }
        }

        public MotherPlanet getMotherPlanet()
        {
            return motherPlanet;
        }
    }
}
