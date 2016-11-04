using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golf
{
    public class PhysicsEngine
    {
        static readonly double GRAVITY = 9.8f;

        private static double getAngleInRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public static int getDistance(double angle, double velocity)
        {
            if (angle == 0 || velocity == 0)
            {
                return 0;
            }
            double answer = Math.Pow(velocity, 2) / GRAVITY * Math.Sin(2 * getAngleInRadians(angle));
            return (int)(answer + 1);
        }


    }
}
