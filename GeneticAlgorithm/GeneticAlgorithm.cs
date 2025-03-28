namespace GeneticAlgorithm;

public abstract class GeneticAlgorithm<T> where T : ISpecimen, new()
{
    private readonly int _populationSize;
    protected int CurrentGeneration { get; private set; }
    protected T[] Population { get; private set; }

    protected GeneticAlgorithm(int populationSize)
    {
        _populationSize = populationSize;
        Population = InitPopulation();
    }

    public abstract void Evolve(int generations);

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

    private double GetCurrentAverageFitness()
    {
        return Population.Average(specimen => specimen.GetFitness());
    }

    private double GetCurrentBestFitness()
    {
        return Population.Max(specimen => specimen.GetFitness());
    }

    protected T EliteHotDeckSelection()
    {
        return (T)Population.OrderByDescending(specimen => specimen.GetFitness()).First().Clone();
    }

    protected T TournamentSelection(int tournamentSize)
    {
        var random = new Random();
        var tournament = new T[tournamentSize];

        for (var i = 0; i < tournamentSize; i++)
        {
            tournament[i] = Population[random.Next(Population.Length)];
        }

        return (T)tournament.OrderByDescending(specimen => specimen.GetFitness()).First().Clone();
    }
    
    protected void LogCurrentPopulation()
    {
        Console.WriteLine(
            "Generation: " + CurrentGeneration +
            " | Best: " + Math.Round(GetCurrentBestFitness(), 2) +
            " | Avg: " + Math.Round(GetCurrentAverageFitness(), 2)
        );
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