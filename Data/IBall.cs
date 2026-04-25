namespace Data
{
    public interface IBall 
    {
        Vector2D position { get; set; }
        Vector2D velocity { get; set; }

        double radius { get; }
    }
}
