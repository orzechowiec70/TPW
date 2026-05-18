using System;
using System.Threading;

namespace Data
{
    public interface IBall 
    {
        Vector2D position { get; set; }
        Vector2D velocity { get; set; }

        double radius { get; }
        double weight { get; }

        object lockObject { get; }

        event EventHandler BallChanged;
        void StartMoving(CancellationToken token);
    }
}
