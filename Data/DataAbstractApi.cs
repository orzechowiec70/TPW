using System;

namespace Data
{
    public abstract class DataAbstractApi
    {
        public abstract IBoard GetBoard();

        public static IBall CreateBall(double px, double py, double vx, double vy, double radius)
        {
            return new Ball(px, py, vx, vy, radius);
        }

        public static DataAbstractApi CreateApi(double boardWidth = 800, double boardHeight = 400)
        {
            return new DataApi(boardWidth, boardHeight);
        }
        
        private class DataApi: DataAbstractApi
        {
            private readonly IBoard board;
            
            public DataApi(double width, double height)
            {
                board = new Board(width, height);
            }

            public override IBoard GetBoard()
            {
                return board;
            }
        }
    }
}
