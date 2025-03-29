using GeneticAlgorithm;

namespace Sinusie;

public class Sinusie(int populationSize) : GeneticAlgorithm<MySpecimen>(populationSize)
{
    public override GenerationData[] Evolve(int generations)
    {
        throw new NotImplementedException();
    }
}