using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WheatAndTurboReactors
{
    class MotherPlanet: Planet
    {
        static double money;
        int wheatNorm, diamondNorm, turboReactorNorm;
        public double wheatGain;
        public Ellipse planetImage;
        public const double wheatGainIncrement = 0.2;
        

        public MotherPlanet(string _name, int _x, int _y, int _wheat, int _diamond, int _turboReactors) : base(_name, _x, _y, _wheat, _diamond, _turboReactors)
        {
            
            money = 0;
            wheatGain = 0;
            brush = new SolidColorBrush(Colors.Red);
            base.planetImage.Fill = brush;
            base.discovered = true;
            this.wheatGain = 0.2;
        }

        public static double Money
        {
            get { return money; }
            set 
            { 
                money = value; 

                if(money>=2000)
                {
                    MessageBoxResult result = MessageBox.Show("The princess is rescued :)", "You won !!!", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (result == MessageBoxResult.OK)
                    {
                        Application.Current.Shutdown();
                    }
                }
            }
        }

        public void addWheat(double _wheat)
        {
            wheat += _wheat;
        }

        public double WheatGain
        {
            get { return wheatGain; }
        }

        public void addWheatGain()
        { 
            double cost = wheatGain * 10 + 1;
            if(money > cost)
            {
                Console.WriteLine(cost);
                money = (int)money - (int)cost;
                wheatGain += wheatGain * wheatGainIncrement;
            }
        }

        public override void buyDiamond()
        {
            if (diamond > 0) 
            {
                if (!Ship.LastShipSelected.addDiamond()) { return; }
                diamond--;
            }
        }

        public override void buyWheat()
        {
            if (wheat > 0)
            {
                if (!Ship.LastShipSelected.addWheat()) { return; }
                wheat--;
            }
        }

        public override void buyTurboReactors()
        {
            if (turboReactors > 0)
            {
                if(!Ship.LastShipSelected.addTurboReactors()) { return; }
                turboReactors--;
            }
        }

        public override void sellDiamond()
        {
            if (Ship.LastShipSelected.subDiamond() == true) { diamond++; }
        }

        public override void sellWheat()
        {
            if (Ship.LastShipSelected.subWheat() == true) { wheat++; }
        }

        public override void sellTurboReactors()
        {
            if (Ship.LastShipSelected.subTurboReactors() == true) { turboReactors++; }
        }


    }
}
