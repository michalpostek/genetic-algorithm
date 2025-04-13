namespace GeneticAlgorithm;

/// <summary>
///     Represents a population of individuals in a genetic algorithm.
///     Provides selection methods like tournament and hot deck selection, using the <see cref="CompareFitness" /> method
///     to compare individuals' fitness.
/// </summary>
public abstract class Population
{
    protected readonly int PopulationSize;
    private Genome[] _currentPopulation;

    protected Population(int populationSize)
    {
        PopulationSize = populationSize;
        _currentPopulation = InitPopulation();
    }

    protected abstract Comparison<Genome> CompareFitness { get; }

    protected abstract double GetIndividualFitness(Genome individual);

    protected abstract Genome CreateIndividual();

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
    protected abstract Genome[] EvolutionStrategy(Genome[] currentPopulation);

    public double GetCurrentAverageFitness()
    {
        return _currentPopulation.Average(GetIndividualFitness);
    }

    public double GetCurrentBestFitness()
    {
        Array.Sort(_currentPopulation, CompareFitness);

        return GetIndividualFitness(_currentPopulation.First());
    }

    protected Genome EliteHotDeckSelection()
    {
        Array.Sort(_currentPopulation, CompareFitness);

        return _currentPopulation.First().Clone();
    }

    protected Genome TournamentSelection(int tournamentSize)
    {
        var random = new Random();
        var tournament = new Genome[tournamentSize];

        for (var i = 0; i < tournamentSize; i++)
        {
            tournament[i] = _currentPopulation[random.Next(_currentPopulation.Length)];
        }

        Array.Sort(tournament, CompareFitness);

        return tournament.First().Clone();
    }

    private Genome[] InitPopulation()
    {
        var population = new Genome[PopulationSize];

        for (var i = 0; i < PopulationSize; i++)
        {
            population[i] = CreateIndividual();
        }

        return population;
    }
}