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
            var golfCourse = GenerateNewGolfCourse();
            golfCourse.PhysicsEngine.SetPlanet(PhysicsEngine.Planet.Earth);
            var player = new Player(golfCourse);
            Console.WriteLine("### Hello, Lets play some Golf! ###");
            Console.WriteLine(golfCourse.Description());
            RunTheGame(player);
            PrintSummary(player);
        }

        /// <summary>
        /// The main game loop.
        /// </summary>
        /// <param name="player"></param>
        private static void RunTheGame(Player player)
        {
            try
            {
                while (player.DistanceToCup() != 0)
                {
                    if (FollowsTheRules(player))
                    {
                        Console.WriteLine(player.Status());
                        Console.Write("- Choose angle: ");
                        int angle = getValidInput(0, 90);
                        Console.Write("- Choose velocity: ");
                        int velocity = getValidInput(0, 140000); // If higher the PhysicsEngine().getDistance starts to exceed int.MaxValue

                        player.DoSwing(angle, velocity);

                        Console.WriteLine($"The ball traveled {player.lastSwing().distance} meters!");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"You cannot continue playing because: {e.Message}");
            }
        }

        /// <summary>
        /// Prints a summary.
        /// </summary>
        /// <param name="player"></param>
        private static void PrintSummary(Player player)
        {
            Console.WriteLine("--- Summary ---");
            if (player.DistanceToCup() == 0)
            {
                Console.WriteLine($"You hit the cup!. Swings = {player.swings.Count}");
            }
            else
            {
                Console.WriteLine("You were not able to finish the game.");
            }
            int count = 0;
            foreach (var swing in player.swings)
            {
                Console.WriteLine($"Swing #{++count} traveled {swing.distance} m and got new position {swing.position}.");
            }
        }

        /// <summary>
        /// Generates a new Golf course.
        /// </summary>
        /// <returns></returns>
        private static GolfCourse GenerateNewGolfCourse()
        {
            int cupPosition = new Random().Next(MIN_DISTANCE, MAX_DISTANCE);
            int courseLength = cupPosition * 2;
            int swingLimit = cupPosition / 100 + 1;
            PhysicsEngine engine = new PhysicsEngine();
            return new GolfCourse(cupPosition, courseLength, swingLimit, engine);
        }

        /// <summary>
        /// Throws exeptions if the rules are broken.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private static bool FollowsTheRules(Player player)
        {
            if (player.swings.Count >= player.golfCourse.swingLimit) // Has player overextended
            {
                throw new Exception("Too many Swings");
            }
            else if (player.currentPosition > player.golfCourse.length)
            {
                throw new Exception("Outside course error!");
            }
            return true;
        }

        /// <summary>
        /// Resets cursor position and waits for input until a valid one is given.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
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
