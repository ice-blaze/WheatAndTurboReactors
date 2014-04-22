using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public int wheatGain;
        public Ellipse planetImage;
        

        public MotherPlanet(string _name, int _x, int _y, int _wheat, int _diamond, int _turboReactors) : base(_name, _x, _y, _wheat, _diamond, _turboReactors)
        {
            
            money = 0;
            wheatGain = 0;
            brush = new SolidColorBrush(Colors.Red);
            base.planetImage.Fill = brush;
            base.discovered = true;
            this.wheatGain = 1;
        }

        public static double Money
        {
            get { return money; }
            set { money = value; }
        }

        public void addWheat(int _wheat)
        {
            wheat += _wheat;
        }

        public int WheatGain
        {
            get { return wheatGain; }
        }

        public void addWheatGain()
        {
            wheatGain += 1;
        }

        public override void buyDiamond()
        {
            if (diamond > 0) 
            {
                diamond--;
                Ship.LastShipSelected.addDiamond(); //TODO messagebox if addDiamond returns false
            }
        }

        public override void buyWheat()
        {
            if (wheat > 0)
            {
                wheat--;
                Ship.LastShipSelected.addWheat(); //TODO messagebox if addDiamond returns false
            }
        }

        public override void buyTurboReactors()
        {
            if (turboReactors > 0)
            {
                turboReactors--;
                Ship.LastShipSelected.addTurboReactors(); //TODO messagebox if addDiamond returns false
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
