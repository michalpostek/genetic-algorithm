namespace GeneticAlgorithm;

public abstract class Population<T> where T : ISpecimen, new()
{
    private readonly Comparison<T> _compareFitness;
    private readonly int _populationSize;
    private T[] _currentPopulation;

    protected Population(int populationSize, Comparison<T> compareFitness)
    {
        _compareFitness = compareFitness;
        _populationSize = populationSize;
        _currentPopulation = InitPopulation();
    }

    public abstract void Evolve();

    protected void NextGeneration(T[] newPopulation)
    {
        _currentPopulation = newPopulation;
    }

    protected void MutateEach(int points)
    {
        foreach (var specimen in _currentPopulation)
        {
            specimen.Mutate(points);
        }
    }

    public double GetCurrentAverageFitness()
    {
        return _currentPopulation.Average(specimen => specimen.GetFitness());
    }

    public double GetCurrentBestFitness()
    {
        Array.Sort(_currentPopulation, _compareFitness);

        return _currentPopulation.First().GetFitness();
    }

    protected T EliteHotDeckSelection()
    {
        Array.Sort(_currentPopulation, _compareFitness);

        return (T)_currentPopulation.First().Clone();
    }

    protected T TournamentSelection(int tournamentSize)
    {
        var random = new Random();
        var tournament = new T[tournamentSize];

        for (var i = 0; i < tournamentSize; i++)
        {
            tournament[i] = _currentPopulation[random.Next(_currentPopulation.Length)];
        }

        Array.Sort(tournament, _compareFitness);

        return (T)tournament.First().Clone();
    }

    private T[] InitPopulation()
    {
        var population = new T[_populationSize];

        for (var i = 0; i < _populationSize; i++)
        {
            population[i] = new T();
        }

        return population;
    }
}