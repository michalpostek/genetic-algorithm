using System.Text.Json;
using GeneticAlgorithm;

namespace Sinusie;

public class MySpecimen : ISpecimen
{
    private const int ParameterLength = 4;
    private const int ParameterMin = 0;
    private const int ParameterMax = 3;
    
    private const int SampleMinX = -1;
    private const float SampleStep = 0.2f;
    private const int SampleMaxX = 6;

    private readonly Dictionary<double, double> _samples;
    private readonly Parameter _pA;
    private readonly Parameter _pB;
    private readonly Parameter _pC;

    public MySpecimen()
    {
        _samples = JsonSerializer.Deserialize<Dictionary<double, double>>(File.ReadAllText("./data.json"))!;
        _pA = new Parameter(ParameterMin, ParameterMax, ParameterLength);
        _pB = new Parameter(ParameterMin, ParameterMax, ParameterLength);
        _pC = new Parameter(ParameterMin, ParameterMax, ParameterLength);
    }
    
    private MySpecimen(bool[] pA, bool[] pB, bool[] pC)
    {
        _samples = JsonSerializer.Deserialize<Dictionary<double, double>>(File.ReadAllText("./data.json"))!;
        _pA = new Parameter(ParameterMin, ParameterMax, pA);
        _pB = new Parameter(ParameterMin, ParameterMax, pB);
        _pC = new Parameter(ParameterMin, ParameterMax, pC);
    }
    
    public double GetFitness()
    {
        var results = new Dictionary<double, double>();
        var x = (float)SampleMinX;

        while (x <= SampleMaxX)
        {
            var y = _pA.GetValue() * Math.Sin(_pB.GetValue() * x + _pC.GetValue());
            results.Add(x, y);
            
            x += SampleStep;
        }

        double result = 0;

        foreach (var pair in results)
        {
            result += Math.Pow(_samples[pair.Key] - pair.Value, 2);
        }

        return result;
    }

    public void Mutate(int points)
    {
        var random = new Random();

        switch (random.Next(0, 3))
        {
            case 0:
                _pA.Mutate(points);
                break;
            case 1:
                _pB.Mutate(points);
                break;
            case 2:
                _pC.Mutate(points);
                break;
        }
    }

    public object Clone()
    {
        return new MySpecimen(_pA.Chromosomes, _pB.Chromosomes, _pC.Chromosomes);
    }
}