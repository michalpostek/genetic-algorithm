using GeneticAlgorithm;

namespace MutatedRug.Model;

public class MyIndividual : IIndividual
{
    private const int Parameters = 2;
    private const int ParameterLength = 4;
    private const int ParameterMin = 0;
    private const int ParameterMax = 100;
    
    private readonly ParameterEncoder _parameterEncoder;
    private readonly Genome _genome;

    public MyIndividual()
    {
        _genome = new Genome(Parameters * ParameterLength);
        _parameterEncoder = new ParameterEncoder(ParameterMin, ParameterMax, ParameterLength);
    }

    private MyIndividual(bool[] chromosomes)
    {
        _genome = new Genome(chromosomes);
        _parameterEncoder = new ParameterEncoder(ParameterMin, ParameterMax, ParameterLength);
    }

    public object Clone()
    {
        return new MyIndividual(_genome.Chromosomes.ToArray());
    }

    public double GetFitness()
    {
        var x1 = _parameterEncoder.GetValue(_genome.Chromosomes.Take(ParameterLength).ToArray());
        var x2 = _parameterEncoder.GetValue(_genome.Chromosomes.Skip(ParameterLength).Take(ParameterLength).ToArray());

        return Math.Sin(x1 * 0.05) + Math.Sin(x2 * 0.05) + 0.4 * Math.Sin(x1 * 0.15) * Math.Sin(x2 * 0.15);
    }

    public void Mutate(int points)
    {
        _genome.Mutate(points);
    }
}