using System;
using System.Collections.Generic;

namespace Golf
{
    internal class Player
    {
        private readonly GolfCourse golfCourse;
        public List<Swing> swings;

        public Player(GolfCourse golfCourse)
        {
            this.golfCourse = golfCourse;
            this.swings = new List<Swings>();
        }

        public void HitTheBall(int angle, int velocity)
        {
            // Is player allowed to hit the ball?
            if (swings.Count > golfCourse.swingLimit)
            {
                throw new Exception("Too many Swings");
            }
            else if (lastSwing().position > golfCourse.length)
            {
                throw new Exception("Outside course error!");
            }

            int distance = PhysicsEngine.getDistance(angle, velocity);
            swings.Add(new Swing(distance));
        }

        public Swing lastSwing()
        {
            return swings[swings.Count - 1];
        }

        public string Info()
        {
            return $"Swing #{swings}. Your current position is {position}, the cup is {golfCourse.cupPosition - position} meter away.";
        }

        public bool IsDone()
        {
            return position == golfCourse.cupPosition;
        }
    }

    internal class Swing
    {
        public int position { get; internal set; }
        public int distance { get; internal set; }

        public Swing(int distance)
        {
            this.distance = distance;

        }
    }
}