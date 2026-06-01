using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    internal class Ball : IBall
    {
        public Vector2D position {  get; set; }
        public Vector2D velocity { get; set; }
        public double radius { get; }
        public double weight { get; }
        public object lockObject { get; } = new object();

        public event EventHandler BallChanged;


        public Ball(double Px, double Py, double Vx, double Vy, double inRadius , double inWeight)
        {
            position = new Vector2D(Px, Py);
            velocity = new Vector2D(Vx, Vy);
            radius = inRadius;
            weight = inWeight;
        }
        public void StartMoving(CancellationToken token)
        {
            Task.Run(async () =>
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                while (!token.IsCancellationRequested)
                {
                    double timeElapsed = stopwatch.Elapsed.TotalSeconds;
                    stopwatch.Restart();

                    lock (lockObject)
                    {
                        double newX = position.X + (velocity.X * timeElapsed * 50);
                        double newY = position.Y + (velocity.Y * timeElapsed * 50);
                        position = new Vector2D(newX, newY);
                    }

                    BallChanged?.Invoke(this, EventArgs.Empty);
                    DataAbstractApi.logger.LogBallState(this);
                    await Task.Delay(10, token);
                }

            }, token);
        }
    }
}
