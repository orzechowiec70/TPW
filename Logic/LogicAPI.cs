using Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    internal class LogicAPI : LogicAbstractApi
    {
        private readonly IBoard? board;
        private List<IBall> balls;
        private Random random = new Random();
        private  CancellationTokenSource cancelTokenSource;
        public override event EventHandler PositionChanged;

        public LogicAPI(IBoard Board)
        {
            board = Board;
            balls = new List<IBall>();
        }

        public override IEnumerable<IBall> GetBalls()
        {
            return balls;
        }
        public override void StartSimulation(int numberOfBalls)
        {
            if (cancelTokenSource != null && !cancelTokenSource.IsCancellationRequested)
                return;
            balls.Clear();
            double radius = 15;


            for (int i = 0; i < numberOfBalls; i++)
            {
                double px = random.NextDouble() * (board.width - 2 * radius) + radius;
                double py = random.NextDouble() * (board.height - 2 * radius) + radius;

                double vx = (random.NextDouble() * 6) - 3;
                double vy = (random.NextDouble() * 6) - 3;
                IBall newBall = DataAbstractApi.CreateBall(px, py, vx, vy, radius);
                balls.Add(newBall);
            }
            cancelTokenSource = new CancellationTokenSource();

            Task.Run(() => SimulationLoop(cancelTokenSource.Token));
        }
        public override void StopSimulation()
        {
            cancelTokenSource?.Cancel();
        }
        private async Task SimulationLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                foreach (var ball in balls)
                {
                    MoveBall(ball);
                   
                }
                PositionChanged?.Invoke(this, EventArgs.Empty);
                // Odświeżanie co ~16ms daje płynność ok. 60 FPS (klatek na sekundę)
                await Task.Delay(16, token);
            }
        }
        private void MoveBall(IBall ball)
        {
            // Obliczanie nowej potencjalnej pozycji
            double newX = ball.position.X + ball.velocity.X;
            double newY = ball.position.Y + ball.velocity.Y;

            // Pobieranie aktualnej prędkość, by móc ją wyzerować w razie kolizji
            double vx = ball.velocity.X;
            double vy = ball.velocity.Y;

            // Sprawdzenie granic na osi X
            if (newX - ball.radius <= 0) // Lewa krawędź
            {
                newX = ball.radius;
                vx = 0; // Stop
            }
            else if (newX + ball.radius >= board.width) // Prawa krawędź
            {
                newX = board.width - ball.radius;
                vx = 0; // Stop
            }

            // Sprawdzenie granic na osi Y
            if (newY - ball.radius <= 0) // Górna krawędź
            {
                newY = ball.radius;
                vy = 0; // Stop
            }
            else if (newY + ball.radius >= board.height) // Dolna krawędź
            {
                newY = board.height - ball.radius;
                vy = 0; // Stop
            }

            // Aktualizacja pozycji i prędkości
            ball.velocity = new Vector2D(vx, vy);
            ball.position = new Vector2D(newX, newY);
        }
    }
}