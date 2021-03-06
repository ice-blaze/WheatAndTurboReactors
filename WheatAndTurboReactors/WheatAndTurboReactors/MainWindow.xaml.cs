﻿using System;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WheatAndTurboReactors
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        List<Button> shipButtons = new List<Button>();
        List<Ship> ships = new List<Ship>();
        Minimap minimap;
        bool shipTripSelectionMod = false;
        Canvas canvas;
        Rectangle rectangle;
        DispatcherTimer gridTimer;
        DispatcherTimer animationTimer;
        DispatcherTimer repeaterTimer;
        DispatcherTimer turboRectorSwapName;
        double rotationAngle;
        const int size = 20;
        GameLogic gameLogic;

        public MainWindow()
        {
            InitializeComponent();

            gridTimer = new DispatcherTimer();
            gridTimer.Tick += new EventHandler(drawMinimapGrid);
            gridTimer.Interval = new TimeSpan(0, 0, 0);
            gridTimer.Start();

            turboRectorSwapName = new DispatcherTimer();
            turboRectorSwapName.Tick += new EventHandler(turboSwap);
            turboRectorSwapName.Interval = new TimeSpan(0,0,0, 0,900);
            turboRectorSwapName.Start();

            rectangle = new Rectangle();

            Brush brush = new SolidColorBrush(Colors.LawnGreen);
            rectangle.Fill = brush;
            rectangle.Width = size;
            rectangle.Height = size;
            rotationAngle = 0;
            animationTimer = new DispatcherTimer();
            animationTimer.Tick += new EventHandler(rotateRectangle);
            animationTimer.Interval = new TimeSpan(0, 0, 0, 0, 30);
            animationTimer.Start();

            repeaterTimer = new DispatcherTimer();
            repeaterTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);

            //Canvas introCanvas = this.FindName("introCanvas") as Canvas;
            IntroCanvas.Margin = new Thickness(0, 0, IntroCanvas.Margin.Right, IntroCanvas.Margin.Bottom);
            

        }

        private void turboSwap(object sender, EventArgs e)
        {
            Button btnTurboSell = this.FindName("sellTurboReactorsBtn") as Button;
            Button btnTurboBuy = this.FindName("buyTurboReactorBtn") as Button;

            if(btnTurboBuy.Content == "reactors")
            {
                btnTurboSell.Content = btnTurboBuy.Content = "turbo";
            }
            else
            {
                btnTurboSell.Content = btnTurboBuy.Content = "reactors";
            }
        }

        private void rotateRectangle(object sender, EventArgs e)
        {
            rotationAngle += 3;
            drawShipLocation();
        }

        private void drawShipLocation()
        {

            Ship ship = Ship.LastShipSelected;
            if(ship != null)
            {

                canvas.Children.Remove(rectangle);

                double xi = -(ship.PlanetShip.x - ship.planetLeft.x);
                double yi = -(ship.PlanetShip.y - ship.planetLeft.y);
                double x = -(ship.PlanetShip.x - ship.planetLeft.x);
                double y = -(ship.PlanetShip.y - ship.planetLeft.y);

                double rapport = (double)ship.ArrivalTime / (double)ship.ArrivalTimeInitiale;

                x *= rapport;
                y *= rapport;


                Canvas.SetLeft(rectangle, ship.planetLeft.x + x - xi);
                Canvas.SetTop(rectangle, ship.planetLeft.y + y - yi);

                RotateTransform rotateTransform1 = new RotateTransform(rotationAngle, size/2, size/2);
                rectangle.RenderTransform = rotateTransform1;

                canvas.Children.Add(rectangle);
            }
            

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
            this.canvas = sender as Canvas;
            Canvas canvas = sender as Canvas;
            minimap.initMinimap(canvas, this);
            gameLogic = new GameLogic(this, minimap);
            minimap.setGameLogic(gameLogic);


            Ship ship = new Ship("small", "Trotter");
            ships.Add(ship);

            Button newBtn = new Button();
            newBtn.Content = "Trotter";
            newBtn.Click += shipShow;
            Style style = Application.Current.FindResource("SwagButton") as Style;
            newBtn.Style = style;
            newBtn.Margin = new Thickness(5);
            newBtn.Padding = new Thickness(7);
            shipButtons.Add(newBtn);

            WrapPanel wrap = (WrapPanel)this.FindName("ShipsPanel");
            wrap.Children.Add(newBtn);

        }

        public void shipShow(object sender, RoutedEventArgs e)
        {
            string shipName = ((Button)sender).Content.ToString();

            Ship ship = null;
            for (int i = 0; i < ships.Count;i++ )
            {
                if(ships[i].Name == shipName)
                {
                    ship = ships[i];
                }
            }
            Ship.LastShipSelected = ship;
            shipShow();
        }

        public void shipShow()
        {
            Ship ship = Ship.LastShipSelected;
            if (ship == null) { return; }
            Label lblShipName = (Label)this.FindName("shipShowName");
            Label lblShipCargo = (Label)this.FindName("shipShowCargo");
            Label lblShipPlanet = (Label)this.FindName("shipShowPlanet");
            Label lblShipTripStatus = (Label)this.FindName("shipShowTripStatus");

            lblShipName.Content = ship.Name;
            lblShipCargo.Content = ship.Container.ToString() + "/" + ship.ContainerMax.ToString();
            lblShipPlanet.Content = ship.PlanetShip.Name;

            //lblShipTripStatus.Content = (ship.IsTravlin) ? "Yes" : "No";
            String[] time = TimeSpan.FromMilliseconds(ship.ArrivalTime).ToString().Split(':');
            lblShipTripStatus.Content = time[0]+" Min "+time[1]+" Sec";
            if (ship.ArrivalTime==0)
            {
                lblShipTripStatus.Content = "Ship is docked";
            }

            drawShipLocation();
        }

        private void Buy_Ships_Click(object sender, RoutedEventArgs e)
        {
            Canvas buyingLayout = (Canvas)this.FindName("BuyShipLayout");
            buyingLayout.Margin = new Thickness(0, 0, buyingLayout.Margin.Right, buyingLayout.Margin.Bottom);

            TextBox tbxShipName = (TextBox)this.FindName("BuyShipTextBox");
            tbxShipName.Text = "Insert a cool Name";
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

        public void hideBuyShipsPanel()
        {
            Canvas buyingLayout = (Canvas)this.FindName("BuyShipLayout");
            buyingLayout.Margin = new Thickness(-2000, -2000, buyingLayout.Margin.Right, buyingLayout.Margin.Bottom);
        }

        private Boolean createShip(string size)
        {
            String nameInput = ((TextBox)this.FindName("BuyShipTextBox")).Text;
            if(nameInput.Contains(" "))
            {
                MessageBox.Show("Choose a better name... (no spaces)", "Naming error", MessageBoxButton.OK, MessageBoxImage.Warning);
                ((TextBox)this.FindName("BuyShipTextBox")).Focus();
                return false;
            }

            int count = 1;
            nameInput = ToUppercaseFirst(nameInput);
            foreach(Ship shipIt in ships)
            {
                String nameClean = shipIt.Name.Split(' ')[0];
                if(nameClean.Equals(nameInput)) { count++; }
            }
            nameInput =  nameInput + " Mark " + ToRoman(count);

            if(Ship.checkPrice(size))
            {

                Ship ship = new Ship(size, nameInput);
                ships.Add(ship);


                Button newBtn = new Button();
                newBtn.Content = nameInput;
                newBtn.Click += shipShow;
                Style style = Application.Current.FindResource("SwagButton") as Style;
                newBtn.Style = style;
                newBtn.Margin = new Thickness(5);
                newBtn.Padding = new Thickness(7);
                shipButtons.Add(newBtn);

                WrapPanel wrap = (WrapPanel)this.FindName("ShipsPanel");
                wrap.Children.Add(newBtn);

                return true;
            }

            return false;
            
        }

        private void Start_Trip_Click(object sender, RoutedEventArgs e)
        {
            if (Ship.LastShipSelected == null) { MessageBox.Show("No ship selected"); return; }
            if (Ship.LastShipSelected.IsTravlin) { MessageBox.Show("The ship is travaling right now"); return; }

            gridTimer.Stop();
            Canvas grid = (Canvas)this.FindName("minimapGrid");
            grid.Children.Clear();

            Canvas minimap = (Canvas)this.FindName("minimapCanvas");
            double mainWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double mainHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double diffX = 2 - minimap.Width / mainWidth;// 1 + diff width pourcent
            double diffY = 2 - minimap.Height / mainHeight;
            st.ScaleX = diffX;
            st.ScaleY = diffY;
            startShipTripSelectionMod();
        }

        public void stopShipTripSelectionMod()
        {
            shipTripSelectionMod = false;
            gridTimer.Start();
            drawMinimapGrid(null, null);
        }

        public void startShipTripSelectionMod()
        {
            shipTripSelectionMod = true;
        }

        public bool ShipTripSelectionMod
        {
            get { return shipTripSelectionMod; }
            private
            set { shipTripSelectionMod = value; }
        }

        static string ToUppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s)) { return string.Empty; }
            return char.ToUpper(s[0]) + s.Substring(1).ToLower();
        }

        public static string ToRoman(int number)
        {
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900); //EDIT: i've typed 400 instead 900
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new ArgumentOutOfRangeException("something bad happened");
        }

        private void boughtProduction(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int cost = (int)minimap.getMotherPlanet().WheatGain * 10 + 1;
            button.Content = "improve wheat production for " + cost + "$";
            minimap.getMotherPlanet().addWheatGain();
            gameLogic.updateLabels();
        }


        private void btnBuyDiamond(object sender, RoutedEventArgs e)
        {
            if (Ship.LastShipSelected == null)
            {
                messageBoxNoShip();
                return;
            }
            Ship.LastShipSelected.PlanetShip.buyDiamond();
            shipShow();
            gameLogic.updateLabels();
        }

        private void btnBuyWheat(object sender, RoutedEventArgs e)
        {
            if (Ship.LastShipSelected == null)
            {
                messageBoxNoShip();
                return;
            }
            Ship.LastShipSelected.PlanetShip.buyWheat();
            shipShow();
            gameLogic.updateLabels();
        }

        private void btnBuyTurboReactors(object sender, RoutedEventArgs e)
        {
            if (Ship.LastShipSelected == null )
            {
                messageBoxNoShip();
                return;
            }
            Ship.LastShipSelected.PlanetShip.buyTurboReactors();
            shipShow();
            gameLogic.updateLabels();
        }

        private void btnSellDiamond(object sender, RoutedEventArgs e)
        {
            if (Ship.LastShipSelected == null)
            {
                messageBoxNoShip();
                return;
            }
            Ship.LastShipSelected.PlanetShip.sellDiamond();
            shipShow();
            gameLogic.updateLabels();
        }

        private void btnSellWheat(object sender, RoutedEventArgs e)
        {
            if (Ship.LastShipSelected == null)
            {
                messageBoxNoShip();
                return;
            }
            Ship.LastShipSelected.PlanetShip.sellWheat();
            shipShow();
            gameLogic.updateLabels();
        }

        private void btnSellTurboReactors(object sender, RoutedEventArgs e)
        {
            if (Ship.LastShipSelected == null)
            {
                messageBoxNoShip();
                return;
            }
            Ship.LastShipSelected.PlanetShip.sellTurboReactors();
            shipShow();
            gameLogic.updateLabels();
        }

        private void messageBoxNoShip()
        {
            MessageBox.Show("There is no ship selected.");
        }

        private void drawMinimapGrid(object sender, EventArgs e)
        {
            Canvas canvas = (Canvas)FindName("minimapGrid");
            canvas.Children.Clear();
            // draw a grid
            int mainWidth = (int)canvas.Width;
            int mainHeight = (int)canvas.Height;

            DoubleAnimation myDoubleAnimation = new DoubleAnimation();
            myDoubleAnimation.From = 0.0;
            myDoubleAnimation.To = 1.0;
            myDoubleAnimation.AutoReverse = true;
            
            for (double i = 0, j = 0; i < mainWidth; i += mainWidth / 10, j += 0.3)
            {
                Line greenLine = new Line();
                greenLine.Stroke = System.Windows.Media.Brushes.Green;
                greenLine.Y1 = 0;
                greenLine.Y2 = mainHeight;
                greenLine.X1 = greenLine.X2 = i;
                greenLine.StrokeThickness = 2;
                greenLine.Opacity = 0;

                myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1.5));
                myDoubleAnimation.BeginTime = TimeSpan.FromSeconds(j);
                greenLine.BeginAnimation(Line.OpacityProperty, myDoubleAnimation);
                canvas.Children.Add(greenLine);
            }

            for (double i = 0, j = 0; i < mainHeight; i += mainHeight / 8, j += 0.3)
            {
                Line greenLine = new Line();
                greenLine.Stroke = System.Windows.Media.Brushes.Green;
                greenLine.X1 = 0;
                greenLine.X2 = mainWidth;
                greenLine.Y1 = greenLine.Y2 = i;
                greenLine.StrokeThickness = 2;
                greenLine.Opacity = 0;

                myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1.5));
                myDoubleAnimation.BeginTime = TimeSpan.FromSeconds(j);
                greenLine.BeginAnimation(Line.OpacityProperty, myDoubleAnimation);
                canvas.Children.Add(greenLine);
            }

            gridTimer.Interval = new TimeSpan(0, 0, new Random().Next(6,20));
        }


        private void btnBuyW(object sender, EventArgs e)
        {
            btnBuyWheat(null, null);
        }

        private void buyWheatBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            resetTimer();
            repeaterTimer.Tick += new EventHandler(btnBuyW);
            repeaterTimer.Start();
        }

        private void buyWheatBtn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            repeaterTimer.Stop();
        }

        private void buyDiamondBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            resetTimer();
            repeaterTimer.Tick += new EventHandler(btnBuyDiamond);
            repeaterTimer.Start();
        }

        private void btnBuyDiamond(object sender, EventArgs e)
        {
            btnBuyDiamond(null, null);
        }

        private void buyDiamondBtn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            repeaterTimer.Stop();
        }

        private void buyTurboReactorBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            resetTimer();
            repeaterTimer.Tick += new EventHandler(btnBuyTurboReactors);
            repeaterTimer.Start();
        }

        private void btnBuyTurboReactors(object sender, EventArgs e)
        {
            btnBuyTurboReactors(null, null);
        }

        private void buyTurboReactorBtn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            repeaterTimer.Stop();
        }

        private void sellDiamondBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            resetTimer();
            repeaterTimer.Tick += new EventHandler(btnSellDiamond);
            repeaterTimer.Start();
        }

        private void btnSellDiamond(object sender, EventArgs e)
        {
            btnSellDiamond(null, null);
        }

        private void sellDiamondBtn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            repeaterTimer.Stop();
        }

        private void sellWheatBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            resetTimer();
            repeaterTimer.Tick += new EventHandler(btnSellWheat);
            repeaterTimer.Start();
        }

        private void btnSellWheat(object sender, EventArgs e)
        {
            btnSellWheat(null, null);
        }

        private void sellWheatBtn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            repeaterTimer.Stop();
        }

        private void sellTurboReactorsBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            resetTimer();
            repeaterTimer.Tick += new EventHandler(btnSellTurboReactors);
            repeaterTimer.Start();
        }

        private void btnSellTurboReactors(object sender, EventArgs e)
        {
            btnSellTurboReactors(null, null);
        }

        private void sellTurboReactorsBtn_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            repeaterTimer.Stop();
        }

        private void Window_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            repeaterTimer.Stop();
        }

        private void Window_LostFocus(object sender, RoutedEventArgs e)
        {
            repeaterTimer.Stop();
        }

        private void Window_LostMouseCapture(object sender, MouseEventArgs e)
        {
            repeaterTimer.Stop();
        }

        private void resetTimer()
        {
            repeaterTimer.Tick -= btnBuyDiamond;
            repeaterTimer.Tick -= btnBuyTurboReactors;
            repeaterTimer.Tick -= btnBuyW;
            repeaterTimer.Tick -= btnSellDiamond;
            repeaterTimer.Tick -= btnSellTurboReactors;
            repeaterTimer.Tick -= btnSellWheat;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IntroCanvas.Margin = new Thickness(3000,3000,IntroCanvas.Margin.Right,IntroCanvas.Margin.Bottom);
        }



    }
}
