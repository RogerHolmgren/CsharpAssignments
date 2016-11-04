using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golf
{
    class Program
    {
        private static readonly int CUP_POS = 900;
        private static readonly int COURSE_LENGTH = 1000;
        private static readonly int SWING_LIMIT = 10;


        static void Main(string[] args)
        {
            var golfCourse = new GolfCourse(CUP_POS, COURSE_LENGTH, SWING_LIMIT);
            var player = new Player(golfCourse);

            Console.WriteLine("### Hello, Lets play some Golf! ###");
            Console.WriteLine(golfCourse.ToString());
            while (!player.IsDone())
            {
                Console.WriteLine(player.Info());

                Console.Write("- Choose angle: ");
                int angle;
                int.TryParse(Console.ReadLine(), out angle);
                Console.Write("- Choose velocity: ");
                int velocity;
                int.TryParse(Console.ReadLine(), out velocity);

                player.HitTheBall(angle, velocity);

                Console.WriteLine($"The ball flew {player.lastDistance()}");
            }
            Console.WriteLine($"You hit the cup!. Swings = {player.swings}");
            foreach (var swing in player.swings)
            {
                Console.WriteLine(swing.);
            }
        }
    }
}
