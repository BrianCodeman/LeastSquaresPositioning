using LeastSquaresPositioning;
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
                new[] {new Point(-1, 0, 0), new Point(0, 1, 1), new Point(1, 0, 0), new Point(0, -1, 1)},
                new[] {1, 1.414, 1, 1.414},
                new Point(0, 0, 0)
            };
            yield return new object[]
            {
                new[] {new Point(-1, 0, 0), new Point(0, 1, 1), new Point(1, 0, 0), new Point(0, -1, 1)},
                new[] {2, 1.732, 0, 1.732},
                new Point(1, 0, 0)
            };
            yield return new object[]
            {
                new[] {new Point(-1, 0, 0), new Point(0, 1, 1), new Point(1, 0, 0), new Point(0, -1, 1)},
                new[] {9.900, 8.307, 9.487, 9.434},
                new Point(2, 5, 8)
            };
            yield return new object[]
            {
                new[]
                {
                    new Point(-1, 0, 0), new Point(0, 1, 1), new Point(1, 0, 0), new Point(0, -1, 1), new Point(0, 0, 0)
                },
                new[] {4.123, 2.449, 3.606, 3.742, 3.742},
                new Point(1, 2, 3)
            };
        }
    }

    [Theory]
    [MemberData(nameof(Data))]
    void ShouldCalculateCorrectly(IList<Point> points, IList<double> distances, Point target)
    {
        Satellites satellites = new(points);
        satellites.SetDistances(distances);
        satellites.Calculate().DistanceTo(target).ShouldBeLessThan(0.01);
    }
}