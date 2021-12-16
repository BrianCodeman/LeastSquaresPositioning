namespace LeastSquaresPositioning;

public record Point(double X, double Y, double Z)
{
    private double _distance;

    public double Distance
    {
        get => _distance;
        set => _distance = value >= 0 ? value : throw new ArgumentException("distance could not be negative.");
    }
}