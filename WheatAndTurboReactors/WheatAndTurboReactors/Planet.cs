using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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
        protected List<Planet> linkedPlanets;
        int size = 20;
        protected bool discovered;

        public Planet(int _x, int _y, int _wheat, int _diamond, int _turboReactors)
        {
            x = _x;
            y = _y;
            wheat = _wheat;
            diamond = _diamond;
            turboReactors = _turboReactors;

            planetImage = new Ellipse();
            planetImage.Height = size;
            planetImage.Width = size;

            linkedPlanets = new List<Planet>();

            brush = new SolidColorBrush(Colors.White);
            planetImage.Fill = brush;

        }

        public void drawLinks(Canvas canvas)
        {
            foreach (Planet planet in linkedPlanets)
            {
                Line line = new Line();
                line.X1 = x + size/2;
                line.Y1 = y + size/2;
                line.X2 = planet.x + size/2;
                line.Y2 = planet.y + size/2;

                line.Stroke = new SolidColorBrush(Colors.White);
                line.StrokeThickness = 2;

                Canvas.SetLeft(line, 0);
                Canvas.SetTop(line, 0);

                canvas.Children.Add(line);
            }
        }

        public virtual bool isDiscovered()
        {
            return discovered;
        }

        public void setDiscovered(bool boolean)
        {
            discovered = boolean;
        }

        public void  showDiscovered(Canvas canvas)
        {
            foreach(Planet planet in linkedPlanets)
            {
                canvas.Children.Add(planet.planetImage);
            }
        }

        public void addPlanetToLinks(Planet planet)
        {
            linkedPlanets.Add(planet);
        }

        public List<Planet> getLinkedPlanets()
        {
            return linkedPlanets;
        }
    }
}
    

