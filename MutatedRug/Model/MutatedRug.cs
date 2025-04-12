using GeneticAlgorithm;

namespace MutatedRug.Model;

public class MutatedRug(int populationSize) : Population<MyIndividual>(populationSize, CompareFitness)
{
    private readonly int _tournamentSize = (int)Math.Ceiling((double)populationSize / 10);

    private static Comparison<IIndividual> CompareFitness => (x, y) => y.GetFitness().CompareTo(x.GetFitness());

    protected override MyIndividual[] EvolutionStrategy(MyIndividual[] population)
    {
        var newPopulation = new MyIndividual[PopulationSize];

        for (var i = 0; i < PopulationSize - 1; i++)
        {
            var winner = TournamentSelection(_tournamentSize);
            winner.Mutate(1);

            newPopulation[i] = winner;
        }

        newPopulation[PopulationSize - 1] = EliteHotDeckSelection();

        return newPopulation;
    }
}