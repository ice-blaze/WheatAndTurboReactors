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
        int wheat, diamond, turboReactors;
        int wheatNorm, diamondNorm, turboReactorNorm;
        

        public ForeignPlanet(int _x, int _y, int _wheat, int _diamond, int _turboReactors, int _wheatNorm, int _diamondNorm, int _turboReactorNorm)
            : base(_x, _y, _wheat, _diamond, _turboReactors)
        {
            wheatNorm = _wheatNorm;
            diamondNorm = _diamondNorm;
            turboReactorNorm = _turboReactorNorm;

            base.discovered = false;
        }


    }
}
