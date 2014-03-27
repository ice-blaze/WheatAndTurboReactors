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
        int wheat, diamond, turboReactors;
        int wheatNorm, diamondNorm, turboReactorNorm;
        int wheatGain;
        public Ellipse planetImage;
        

        public MotherPlanet(int _x, int _y, int _wheat, int _diamond, int _turboReactors) : base(_x, _y, _wheat, _diamond, _turboReactors)
        {
            wheatGain = 0;
            brush = new SolidColorBrush(Colors.Red);
            base.planetImage.Fill = brush;
            base.discovered = true;
        }


    }
}
