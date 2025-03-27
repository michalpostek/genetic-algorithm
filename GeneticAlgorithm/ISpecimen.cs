namespace GeneticAlgorithm;

public interface ISpecimen : ICloneable
{
    public double GetFitness();

    public void Mutate(int points);
}