namespace GeneticAlgorithm;

/// <summary>
///     Represents an individual in a genetic algorithm.
///     Requires the implementation of the <see cref="GetFitness" /> method to calculate the individual's fitness.
///     Implements the <see cref="ICloneable" /> interface for cloning individuals.
/// </summary>
public interface IIndividual : ICloneable
{
    public double GetFitness();
}