using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheatAndTurboReactors
{
    class PlanetDescriptionsManager
    {
        private List<Planet> planetList;

        public PlanetDescriptionsManager(List<Planet> planetList)
        {
            this.planetList = planetList;

            foreach(Planet planet in planetList)
            {
                switch (planet.Name)
                {
                    case "lonely planet":
                        planet.Description = "For the longest time, the population of this planet thought it was alone in the galaxy, it developped an economy that is able to sustain it self, and therefore has only little interest in commerce with other civilizations.";
                        break;
                    case "mother planet":
                        planet.Description = "Also called \"the great ball of grass\", this planet is mostly covered in forest and fields. It has the lowest intelligent population density in the galaxy, it has two inhabitants, some say it was brought down to one.";
                        break;
                    case "anus":
                        planet.Description = "Mostly a dark hole in space";
                        break;
                    case "crashed":
                        planet.Description = "This is the planet of the \"Renaissance\", the crashing of a huge spaceship that wiped out most of the life on the planet, the survivors of the spaceship destroyed any other life they found in the galaxy. We are the descendants of these survivors";
                        break;
                    case "broken":
                        planet.Description = "this planet is very rich in diamond and bases most of its economy trading them. The diamond trade is so strong that the price of turboreactors for building spaceships is incredibly high.";
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }
            }
        }

    }
}
