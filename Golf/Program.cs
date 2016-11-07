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
        private static readonly int SWING_LIMIT = 1;


        static void Main(string[] args)
        {
            var golfCourse = new GolfCourse(CUP_POS, COURSE_LENGTH, SWING_LIMIT);
            var player = new Player(new GolfCourse(CUP_POS, COURSE_LENGTH, SWING_LIMIT));

            Console.WriteLine("### Hello, Lets play some Golf! ###");
            Console.WriteLine(golfCourse.Description());
            try
            {
                while (player.CanContinue())
                {
                    Console.WriteLine(player.Status());
                    Console.Write("- Choose angle: ");
                    int angle;
                    int.TryParse(Console.ReadLine(), out angle);
                    Console.Write("- Choose velocity: ");
                    int velocity;
                    int.TryParse(Console.ReadLine(), out velocity);

                    player.DoSwing(angle, velocity);

                    Console.WriteLine($"The ball flew {player.lastSwing().distance}");
                }

                Console.WriteLine($"You hit the cup!. Swings = {player.swings.Count}");
                int count = 0;
                foreach (var swing in player.swings)
                {
                    Console.WriteLine($"Swing #{++count} traveled {swing.distance} m and got new position {swing.position}.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"You cannot continue playing because: {e.Message}");
            }
        }
    }
}
