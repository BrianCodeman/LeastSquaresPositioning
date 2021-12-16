using LeastSquaresPositioning;
using Position;
using Shouldly;
using Xunit;

namespace Tests;

public class LeastSquaresPositioningTest
{
    public static IEnumerable<object[]> Data
    {
        get
        {
            yield return new object[]
            {
                new Point(-1, 0, 0), new Point(0, 1, 1), new Point(1, 0, 0), new Point(0, -1, 1),
                1, 1.414, 1, 1.414,
                new Point(0, 0, 0)
            };
            yield return new object[]
            {
                new Point(-1, 0, 0), new Point(0, 1, 1), new Point(1, 0, 0), new Point(0, -1, 1),
                2, 1.732, 0, 1.732,
                new Point(1, 0, 0)
            };
            yield return new object[]
            {
                new Point(-1, 0, 0), new Point(0, 1, 1), new Point(1, 0, 0), new Point(0, -1, 1),
                9.900, 8.307, 9.487, 9.434,
                new Point(2, 5, 8)
            };
        }
    }

    [Theory]
    [MemberData(nameof(Data))]
    void ShouldCalculateCorrectly(Point p1, Point p2, Point p3, Point p4,
        double d1, double d2, double d3, double d4,
        Point target)
    {
        Satellites satellites = new(p1, p2, p3, p4);
        satellites.Calculate(d1, d2, d3, d4).DistanceTo(target).ShouldBeLessThan(0.01);
    }
}