using GeneticAlgorithm;

namespace MutatedRug.Model;

public class MutatedRug(int populationSize) : Population<MySpecimen>(populationSize, CompareFitness)
{
    private readonly int _tournamentSize = (int)Math.Ceiling((double)populationSize / 10);

    private static Comparison<ISpecimen> CompareFitness => (x, y) => y.GetFitness().CompareTo(x.GetFitness());

    public override void Evolve()
    {
        var newPopulation = new MySpecimen[PopulationSize];

        for (var i = 0; i < PopulationSize - 1; i++)
        {
            var winner = TournamentSelection(_tournamentSize);
            winner.Mutate(1);
            
            newPopulation[i] = winner;
        }
        
        newPopulation[PopulationSize - 1] = EliteHotDeckSelection();
        NextGeneration(newPopulation);
    }
}