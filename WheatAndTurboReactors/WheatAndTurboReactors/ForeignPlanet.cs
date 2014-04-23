using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheatAndTurboReactors
{
    class ForeignPlanet : Planet
    {
        // norms
        // background text
        double wheatPrice, diamondPrice, turboReactorPrice;
        int wheatNorm, diamondNorm, turboReactorNorm;
        double diamondPriceVariation = 0.001;
        double wheatPriceVariation = 0.0004;
        double turboReactorPriceVariation = 0.002;
        

        public ForeignPlanet(string _name, int _x, int _y, int _wheatPrice, int _diamondPrice, int _turboReactorPrice)
            : base(_name, _x, _y, _wheatPrice, _diamondPrice, _turboReactorPrice)
        {
            wheatPrice = _wheatPrice;
            diamondPrice = _diamondPrice;
            turboReactorPrice = _turboReactorPrice;


            wheatNorm = _wheatPrice;
            diamondNorm = _diamondPrice;
            turboReactorNorm = _turboReactorPrice;


            base.discovered = false;
        }

        public override void normalizePrices()
        {
            double differenceFromNorm = wheatNorm - wheatPrice;
            wheatPrice += differenceFromNorm * wheatPriceVariation;
            differenceFromNorm = diamondNorm - diamondPrice;
            diamondPrice += differenceFromNorm * diamondPriceVariation;
            differenceFromNorm = turboReactorNorm - turboReactorPrice;
            turboReactorPrice += differenceFromNorm * turboReactorPriceVariation;
        }

        public override void buyDiamond()
        {
            if(MotherPlanet.Money >= diamondPrice)
            {
                if (!Ship.LastShipSelected.addDiamond()) { return; }
                MotherPlanet.Money -= (int)diamondPrice;
                diamondPrice += diamondPrice * diamondPriceVariation*10;
                updateView();
            }
        }

        public override void buyWheat()
        {
            if(MotherPlanet.Money >= wheatPrice)
            {
                if (!Ship.LastShipSelected.addWheat()) { return; }
                MotherPlanet.Money -= (int)wheatPrice;
                wheatPrice += wheatPrice * wheatPriceVariation*10;
                updateView();
            }
        }

        public override void buyTurboReactors()
        {
            if (MotherPlanet.Money >= turboReactorPrice)
            {
                if (!Ship.LastShipSelected.addTurboReactors()) { return; }
                MotherPlanet.Money -= (int)turboReactorPrice;
                turboReactorPrice += turboReactorPrice * turboReactorPriceVariation*10;
                updateView();
            }
        }

        public override void sellDiamond()
        {
            if (Ship.LastShipSelected.subDiamond() == true)
            {
                MotherPlanet.Money += (int)diamondPrice;
                diamondPrice -= diamondPrice * diamondPriceVariation * 10;
            }
        }

        public override void sellWheat()
        {
            if(Ship.LastShipSelected.subWheat() == true)
            {
                MotherPlanet.Money += (int)wheatPrice;
                wheatPrice -= wheatPrice * wheatPriceVariation * 10;
            }
        }

        public override void sellTurboReactors()
        {
            if (Ship.LastShipSelected.subTurboReactors() == true)
            {
                MotherPlanet.Money += (int)turboReactorPrice;
                turboReactorPrice -= turboReactorPrice * turboReactorPriceVariation * 10;
            }
        }

        public override void updateView()
        {
            wheat = (int)wheatPrice;
            diamond = (int)diamondPrice;
            turboReactors = (int)turboReactorPrice;
        }


    }
}
