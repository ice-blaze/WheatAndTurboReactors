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
        //TODO wheat production, ship list
        // norms
        // background text
        static double money;
        int wheatNorm, diamondNorm, turboReactorNorm;
        public double wheatGain;
        public Ellipse planetImage;
        public const double wheatGainIncrement = 0.2;
        

        public MotherPlanet(string _name, int _x, int _y, int _wheat, int _diamond, int _turboReactors) : base(_name, _x, _y, _wheat, _diamond, _turboReactors)
        {
            
            money = 0;
            wheatGain = 0;
            // test
            money = 100000;
            wheat = diamond = turboReactors = 10000;
            //end test
            brush = new SolidColorBrush(Colors.Red);
            base.planetImage.Fill = brush;
            base.discovered = true;
            this.wheatGain = 0.2;
        }

        public static double Money
        {
            get { return money; }
            set { money = value; }
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
                //Console.WriteLine("pass");
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
