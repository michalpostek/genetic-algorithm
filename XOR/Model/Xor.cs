using GeneticAlgorithm;

namespace XOR.Model;

public class Xor() : Population<MyIndividual>(PopulationSize)
{
    private new const int PopulationSize = 13;
    private const int TournamentSize = 3;

    protected override Comparison<MyIndividual> CompareFitness => (x, y) => x.GetFitness().CompareTo(y.GetFitness());

    protected override MyIndividual[] EvolutionStrategy(MyIndividual[] population)
    {
        var newPopulation = new MyIndividual[PopulationSize];

        for (var i = 0; i < PopulationSize - 1; i++)
        {
            newPopulation[i] = TournamentSelection(TournamentSize);
            newPopulation[i].FlipBitMutation(3);
        }

        newPopulation[PopulationSize - 1] = EliteHotDeckSelection();

        return newPopulation;
    }
}