using GeneticAlgorithm;

namespace Sinusie.Model;

public class Sinusie() : Population<MyIndividual>(PopulationSize, CompareFitness)
{
    private new const int PopulationSize = 13;
    private const int TournamentSize = 3;

    private static readonly Comparison<MyIndividual> CompareFitness = (x, y) => x.GetFitness().CompareTo(y.GetFitness());
    private static readonly (int x, int y)[] CrossoverIndexes = [(0, 1), (2, 3), (8, 9), (10, 11)];

    protected override MyIndividual[] EvolutionStrategy(MyIndividual[] population)
    {
        var newPopulation = new MyIndividual[PopulationSize];

        for (var i = 0; i < PopulationSize - 1; i++)
        {
            newPopulation[i] = TournamentSelection(TournamentSize);
        }

        foreach (var (p1, p2) in CrossoverIndexes)
        {
            var children = newPopulation[p1].Crossover(newPopulation[p2]);

            newPopulation[p1] = children.Item1;
            newPopulation[p2] = children.Item2;
        }

        for (var i = 4; i < newPopulation.Length - 1; i++)
        {
            newPopulation[i].FlipBitMutation(1);
        }

        newPopulation[PopulationSize - 1] = EliteHotDeckSelection();

        return newPopulation;
    }
}
