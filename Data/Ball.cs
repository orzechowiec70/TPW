namespace Data
{
    internal class Ball : IBall
    {
        public Vector2D position { get; set; }
        public Vector2D velocity { get; set; }
        public double radius { get; }


        public Ball(double Px, double Py, double Vx, double Vy, double inRadius)
        {
            position = new Vector2D(Px, Py);
            velocity = new Vector2D(Vx, Vy);
            radius = inRadius;
        }
      
    }
}
