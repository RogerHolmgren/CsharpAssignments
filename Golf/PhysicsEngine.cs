using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golf
{
    public class PhysicsEngine
    {
        public double gravity { get; set; }
        public enum Planet
        {
            Mercury,
            Venus,
            Earth,
            Mars
        };

        public PhysicsEngine()
        {
            setPlanet(Planet.Earth);
        }

        private double getAngleInRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public int getDistance(double angle, double velocity)
        {
            if (angle == 0 || velocity == 0)
            {
                return 0;
            }
            double answer = Math.Pow(velocity, 2) / gravity * Math.Sin(2 * getAngleInRadians(angle));
            return (int)(answer + 0.5);
        }

        /// <summary>
        /// Just for fun I added the option to play golf on any of the inner planets in our solar system.
        /// </summary>
        /// <param name="planet"></param>
        private void setPlanet(Planet planet)
        {
            switch (planet)
            {
                case Planet.Mercury:
                    gravity = 3.7f;
                    break;
                case Planet.Venus:
                    gravity = 8.87f;
                    break;
                case Planet.Mars:
                    gravity = 3.71f;
                    break;
                case Planet.Earth:
                default:
                    gravity = 9.8f; // Default to Earth gravity. Go team Earth!
                    break;
            }
        }


    }
}
