using GeneticAlgorithm;

namespace MutatedRug.Model;

public class MutatedRug(int populationSize) : Population(populationSize)
{
    private const int Parameters = 2;
    private const int ParameterLength = 4;
    private const int ParameterMin = 0;
    private const int ParameterMax = 100;

    private readonly ParameterEncoder _parameterEncoder = new(ParameterMin, ParameterMax, ParameterLength);
    private readonly int _tournamentSize = (int)Math.Ceiling((double)populationSize / 10);

    protected override Comparison<Genome> CompareFitness => (x, y) => GetIndividualFitness(y).CompareTo(GetIndividualFitness(x));

    protected override Genome CreateIndividual()
    {
        return new Genome(ParameterLength * Parameters);
    }

    protected override double GetIndividualFitness(Genome individual)
    {
        var x1 = _parameterEncoder.GetValue(individual.Chromosomes.Take(ParameterLength).ToArray());
        var x2 = _parameterEncoder.GetValue(individual.Chromosomes.Skip(ParameterLength).Take(ParameterLength).ToArray());

        return Math.Sin(x1 * 0.05) + Math.Sin(x2 * 0.05) + 0.4 * Math.Sin(x1 * 0.15) * Math.Sin(x2 * 0.15);
    }

    protected override Genome[] EvolutionStrategy(Genome[] population)
    {
        var newPopulation = new Genome[PopulationSize];

        for (var i = 0; i < PopulationSize - 1; i++)
        {
            var winner = TournamentSelection(_tournamentSize);
            winner.FlipBit(1);

            newPopulation[i] = winner;
        }

        newPopulation[PopulationSize - 1] = EliteHotDeckSelection();

        return newPopulation;
    }
}