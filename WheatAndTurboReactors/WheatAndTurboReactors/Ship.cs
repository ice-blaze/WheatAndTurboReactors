﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace WheatAndTurboReactors
{
    class Ship
    {
        public const int SMALL_LEVEL = 0;
        public const int MEDIUM_LEVEL = 1;
        public const int BIG_LEVEL = 2;
        public const int SMALL_SHIP_SIZE = 15;
        public const int MEDIUM_SHIP_SIZE = 50;
        public const int BIG_SHIP_SIZE = 100;

        private int container;
        private int wheat;
        private int diamond;
        private int turboReactors;
        private int containerMax;
        private int level;
        private string name;
        private Planet planetShip;
        private bool isTravlin;
        private DispatcherTimer travlinCountDown;
        private double arrivalTime;
        private double arrivalTimeInitiale;
        static private MotherPlanet motherPlanet;
        static private Ship lastShipSelected = null;

        // bitmap (imageloader?)
        public Ship(int _level, string _name = "NaN name")
        {
            level = _level;
            name = _name;

            wheat = 0;
            turboReactors = 0;
            diamond = 0;

            travlinCountDown = new System.Windows.Threading.DispatcherTimer();
            travlinCountDown.Tick += new EventHandler(countdown_tick);
            arrivalTime = 0;
            arrivalTimeInitiale = 0;

            // adding the correct countner size in function with the level
            switch (level)
            {
                case SMALL_LEVEL:
                    containerMax = SMALL_SHIP_SIZE;
                    break;
                case MEDIUM_LEVEL:
                    containerMax = MEDIUM_SHIP_SIZE;
                    break;
                case BIG_LEVEL:
                    containerMax = BIG_SHIP_SIZE;
                    break;
            }
            planetShip = motherPlanet;
            isTravlin = false;
        }

        public Ship(string _level, string _name = "NaN name")
            : this(convertLevel(_level), _name)
        {

        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Container
        {
            get { return container; }
            set
            {
                int somme = container + value;
                if (somme <= 0 && somme <= containerMax)
                {
                    container = somme;
                }
            }
        }

        public bool IsTravlin
        {
            get { return isTravlin; }
            set 
            {
                isTravlin = value;
                MainWindow mainWin = ((MainWindow)Application.Current.MainWindow);
                mainWin.shipShow();
            }
        }

        public void startTrip(Planet startPlanet, Planet arrivalPlanet)
        {
            isTravlin = true;
            System.Threading.ThreadPool.QueueUserWorkItem(delegate(object obj)
            {
                double distance = ((startPlanet.x - arrivalPlanet.x) * (startPlanet.x - arrivalPlanet.x) + (startPlanet.y - arrivalPlanet.x) * (startPlanet.y - arrivalPlanet.x));
                arrivalTimeInitiale = distance * 10; 
                travlinCountDown.Interval = new TimeSpan(1000);
                arrivalTime = arrivalTimeInitiale;
                travlinCountDown.Start();
                //Thread.Sleep((int)arrivalTimeInitiale);
                
                //isTravlin = false;
                //planetShip.setDiscovered(true);
            });
        }

        private void countdown_tick(object sender, EventArgs e)
        {
            arrivalTime -= 1000;
            if (arrivalTime < 0)
            {
                travlinCountDown.Stop();
                arrivalTime = 0;
                isTravlin = false;
                planetShip.setDiscovered(true);
            }
        }

        public double ArrivalTime
        {
            get { return arrivalTime; }
        }

        public int ContainerMax
        {
            get { return containerMax; }
        }
        
        public Planet PlanetShip
        {
            get { return planetShip; }
            set { planetShip = value; }
        }

        public int Wheat
        {
            get { return wheat; }
            set { wheat = value; }
        }

        public bool addWheat()
        {
            if (container == containerMax) { return false; }
            container++;
            wheat++;
            return true;
        }

        public bool subWheat()
        {
            if (wheat == 0) { return false; }
            container--; 
            wheat--;
            
            return true;
        }

        public int Diamond
        {
            get { return diamond; }
            set { diamond = value; }
        }

        public bool addDiamond()
        {
            if (container == containerMax) { return false; }
            container++;
            diamond++;
            return true;
        }

        public bool subDiamond()
        {
            if (diamond == 0) { return false; }
            diamond--;
            container--;
            return true;
        }

        public int TurboReactors
        {
            get { return turboReactors; }
            set { turboReactors = value; }
        }

        public bool addTurboReactors()
        {
            if (container == containerMax) { return false; }
            container++;
            turboReactors++;
            return true;
        }

        public bool subTurboReactors()
        {
            if (turboReactors == 0) { return false; }
            turboReactors--;
            container--;
            return true;
        }

        private static int convertLevel(string level)
        {
            level = level.ToLower();
            switch (level)
            {
                case "small":
                    return SMALL_LEVEL;

                case "medium":
                    return MEDIUM_LEVEL;

                case "big":
                    return BIG_LEVEL;
            }

            return -1;
        }

        public string show()
        {
            return name + " " + level.ToString();
        }

        static public Ship LastShipSelected
        {
            get { return lastShipSelected; }
            set { lastShipSelected = value; }
        }
        static public MotherPlanet MotherPlanet
        {
            get { return motherPlanet; }
            set { motherPlanet = value; }
        }
    }
}
