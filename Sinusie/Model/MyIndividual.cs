using GeneticAlgorithm;

namespace Sinusie.Model;

public class MyIndividual : IIndividual
{
    private const int Parameters = 3;
    private const int ParameterLength = 4;
    private const int ParameterMin = 0;
    private const int ParameterMax = 3;
    private readonly Genome _genome;

    private readonly ParameterEncoder _parameterEncoder;

    public MyIndividual() : this(new Genome(ParameterLength * Parameters))
    {
    }

    private MyIndividual(bool[] chromosomes) : this(new Genome(chromosomes))
    {
    }

    private MyIndividual(Genome genome)
    {
        _genome = genome;
        _parameterEncoder = new ParameterEncoder(ParameterMin, ParameterMax, ParameterLength);
    }

    public double GetFitness()
    {
        var pA = _parameterEncoder.GetValue(_genome.Chromosomes.Take(ParameterLength).ToArray());
        var pB = _parameterEncoder.GetValue(_genome.Chromosomes.Skip(ParameterLength).Take(ParameterLength).ToArray());
        var pC = _parameterEncoder.GetValue(_genome.Chromosomes.Skip(ParameterLength * 2).Take(ParameterLength).ToArray());

        var diff = FitnessCalculator.CalculateFitness(pA, pB, pC);

        return diff;
    }

    public object Clone()
    {
        return new MyIndividual(_genome.Chromosomes.ToArray());
    }

    public void Mutate(int points)
    {
        _genome.Mutate(points);
    }

    public Tuple<MyIndividual, MyIndividual> Crossover(MyIndividual other)
    {
        var children = Genome.Crossover(_genome, other._genome);

        return new Tuple<MyIndividual, MyIndividual>(
            new MyIndividual(children.Item1),
            new MyIndividual(children.Item2)
        );
    }
}