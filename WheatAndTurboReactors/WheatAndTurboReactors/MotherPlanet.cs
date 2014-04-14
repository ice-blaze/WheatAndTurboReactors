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
        int money;
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

        public int Money
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


        public int getMoney()
        {
            return money;
        }


    }
}
