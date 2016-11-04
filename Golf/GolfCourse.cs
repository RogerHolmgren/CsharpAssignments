namespace Golf
{
    internal class GolfCourse
    {
        public int cupPosition { get; private set; }
        public int length { get; private set; }
        public int swingLimit { get; internal set; }

        public GolfCourse(int cupPosition, int courseLength, int swingLimit)
        {
            this.cupPosition = cupPosition;
            this.length = courseLength;
            this.swingLimit = swingLimit;
        }

        public override string ToString()
        {
            return $"Distance to cup {cupPosition}, golf course length {length} meters in total.";
        }
    }
}