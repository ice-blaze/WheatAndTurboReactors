using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private int countner;
        private int countnerMaxSize;
        private int level;
        private string name;

        // bitmap (imageloader?)
        public Ship(int _level,string _name="NaN name")
        {
            level = _level;
            name = _name;

            // adding the correct countner size in function with the level
            switch(level)
            {
                case SMALL_LEVEL:
                    countnerMaxSize = SMALL_SHIP_SIZE;
                    break;
                case MEDIUM_LEVEL:
                    countnerMaxSize = MEDIUM_SHIP_SIZE;
                    break;
                case BIG_LEVEL:
                    countnerMaxSize = BIG_SHIP_SIZE;
                    break;
            }
        }

        public int Countner
        {
            get
            {
                return countner;
            }
            set
            {
                int somme = countner+value;
                if ( somme <= 0 && somme <= countnerMaxSize )
                {
                    countner = somme;
                }
            }
        }
    }
}
