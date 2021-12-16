using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using Position;

namespace LeastSquaresPositioning;

public class Satellites
{
    public Point P1 { get;  }
    public Point P2 { get;  }
    public Point P3 { get;  }
    public Point P4 { get;  }

    public Satellites(Point p1, Point p2, Point p3, Point p4 )
    {
        P1 = p1;
        P2 = p2;
        P3 = p3;
        P4 = p4;
    }
    
    public Point Calculate(double d1, double d2, double d3, double d4)
    {
        DenseMatrix A = GetMatrixA();
        DenseMatrix B = GetMatrixB(d1,d2,d3,d4);
        Matrix<double> X = (A.Transpose() * A).Inverse() * (A.Transpose() * B);
        
        return new (X[0, 0], X[1, 0], X[2, 0]);
    }

    private DenseMatrix GetMatrixA()
    {
        return new(3, 3)
        {
            [0, 0] = P2.X - P1.X,
            [0, 1] = P2.Y - P1.Y,
            [0, 2] = P2.Z - P1.Z,
            [1, 0] = P3.X - P2.X,
            [1, 1] = P3.Y - P2.Y,
            [1, 2] = P3.Z - P2.Z,
            [2, 0] = P4.X - P3.X,
            [2, 1] = P4.Y - P3.Y,
            [2, 2] = P4.Z - P3.Z
        };
    }
    
    private DenseMatrix GetMatrixB(double d1, double d2, double d3, double d4)
    {
        return 0.5 * new DenseMatrix(3, 1)
        {
            [0, 0] = GetMatrixBElement(d1,d2,P1,P2),
            [1, 0] = GetMatrixBElement(d2,d3,P2,P3),
            [2, 0] = GetMatrixBElement(d3,d4,P3,P4),
        };
    }

    private double GetMatrixBElement(double d1, double d2, Point p1, Point p2)
    {
        return d1.Pow() - d2.Pow() +
               (p2.X.Pow() + p2.Y.Pow() + p2.Z.Pow()) -
               (p1.X.Pow() + p1.Y.Pow() + p1.Z.Pow());
    }
}