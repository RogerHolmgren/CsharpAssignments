using System;
using System.Collections.Generic;

namespace Golf
{
    internal class Player
    {
        private readonly GolfCourse golfCourse;
        public List<Swing> swings;
        public int currentPosition { get; private set; }

        public Player(GolfCourse golfCourse)
        {
            this.golfCourse = golfCourse;
            this.swings = new List<Swing>();
            this.currentPosition = 0;
        }

        public void DoSwing(int angle, int velocity)
        {
            int distanceTraveled = PhysicsEngine.getDistance(angle, velocity);
            updateCurrentPosition(distanceTraveled);
            swings.Add(new Swing(currentPosition, distanceTraveled));
        }

        public Swing lastSwing()
        {
            return swings[swings.Count - 1];
        }

        public void updateCurrentPosition(int distanceTraveled)
        {
            if (currentPosition > golfCourse.cupPosition)
            {
                currentPosition -= distanceTraveled;
            } else
            {
                currentPosition += distanceTraveled;
            }
            
        }

        public string Status()
        {
            return $"Swing #{swings.Count}. Your current position is {currentPosition}, the cup is {DistanceToCup()} meter away.";
        }

        public bool CanContinue()
        {
            // Is player allowed to hit the ball?
            if (swings.Count > golfCourse.swingLimit)
            {
                throw new Exception("Too many Swings");
            }
            else if (currentPosition > golfCourse.length)
            {
                throw new Exception("Outside course error!");
            }

            return DistanceToCup() != 0;
        }

        public int DistanceToCup()
        {
            return Math.Abs(golfCourse.cupPosition - currentPosition);
        }
    }

    internal class Swing
    {
        public int position { get; private set; }
        public int distance { get; private set; }

        public Swing(int position, int distance)
        {
            this.position = position;
            this.distance = distance;
        }
    }
}