using GeneticAlgorithm;

namespace MutatedRug.Model;

public class MutatedRug(int populationSize) : GeneticAlgorithm<MySpecimen>(populationSize)
{
    private readonly int _tournamentSize = (int)Math.Ceiling((double)populationSize / 10);

    public override GenerationData[] Evolve(int generations)
    {
        var result = new List<GenerationData>();
        result.Add(new GenerationData(GetCurrentAverageFitness(), GetCurrentBestFitness()));

        while (CurrentGeneration < generations)
        {
            var newPopulation = new MySpecimen[Population.Length];

            for (var i = 0; i < Population.Length - 1; i++)
            {
                newPopulation[i] = TournamentSelection(_tournamentSize);
            }

            MutateEach(1);
            newPopulation[Population.Length - 1] = EliteHotDeckSelection();
            UpdatePopulation(newPopulation);

            result.Add(new GenerationData(GetCurrentAverageFitness(), GetCurrentBestFitness()));
        }

        return result.ToArray();
    }
}