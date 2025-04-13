using GeneticAlgorithm;

namespace Sinusie.Model;

public class Sinusie() : Population(PopulationSize)
{
    private const int Parameters = 3;
    private const int ParameterLength = 4;
    private const int ParameterMin = 0;
    private const int ParameterMax = 3;
    private new const int PopulationSize = 13;
    private const int TournamentSize = 3;
    private static readonly (int x, int y)[] CrossoverIndexes = [(0, 1), (2, 3), (8, 9), (10, 11)];

    private readonly ParameterEncoder _parameterEncoder = new(ParameterMin, ParameterMax, ParameterLength);

    protected override Comparison<Genome> CompareFitness => (x, y) => GetIndividualFitness(x).CompareTo(GetIndividualFitness(y));

    protected override double GetIndividualFitness(Genome individual)
    {
        var pA = _parameterEncoder.GetValue(individual.Chromosomes.Take(ParameterLength).ToArray());
        var pB = _parameterEncoder.GetValue(individual.Chromosomes.Skip(ParameterLength).Take(ParameterLength).ToArray());
        var pC = _parameterEncoder.GetValue(individual.Chromosomes.Skip(ParameterLength * 2).Take(ParameterLength).ToArray());

        var diff = FitnessCalculator.CalculateFitness(pA, pB, pC);

        return diff;
    }

    protected override Genome CreateIndividual()
    {
        return new Genome(ParameterLength * Parameters);
    }

    protected override Genome[] EvolutionStrategy(Genome[] population)
    {
        var newPopulation = new Genome[PopulationSize];

        for (var i = 0; i < PopulationSize - 1; i++)
        {
            newPopulation[i] = TournamentSelection(TournamentSize);
        }

        foreach (var (p1, p2) in CrossoverIndexes)
        {
            var children = Genome.Crossover(newPopulation[p1], newPopulation[p2]);

            newPopulation[p1] = children.Item1;
            newPopulation[p2] = children.Item2;
        }

        for (var i = 4; i < newPopulation.Length - 1; i++)
        {
            newPopulation[i].FlipBit(1);
        }

        newPopulation[PopulationSize - 1] = EliteHotDeckSelection();

        return newPopulation;
    }
}