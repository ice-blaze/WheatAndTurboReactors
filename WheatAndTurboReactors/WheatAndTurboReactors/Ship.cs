using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WheatAndTurboReactors
{
    class Ship
    {
        public const int SMALL_LEVEL = 0;
        public const int MEDIUM_LEVEL = 1;
        public const int BIG_LEVEL = 2;
        public const int SMALL_SHIP_SIZE = 50;
        public const int MEDIUM_SHIP_SIZE = 100;
        public const int BIG_SHIP_SIZE = 200;

        private int container;
        private int containerMaxSize;
        private int level;
        private string name;

        // bitmap (imageloader?)
        public Ship(int _level, string _name = "NaN name")
        {
            level = _level;
            name = _name;

            // adding the correct countner size in function with the level
            switch (level)
            {
                case SMALL_LEVEL:
                    containerMaxSize = SMALL_SHIP_SIZE;
                    break;
                case MEDIUM_LEVEL:
                    containerMaxSize = MEDIUM_SHIP_SIZE;
                    break;
                case BIG_LEVEL:
                    containerMaxSize = BIG_SHIP_SIZE;
                    break;
            }
        }

        public Ship(string _level, string _name = "NaN name")
            : this(convertLevel(_level), _name)
        {

        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public int Container
        {
            get
            {
                return container;
            }
            set
            {
                int somme = container + value;
                if (somme <= 0 && somme <= containerMaxSize)
                {
                    container = somme;
                }
            }
        }

        public int ContainerMax
        {
            get
            {
                return containerMaxSize;
            }
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
    }
}
