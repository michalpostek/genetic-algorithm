namespace GeneticAlgorithm;

public abstract class GeneticAlgorithm<T> where T : ISpecimen, new()
{
    private readonly int _populationSize;
    private readonly Comparison<T> _compareFitness;

    protected GeneticAlgorithm(int populationSize, Comparison<T> compareFitness)
    {
        _compareFitness = compareFitness;
        _populationSize = populationSize;
        Population = InitPopulation();
    }

    protected int CurrentGeneration { get; private set; }
    protected T[] Population { get; private set; }

    public abstract void Evolve();

    protected void UpdatePopulation(T[] newPopulation)
    {
        Population = newPopulation;
        CurrentGeneration++;
    }

    protected void MutateEach(int points)
    {
        foreach (var specimen in Population)
        {
            specimen.Mutate(points);
        }
    }

    public double GetCurrentAverageFitness()
    {
        return Population.Average(specimen => specimen.GetFitness());
    }

    public double GetCurrentBestFitness()
    {
        var population = Population.ToList();
        population.Sort(_compareFitness);

        return population.First().GetFitness();
    }

    protected T EliteHotDeckSelection()
    {
        var population = Population.ToList();
        population.Sort(_compareFitness);

        return (T)population.First().Clone();
    }

    protected T TournamentSelection(int tournamentSize)
    {
        var random = new Random();
        var tournament = new List<T>();

        for (var i = 0; i < tournamentSize; i++)
        {
            tournament.Add(Population[random.Next(Population.Length)]);
        }

        tournament.Sort(_compareFitness);

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