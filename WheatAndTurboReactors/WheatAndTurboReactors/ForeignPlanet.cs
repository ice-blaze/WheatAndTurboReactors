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
            wheatPrice += differenceFromNorm * 0.01;
            differenceFromNorm = diamondNorm - diamondPrice;
            diamondPrice += differenceFromNorm;
            differenceFromNorm = turboReactorNorm - turboReactorPrice;
            turboReactorPrice += differenceFromNorm;
        }


    }
}
