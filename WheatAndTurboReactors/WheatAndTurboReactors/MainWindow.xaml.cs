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
        List<Ship> ships = new List<Ship>();
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
            string shipName = ((Button)sender).Content.ToString();

            Ship ship = null;
            for (int i = 0; i < ships.Count;i++ )
            {
                if(i.ToString()+ships[i].Name == shipName)
                {
                    ship = ships[i];
                }
            }

            //faire ce quon a envie de faire pour ce ship
            MessageBox.Show(ship.Name);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Canvas buyingLayout = (Canvas)this.FindName("BuyShipLayout");
            buyingLayout.Margin = new Thickness(0, 0, buyingLayout.Margin.Right, buyingLayout.Margin.Bottom);

            TextBox tbxShipName = (TextBox)this.FindName("BuyShipTextBox");
            tbxShipName.Focus();
            tbxShipName.SelectAll();
        }

        private void Buy_Ships_Cancel_Click(object sender, RoutedEventArgs e)
        {
            hideBuyShipsPanel();
        }

        private void Buy_Ships_Small_Click(object sender, RoutedEventArgs e)
        {
            if(createShip("Small"))
            {
                hideBuyShipsPanel();
            }
        }

        private void Buy_Ships_Medium_Click(object sender, RoutedEventArgs e)
        {
            if(createShip("Medium"))
            {
                hideBuyShipsPanel();
            }
        }

        private void Buy_Ships_Big_Click(object sender, RoutedEventArgs e)
        {
            if(createShip("Big"))
            {
                hideBuyShipsPanel();
            }
        }

        private void hideBuyShipsPanel()
        {
            Canvas buyingLayout = (Canvas)this.FindName("BuyShipLayout");
            buyingLayout.Margin = new Thickness(-2000, -2000, buyingLayout.Margin.Right, buyingLayout.Margin.Bottom);
        }

        private Boolean createShip(string size)
        {
            TextBox tbxName = (TextBox)this.FindName("BuyShipTextBox");
            if(tbxName.Text == "Insert a cool Name")
            {
                MessageBox.Show("Choose a better name...", "Naming error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            Ship ship = new Ship(size, tbxName.Text);
            ships.Add(ship);

            Button newBtn = new Button();
            newBtn.Content = shipButtons.Count.ToString()+tbxName.Text;
            newBtn.Click += functionHA;
            shipButtons.Add(newBtn); 
            WrapPanel wrap = (WrapPanel)this.FindName("ShipsPanel");
            wrap.Children.Add(newBtn);

            return true;
        }
    }
}
