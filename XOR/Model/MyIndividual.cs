using GeneticAlgorithm;

namespace XOR.Model;

public class MyIndividual : IIndividual
{
    private const int Parameters = 9;
    private const int ParameterLength = 4;
    private const int ParameterMin = -10;
    private const int ParameterMax = 10;
    private readonly Genome _genome;
    private readonly ParameterEncoder _parameterEncoder;

    public MyIndividual() : this(new Genome(Parameters * ParameterLength))
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
        var parameterValues = new double[Parameters];

        for (var i = 0; i < Parameters; i++)
        {
            var parameter = _genome.Chromosomes.Skip(i * ParameterLength).Take(ParameterLength).ToArray();
            parameterValues[i] = _parameterEncoder.GetValue(parameter);
        }

        return FitnessCalculator.Calculate(parameterValues);
    }

    public object Clone()
    {
        return new MyIndividual(_genome.Chromosomes.ToArray());
    }

    public void Mutate(int points)
    {
        _genome.Mutate(points);
    }
}