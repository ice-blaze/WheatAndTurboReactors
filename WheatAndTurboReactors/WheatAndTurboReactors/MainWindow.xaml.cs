using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WheatAndTurboReactors
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Button> shipButtons = new List<Button>();
        Minimap minimap;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MiniMap_Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Canvas canvas = sender as Canvas;
            Console.WriteLine("minimap");
            minimap.checkIfPlanetIsClicked(e, canvas);
        }

        private void Info_Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("info");
        }

        private void Ship_Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("ship");
        }

        private void Resources_Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("resources");
        }

        private void Minimap_Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            minimap = new Minimap();
            Canvas canvas = sender as Canvas;
            minimap.initMinimap(canvas, this);
            GameLogic gamelogic = new GameLogic(this, minimap);
        }

        public void functionHA(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("HA");
        }

        public void functionHA(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("HA");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button newBtn = new Button();
            newBtn.Content = shipButtons.Count.ToString();
            newBtn.Click += functionHA;
            shipButtons.Add(newBtn);
            WrapPanel wrap = (WrapPanel)this.FindName("ShipsPanel");
            wrap.Children.Add(newBtn);
<<<<<<< Updated upstream

<<<<<<< Updated upstream

=======
            Canvas buyingLayout = (Canvas)this.FindName("BuyShipLayout");
            buyingLayout.Margin = new Thickness(0, 0, buyingLayout.Margin.Right, buyingLayout.Margin.Bottom);
        }
>>>>>>> Stashed changes
=======

            Canvas buyingLayout = (Canvas)this.FindName("BuyShipLayout");
            buyingLayout.Margin = new Thickness(0, 0, buyingLayout.Margin.Right, buyingLayout.Margin.Bottom);
        }
>>>>>>> Stashed changes
    }
}
