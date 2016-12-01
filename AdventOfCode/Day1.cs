using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day1
    {
        static string input = "R4, R3, R5, L3, L5, R2, L2, R5, L2, R5, R5, R5, R1, R3, L2, L2, L1, R5, L3, R1, L2, R1, L3, L5, L1, R3, L4, R2, R4, L3, L1, R4, L4, R3, L5, L3, R188, R4, L1, R48, L5, R4, R71, R3, L2, R188, L3, R2, L3, R3, L5, L1, R1, L2, L4, L2, R5, L3, R3, R3, R4, L3, L4, R5, L4, L4, R3, R4, L4, R1, L3, L1, L1, R4, R1, L4, R1, L1, L3, R2, L2, R2, L1, R5, R3, R4, L5, R2, R5, L5, R1, R2, L1, L3, R3, R1, R3, L4, R4, L4, L1, R1, L2, L2, L4, R1, L3, R4, L2, R3, L1, L5, R4, R5, R2, R5, R1, R5, R1, R3, L3, L2, L2, L5, R2, L2, R5, R5, L2, R3, L5, R5, L2, R4, R2, L1, R3, L5, R3, R2, R5, L1, R3, L2, R2, R1";
        //static string input = "R8, R4, R4, R8";
        static Dir direction = Dir.North;
        enum Dir
        {
            North,
            East,
            South,
            West
        };
        static int x = 0, y = 0;
        static List<string> cords = new List<string>();

        public static void Solve()
        {
            string[] data = Regex.Replace(input, @"\s+", "").Split(',');
            bool Found = false;
            foreach (var d in data)
            {
                int distance = int.Parse(d.Substring(1));
                UpdateDirection(d);
                for (int i = 1; i <= distance; i++)
                {
                    switch (direction)
                    {
                        case Dir.North:
                            y++;
                            break;
                        case Dir.East:
                            x++;
                            break;
                        case Dir.South:
                            y--;
                            break;
                        case Dir.West:
                            x--;
                            break;
                        default:
                            break;
                    }
                    if (!Found && cords.Contains($"{x}:{y}"))
                    {
                        Console.WriteLine($"First place visited twice: {x}:{y} {Math.Abs(x) + Math.Abs(y)}");
                        Found = true;
                    }
                    cords.Add($"{x}:{y}");
                }
            }

            //Console.WriteLine($"x: {x}, y: {y}");
            Console.WriteLine($"Final step is blocks away: {x + y}");
        }

        private static void UpdateDirection(string d)
        {
            if (d.StartsWith("R"))
            {
                switch (direction)
                {
                    case Dir.North:
                        direction = Dir.East;
                        break;
                    case Dir.East:
                        direction = Dir.South;
                        break;
                    case Dir.South:
                        direction = Dir.West;
                        break;
                    case Dir.West:
                        direction = Dir.North;
                        break;
                    default:
                        break;
                }
            }
            else if (d.StartsWith("L"))
            {
                switch (direction)
                {
                    case Dir.North:
                        direction = Dir.West;
                        break;
                    case Dir.East:
                        direction = Dir.North;
                        break;
                    case Dir.South:
                        direction = Dir.East;
                        break;
                    case Dir.West:
                        direction = Dir.South;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
