namespace Meteor.Core.Utils
{
    public class Ticker
    {
        public delegate void OnTickDelegate(double deltaTime);

        private readonly OnTickDelegate onTick;
        private readonly double duration;

        private double lastTick = 0;

        public Ticker(OnTickDelegate onTick, int tickRate)
        {
            if (tickRate <= 0)
            {
                throw new System.ArgumentOutOfRangeException("tickRate");
            }

            if (onTick == null)
            {
                throw new System.ArgumentNullException("onTick");
            }

            this.onTick = onTick;
            this.duration = 1f / tickRate;

            this.lastTick = (double)System.DateTime.Now.Ticks / (double)System.TimeSpan.TicksPerSecond;
        }

        public void Update()
        {
            double currentTime = (double)System.DateTime.Now.Ticks / (double)System.TimeSpan.TicksPerSecond;
            if (currentTime - this.lastTick >= this.duration)
            {
                this.onTick.Invoke(currentTime - this.lastTick);
                this.lastTick = currentTime;
            }
        }
    }
}
