using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WheatAndTurboReactors
{
    class Planet
    {
        public int x, y;
        int wheat, diamond, turboReactors;
        public Ellipse planetImage;
        public Brush brush;

        public Planet(int _x, int _y, int _wheat, int _diamond, int _turboReactors)
        {
            x = _x;
            y = _y;
            wheat = _wheat;
            diamond = _diamond;
            turboReactors = _turboReactors;

            planetImage = new Ellipse();
            planetImage.Height = 5;
            planetImage.Width = 5;

            brush = new SolidColorBrush(Colors.White);
            planetImage.Fill = brush;

        }
    }
}
