using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace LeastSquaresPositioning;

public class Satellites
{
    public IList<Point> Points { get; }

    public Satellites(IList<Point> points)
    {
        Points = points is null ? throw new NullReferenceException() :
            points.Count < 4 ? throw new ArgumentException("It should be more than four satellites.") : points;
    }

    public void SetDistances(IList<double> distances)
    {
        if (distances?.Count != Points.Count)
        {
            throw new ArgumentException("Counts of distances do not match counts of satellites.");
        }

        for (int i = 0; i < Points.Count; i++)
        {
            Points[i].Distance = distances[i];
        }
    }

    public Point Calculate()
    {
        DenseMatrix A = GetMatrixA();
        DenseMatrix B = GetMatrixB();
        Matrix<double> X = (A.Transpose() * A).Inverse() * (A.Transpose() * B);

        return new(X[0, 0], X[1, 0], X[2, 0]);
    }

    private DenseMatrix GetMatrixA()
    {
        int dimension = Points.Count() - 1;
        DenseMatrix matrix = new(dimension, 3);

        for (int i = 0; i < dimension; i++)
        {
            matrix[i, 0] = Points[i + 1].X - Points[i].X;
            matrix[i, 1] = Points[i + 1].Y - Points[i].Y;
            matrix[i, 2] = Points[i + 1].Z - Points[i].Z;
        }

        return matrix;
    }

    private DenseMatrix GetMatrixB()
    {
        int dimension = Points.Count() - 1;
        DenseMatrix matrix = new(dimension, 1);

        for (int i = 0; i < dimension; i++)
        {
            matrix[i, 0] = GetMatrixBElement(Points[i], Points[i + 1]);
        }

        return 0.5 * matrix;
    }

    private double GetMatrixBElement(Point p1, Point p2)
    {
        return p1.Distance.Pow() - p2.Distance.Pow() +
               (p2.X.Pow() + p2.Y.Pow() + p2.Z.Pow()) -
               (p1.X.Pow() + p1.Y.Pow() + p1.Z.Pow());
    }
}