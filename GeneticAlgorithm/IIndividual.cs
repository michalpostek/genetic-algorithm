namespace GeneticAlgorithm;

public interface IIndividual : ICloneable
{
    public double GetFitness();
}