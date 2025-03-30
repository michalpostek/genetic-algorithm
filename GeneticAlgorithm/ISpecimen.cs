namespace GeneticAlgorithm;

public interface ISpecimen : ICloneable
{
    public double GetFitness();
}