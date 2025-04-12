using GeneticAlgorithm;

namespace XOR.Model;

public class Xor() : Population<MyIndividual>(PopulationSize, CompareFitness)
{
    private new const int PopulationSize = 13;
    private const int TournamentSize = 3;

    private static readonly Comparison<MyIndividual> CompareFitness = (x, y) => x.GetFitness().CompareTo(y.GetFitness());

    public override void Evolve()
    {
        var newPopulation = new MyIndividual[PopulationSize];

        for (var i = 0; i < PopulationSize - 1; i++)
        {
            newPopulation[i] = TournamentSelection(TournamentSize);
            newPopulation[i].Mutate(3);
        }

        newPopulation[PopulationSize - 1] = EliteHotDeckSelection();

        NextGeneration(newPopulation);
    }
}