namespace GeneticAlgorithm;

/// <summary>
///     Represents a population of individuals in a genetic algorithm.
///     Provides selection methods like tournament and hot deck selection, using the <see cref="CompareFitness" /> method
///     to compare individuals' fitness.
/// </summary>
public abstract class Population
{
    public static Random _random = null;
    protected readonly int PopulationSize;
    private Individual[] _currentPopulation;

    protected Population(int populationSize)
    {
        PopulationSize = populationSize;
        _currentPopulation = InitPopulation();
    }

    protected abstract Comparison<Individual> CompareFitness { get; }

    protected abstract double GetFitness(Individual individual);

    protected abstract Individual CreateIndividual();

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
    protected abstract Individual[] EvolutionStrategy(Individual[] currentPopulation);

    public double GetCurrentAverageFitness()
    {
        return _currentPopulation.Average(GetFitness);
    }

    public double GetCurrentBestFitness()
    {
        Array.Sort(_currentPopulation, CompareFitness);

        return GetFitness(_currentPopulation.First());
    }

    protected Individual EliteHotDeckSelection()
    {
        Array.Sort(_currentPopulation, CompareFitness);

        return _currentPopulation.First().Clone();
    }

    protected Individual TournamentSelection(int tournamentSize)
    {
        // var random = new Random();
        var tournament = new Individual[tournamentSize];

        for (var i = 0; i < tournamentSize; i++)
        {
            tournament[i] = _currentPopulation[_random.Next(_currentPopulation.Length)];
        }

        Array.Sort(tournament, CompareFitness);

        return tournament.First().Clone();
    }

    private Individual[] InitPopulation()
    {
        var population = new Individual[PopulationSize];

        for (var i = 0; i < PopulationSize; i++)
        {
            population[i] = CreateIndividual();
        }

        return population;
    }
}
