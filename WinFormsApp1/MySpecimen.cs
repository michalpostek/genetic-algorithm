using GeneticAlgorithm;

namespace WinFormsApp1;

public class MySpecimen : ISpecimen
{
    private const int ParameterLength = 4;
    private const int ParameterMin = 0;
    private const int ParameterMax = 100;

    private readonly Parameter _x1;
    private readonly Parameter _x2;

    public MySpecimen()
    {
        _x1 = new Parameter(ParameterMin, ParameterMax, ParameterLength);
        _x2 = new Parameter(ParameterMin, ParameterMax, ParameterLength);
    }

    private MySpecimen(bool[] x1, bool[] x2)
    {
        _x1 = new Parameter(ParameterMin, ParameterMax, x1);
        _x2 = new Parameter(ParameterMin, ParameterMax, x2);
    }
    
    public object Clone()
    {
        return new MySpecimen(_x1.Chromosomes.ToArray(), _x2.Chromosomes.ToArray());
    }
    
    public double GetFitness()
    {
        var x1 = _x1.GetValue();
        var x2 = _x2.GetValue();

        return Math.Sin(x1 * 0.05) + Math.Sin(x2 * 0.05) + 0.4 * Math.Sin(x1 * 0.15) * Math.Sin(x2 * 0.15);
    }
    
    public void Mutate(int points)
    {
        if (Random.Shared.Next(2) == 0)
        {
            _x1.Mutate(points);
        }
        else
        {
            _x2.Mutate(points);
        }
    }
}