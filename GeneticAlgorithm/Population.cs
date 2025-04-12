namespace GeneticAlgorithm;

/// <summary>
///     Represents a population of <typeparamref name="T" /> individuals in a genetic algorithm.
///     Provides selection methods like tournament and hot deck selection, using the <see cref="CompareFitness" /> method
///     to compare individuals' fitness.
/// </summary>
/// <typeparam name="T">The type of individual, which must implement the <see cref="IIndividual" /> interface.</typeparam>
public abstract class Population<T> where T : IIndividual, new()
{
    protected readonly int PopulationSize;
    private T[] _currentPopulation;

    protected Population(int populationSize)
    {
        PopulationSize = populationSize;
        _currentPopulation = InitPopulation();
    }

    protected abstract Comparison<T> CompareFitness { get; }

    /// <summary>
    ///     Replaces the current population with a new generation using the provided <see cref="EvolutionStrategy" />
    ///     implementation.
    /// </summary>
    public void Evolve()
    {
        _currentPopulation = EvolutionStrategy(_currentPopulation);
    }

    /// <summary>
    ///     Describes how the current generation evolves into the next one.
    ///     Applies selections and genetic operators such as crossovers and mutations.
    /// </summary>
    /// <param name="currentPopulation">The list of individuals representing the current generation. </param>
    /// <returns>A new list of individuals forming the next generation. </returns>
    protected abstract T[] EvolutionStrategy(T[] currentPopulation);

    public double GetCurrentAverageFitness()
    {
        return _currentPopulation.Average(individual => individual.GetFitness());
    }

    public double GetCurrentBestFitness()
    {
        Array.Sort(_currentPopulation, CompareFitness);

        return _currentPopulation.First().GetFitness();
    }

    protected T EliteHotDeckSelection()
    {
        Array.Sort(_currentPopulation, CompareFitness);

        return (T)_currentPopulation.First().Clone();
    }

    protected T TournamentSelection(int tournamentSize)
    {
        var random = new Random();
        var tournament = new T[tournamentSize];

        for (var i = 0; i < tournamentSize; i++) tournament[i] = _currentPopulation[random.Next(_currentPopulation.Length)];

        Array.Sort(tournament, CompareFitness);

        return (T)tournament.First().Clone();
    }

    private T[] InitPopulation()
    {
        var population = new T[PopulationSize];

        for (var i = 0; i < PopulationSize; i++) population[i] = new T();

        return population;
    }
}