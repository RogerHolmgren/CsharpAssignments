using System;

namespace Golf
{
    internal class GolfCourse
    {
        public int cupPosition { get; private set; }
        public int length { get; private set; }
        public int swingLimit { get; private set; }
        public PhysicsEngine PhysicsEngine { get; private set; }


        public GolfCourse(int cupPosition, int courseLength, int swingLimit, PhysicsEngine engine)
        {
            this.cupPosition = cupPosition;
            this.length = courseLength;
            this.swingLimit = swingLimit;
            this.PhysicsEngine = engine;
        }

        public string Description()
        {
            return $"Distance to cup {cupPosition}, golf course length {length} meters in total. Swing limit is {swingLimit}.";
        }
    }
}