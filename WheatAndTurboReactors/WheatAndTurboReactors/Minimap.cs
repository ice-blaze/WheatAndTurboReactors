using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WheatAndTurboReactors
{
    class Minimap
    {
        public List<Planet> planetList;
        public int planetSize = 20;
        private MainWindow parent;
        private MotherPlanet motherPlanet;
        private GameLogic gameLogic;
        public delegate void ShowShip();

        public void initMinimap(Canvas canvas, MainWindow _parent)
        {
            parent = _parent;

            //replace this by reading from a file
            //-------------------------------------------------------------------------------------
            ForeignPlanet lonelyPlanet = new ForeignPlanet("lonely planet",150, 20, 2, 150, 50);
            ForeignPlanet farAwayPlanet = new ForeignPlanet("dark planet", 200, 200, 3, 200, 100);
            ForeignPlanet thisGuy = new ForeignPlanet("anus", 20, 35, 5, 130, 120);
            ForeignPlanet loPlanet = new ForeignPlanet("pissus", 100, 120, 2, 100, 100);
            ForeignPlanet farPlanet = new ForeignPlanet("couillus", 200, 10, 10, 20, 500);
            ForeignPlanet guy = new ForeignPlanet("jameder", 60, 300, 4, 150, 200);
            ForeignPlanet et = new ForeignPlanet("cokandballs", 100, 200, 1, 200, 100);
            ForeignPlanet lanet = new ForeignPlanet("bitchslap", 150, 310, 4, 150, 100);
            ForeignPlanet uy = new ForeignPlanet("crashed", 40, 100, 6, 200, 300);
            ForeignPlanet laneti = new ForeignPlanet("or not", 100, 350, 12, 150, 200);
            ForeignPlanet faret = new ForeignPlanet("speedimus", 10, 120, 9, 200, 500);
            ForeignPlanet guydqw = new ForeignPlanet("broken", 40, 360, 3, 20, 200);

            motherPlanet = new MotherPlanet("mother planet", 10, 10, 10, 0, 0);
            Ship.MotherPlanet = motherPlanet;

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
            thisGuy.addPlanetToLinks(motherPlanet);

            lonelyPlanet.addPlanetToLinks(farAwayPlanet);
            lonelyPlanet.addPlanetToLinks(farPlanet);
            lonelyPlanet.addPlanetToLinks(motherPlanet);

            farAwayPlanet.addPlanetToLinks(lanet);
            farAwayPlanet.addPlanetToLinks(lonelyPlanet);

            farPlanet.addPlanetToLinks(lonelyPlanet);

            lanet.addPlanetToLinks(farAwayPlanet);

            guy.addPlanetToLinks(guydqw);
            guy.addPlanetToLinks(laneti);
            guy.addPlanetToLinks(thisGuy);

            guydqw.addPlanetToLinks(guy);

            laneti.addPlanetToLinks(guy);

            uy.addPlanetToLinks(faret);
            uy.addPlanetToLinks(thisGuy);

            faret.addPlanetToLinks(uy);

            loPlanet.addPlanetToLinks(et);
            loPlanet.addPlanetToLinks(thisGuy);

            et.addPlanetToLinks(loPlanet);

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
                if (e.GetPosition(canvas).X >= planet.x &&
                    e.GetPosition(canvas).X <= planet.x + planetSize &&
                    e.GetPosition(canvas).Y >= planet.y &&
                    e.GetPosition(canvas).Y <= planet.y + planetSize)
                {
                    Console.WriteLine("planet is clicked");
                    setPlanetHighlight(planet);
                    updateInformation(planet);
                    //planet.setDiscovered(true);
                    //drawDiscoveredPlanets(canvas);

                    //set the scale of the minimap to classic view
                    (canvas.RenderTransform as ScaleTransform).ScaleX = 1;
                    (canvas.RenderTransform as ScaleTransform).ScaleY = 1;

                    MainWindow mainWin = ((MainWindow)Application.Current.MainWindow);

                    // if we are in the selection mode (minimap over all the window) then set a planet and 
                    if (Ship.LastShipSelected != null && mainWin.ShipTripSelectionMod)
                    {
                        mainWin.stopShipTripSelectionMod();

                        // check if the planet is a neibourgh
                        if (!Ship.LastShipSelected.PlanetShip.LinkedPlanets.Contains(planet))
                        {
                            MessageBox.Show("Planet isn't linked..");
                            return;
                        }

                        //foreach(Planet planet in Ship.LastShipSelected.PlanetShip)
                        Planet oldPlanet = Ship.LastShipSelected.PlanetShip;
                        Ship.LastShipSelected.PlanetShip = planet;
                        Ship.LastShipSelected.startTrip(oldPlanet, planet);
                        mainWin.shipShow();
                    }
                }
            }
        }

        private void updateInformation(Planet planet)
        {
            Label label = (Label)parent.FindName("titleLabel");
            TextBlock tbxBlock = parent.FindName("foreignDescriptionLabel") as TextBlock;
            label.Content = planet.Name;
            tbxBlock.Text = planet.Description;
            gameLogic.setShownPlanet(planet);
            gameLogic.updateLabels();
            
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
                    foreach(Planet extremePlanet in planet.LinkedPlanets)
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

        public void setGameLogic(GameLogic _gameLogic)
        {
            gameLogic = _gameLogic;
        }
    }
}
