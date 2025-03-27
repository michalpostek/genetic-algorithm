namespace GeneticAlgorithm;

public abstract class GeneticAlgorithm<T> where T : ISpecimen, new()
{
    private readonly int _populationSize;
    protected int CurrentGeneration { get; private set; }
    protected List<T> Population { get; private set; }

    protected GeneticAlgorithm(int populationSize)
    {
        _populationSize = populationSize;
        Population = InitPopulation();
    }

    public abstract void Evolve(int maxGenerations);

    protected void UpdatePopulation(List<T> newPopulation)
    {
        Population = newPopulation;
        CurrentGeneration++;
    }

    protected void MutateEach(int points)
    {
        Population.ForEach(specimen => specimen.Mutate(points));
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
        var tournament = new List<T>();

        for (var i = 0; i < tournamentSize; i++)
            tournament.Add(Population[random.Next(Population.Count)]);

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

    private List<T> InitPopulation()
    {
        var population = new List<T>();

        for (var i = 0; i < _populationSize; i++) population.Add(new T());

        return population;
    }
}