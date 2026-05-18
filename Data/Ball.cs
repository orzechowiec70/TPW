using System;
using System.Dynamic;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    internal class Ball : IBall
    {
        public Vector2D position { get; set; }
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
                while (!token.IsCancellationRequested)
                {
                    lock (lockObject)
                    {
                        double newX = position.X + velocity.X;
                        double newY = position.Y + velocity.Y;

                        position = new Vector2D(newX, newY);
                    }

                    BallChanged?.Invoke(this, EventArgs.Empty);
                    await Task.Delay(10, token);
                }
            }, token);
        }
    }
}
