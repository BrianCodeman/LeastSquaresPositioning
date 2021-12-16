using LeastSquaresPositioning;

namespace Tests;

public static class PointExtensions
{
    public static double DistanceTo(this Point from, Point to)
    {
        return  Math.Sqrt((from.X - to.X).Pow() + (from.Y - to.Y).Pow() + (from.Z - to.Z).Pow());
    }
}