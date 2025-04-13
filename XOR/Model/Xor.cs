using GeneticAlgorithm;

namespace XOR.Model;

public class Xor() : Population(PopulationSize)
{
    private const int Parameters = 9;
    private const int ParameterLength = 4;
    private const int ParameterMin = -10;
    private const int ParameterMax = 10;
    private new const int PopulationSize = 13;
    private const int TournamentSize = 3;

    private readonly ParameterEncoder _parameterEncoder = new(ParameterMin, ParameterMax, ParameterLength);

    protected override Comparison<Genome> CompareFitness => (x, y) => GetIndividualFitness(x).CompareTo(GetIndividualFitness(y));

    protected override double GetIndividualFitness(Genome individual)
    {
        var parameterValues = new double[Parameters];

        for (var i = 0; i < Parameters; i++)
        {
            var parameter = individual.Chromosomes.Skip(i * ParameterLength).Take(ParameterLength).ToArray();
            parameterValues[i] = _parameterEncoder.GetValue(parameter);
        }

        return FitnessCalculator.Calculate(parameterValues);
    }

    protected override Genome CreateIndividual()
    {
        return new Genome(Parameters * ParameterLength);
    }

    protected override Genome[] EvolutionStrategy(Genome[] population)
    {
        var newPopulation = new Genome[PopulationSize];

        for (var i = 0; i < PopulationSize - 1; i++)
        {
            newPopulation[i] = TournamentSelection(TournamentSize);
            newPopulation[i].FlipBit(3);
        }

        newPopulation[PopulationSize - 1] = EliteHotDeckSelection();

        return newPopulation;
    }
}