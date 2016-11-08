using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golf
{
    class Program
    {
        private static readonly int MIN_DISTANCE = 200;
        private static readonly int MAX_DISTANCE = 900;

        static void Main(string[] args)
        {
            Random rnd = new Random();
            int cupPosition = rnd.Next(MIN_DISTANCE, MAX_DISTANCE);
            int courseLength = cupPosition * 2;
            int swingLimit = cupPosition / 100;
            PhysicsEngine engine = new PhysicsEngine();
            var golfCourse = new GolfCourse(cupPosition, courseLength, swingLimit, engine);
            var player = new Player(golfCourse);

            Console.WriteLine("### Hello, Lets play some Golf! ###");
            Console.WriteLine(golfCourse.Description());
            try
            {
                while (player.CanContinue())
                {
                    Console.WriteLine(player.Status());
                    Console.Write("- Choose angle: ");
                    int angle = getValidInput(0, 90);
                    Console.Write("- Choose velocity: ");
                    int velocity = getValidInput(0, 140000); // If higher the PhysicsEngine().getDistance starts to exceed int.MaxValue

                    player.DoSwing(angle, velocity);

                    Console.WriteLine($"The ball traveled {player.lastSwing().distance} meters!");
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
                //Console.WriteLine(e.StackTrace);
            }
        }

        private static int getValidInput(int min, int max)
        {
            Dictionary<string, int> cursor = new Dictionary<string, int> { { "row", Console.CursorTop }, { "col", Console.CursorLeft } };
            int value;
            do
            {
                Console.SetCursorPosition(cursor["col"], cursor["row"]);
                Console.Write("                                ");
                Console.SetCursorPosition(cursor["col"], cursor["row"]);
                int.TryParse(Console.ReadLine(), out value);
            } while (value <= min || value >= max);
            return value;
        }
    }
}
