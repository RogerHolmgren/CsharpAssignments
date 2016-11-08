using System;
using System.Collections.Generic;

namespace Golf
{
    internal class Player
    {
        public List<Swing> swings;
        public GolfCourse golfCourse { get; private set; }
        public int currentPosition { get; private set; }

        public Player(GolfCourse golfCourse)
        {
            this.golfCourse = golfCourse;
            this.swings = new List<Swing>();
            this.currentPosition = 0;
        }

        /// <summary>
        /// Calculates the distance travled and adds that swing to the collection.
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="velocity"></param>
        public void DoSwing(int angle, int velocity)
        {
            int distanceTraveled = golfCourse.PhysicsEngine.getDistance(angle, velocity);
            updateCurrentPosition(distanceTraveled);
            swings.Add(new Swing(currentPosition, distanceTraveled));
        }

        /// <summary>
        /// The most recent swing.
        /// </summary>
        /// <returns></returns>
        public Swing lastSwing()
        {
            return swings[swings.Count - 1];
        }

        /// <summary>
        /// Return a formated string with information about the player.
        /// </summary>
        /// <returns></returns>
        public string Status()
        {
            return $"Your current position is {currentPosition}, the cup is {DistanceToCup()} meter away ({golfCourse.swingLimit - swings.Count} swings left).";
        }

        public int DistanceToCup()
        {
            return Math.Abs(golfCourse.cupPosition - currentPosition);
        }

        /// <summary>
        /// Updates the current position based on the distance traveled.
        /// Takes into account whether you position is in front of or behind the cup position.
        /// </summary>
        /// <param name="distanceTraveled"></param>
        private void updateCurrentPosition(int distanceTraveled)
        {
            if (currentPosition > golfCourse.cupPosition)
            {
                currentPosition -= distanceTraveled;
            }
            else
            {
                currentPosition += distanceTraveled;
            }

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