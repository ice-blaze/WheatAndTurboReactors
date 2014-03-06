using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Drawing;
using System.Windows.Media;

namespace WpfApplication1
{
    class Planet
    {
        public int x, y;
        int wheat, diamond, turboReactors;
        int wheatNorm, diamondNorm, turboReactorNorm;
        public Ellipse planetImage;
        public Brush brush;

        public Planet(int _x, int _y, int _wheat, int _diamond, int _turboReactors, int _wheatNorm, int _diamondNorm, int _turboReactorNorm)
        {
            x = _x;
            y = _y;
            wheat = _wheat;
            diamond = _diamond;
            turboReactors = _turboReactors;
            wheatNorm = _wheatNorm;
            diamondNorm = _diamondNorm;
            turboReactorNorm = _turboReactorNorm;

            planetImage = new Ellipse();
            planetImage.Height = 5;
            planetImage.Width = 5;

            brush = new SolidColorBrush(Colors.White);
            

            planetImage.Fill = brush;


        }

    }
}
