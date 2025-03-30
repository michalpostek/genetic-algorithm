namespace GeneticAlgorithm;

public abstract class GeneticAlgorithm<T> where T : ISpecimen, new()
{
    private readonly int _populationSize;

    protected GeneticAlgorithm(int populationSize)
    {
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

    public abstract double GetCurrentBestFitness();

    protected T EliteHotDeckSelection(Comparison<T> compareFitness)
    {
        var population = Population.ToList();
        population.Sort(compareFitness);

        return (T)population.First().Clone();
    }

    protected T TournamentSelection(int tournamentSize, Comparison<T> compareFitness)
    {
        var random = new Random();
        var tournament = new List<T>();

        for (var i = 0; i < tournamentSize; i++)
        {
            tournament.Add(Population[random.Next(Population.Length)]);
        }

        tournament.Sort(compareFitness);

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