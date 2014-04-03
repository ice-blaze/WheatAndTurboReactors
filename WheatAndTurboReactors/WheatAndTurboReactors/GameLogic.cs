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

        public GameLogic(MainWindow _parent, Minimap _minimap)
        {
            parent = _parent;
            minimap = _minimap;

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
            
        }

        private void updateLabels()
        {
            Label moneyLabel = (Label)parent.FindName("moneyLabel");
            Label diamondLabel = (Label)parent.FindName("diamondLabel");
            Label wheatLabel = (Label)parent.FindName("wheatLabel");
            Label turboReactorLabel = (Label)parent.FindName("turboReactorLabel");

            moneyLabel.Content = minimap.getMotherPlanet().getMoney();
        }
    }
}
