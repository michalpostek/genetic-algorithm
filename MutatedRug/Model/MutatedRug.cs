using GeneticAlgorithm;

namespace MutatedRug.Model;

public class MutatedRug(int populationSize) : GeneticAlgorithm<MySpecimen>(populationSize, CompareFitness)
{
    private readonly int _tournamentSize = (int)Math.Ceiling((double)populationSize / 10);

    private static Comparison<ISpecimen> CompareFitness => (x, y) => y.GetFitness().CompareTo(x.GetFitness());

    public override void Evolve()
    {
        var newPopulation = new MySpecimen[Population.Length];

        for (var i = 0; i < Population.Length - 1; i++)
        {
            newPopulation[i] = TournamentSelection(_tournamentSize);
        }

        MutateEach(1);
        newPopulation[Population.Length - 1] = EliteHotDeckSelection();
        UpdatePopulation(newPopulation);
    }
}