using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WheatAndTurboReactors
{
    class GameLogic
    {
        DispatcherTimer timer;
        MainWindow parent;
        Minimap minimap;
        Planet currentlyShownPlanet;

        public GameLogic(MainWindow _parent, Minimap _minimap)
        {
            parent = _parent;
            minimap = _minimap;
            currentlyShownPlanet =  new ForeignPlanet("broken", 40, 360, 0, 0, 0);

            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(timer_tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void timer_tick(object sender, EventArgs e)
        {
            Console.WriteLine("tick");
            growth();
            updateLabels();
        }

        private void growth()
        {
            minimap.getMotherPlanet().addWheat(minimap.getMotherPlanet().WheatGain);
            foreach(Planet planet in minimap.planetList)
            {
                planet.normalizePrices();
            }
            
        }

        public void updateLabels()
        {
            Label moneyLabel = (Label)parent.FindName("moneyLabel");
            Label diamondLabel = (Label)parent.FindName("diamondLabel");
            Label wheatLabel = (Label)parent.FindName("wheatLabel");
            Label turboReactorLabel = (Label)parent.FindName("turboReactorLabel");

            Label foreignWheatLabel = (Label)parent.FindName("foreignWheatLabel");
            Label foreignDiamondLabel = (Label)parent.FindName("foreignDiamondLabel");
            Label foreignTurboReactorLabel = (Label)parent.FindName("foreignTurboReactorLabel");

            moneyLabel.Content = "money: " + MotherPlanet.Money;
            diamondLabel.Content = "diamond: " + minimap.getMotherPlanet().Diamond;
            wheatLabel.Content = "wheat: " + minimap.getMotherPlanet().Wheat;
            turboReactorLabel.Content = "turbo reactor: " + minimap.getMotherPlanet().TurboReactors;

            currentlyShownPlanet.updateView();
            foreignWheatLabel.Content = "wheat: " + currentlyShownPlanet.Wheat;
            foreignDiamondLabel.Content = "diamond: " + currentlyShownPlanet.Diamond;
            foreignTurboReactorLabel.Content = "turbo reactors: " + currentlyShownPlanet.TurboReactors;

            parent.shipShow();

            Canvas canvas = (Canvas)parent.FindName("minimapCanvas");
            minimap.drawDiscoveredPlanets(canvas);
        }

        public void setShownPlanet(Planet planet)
        {
            currentlyShownPlanet = planet;
        }
    }
}
