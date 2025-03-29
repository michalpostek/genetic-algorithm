﻿namespace GeneticAlgorithm;

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

    public abstract GenerationData[] Evolve(int generations);

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

    protected double GetCurrentAverageFitness()
    {
        return Population.Average(specimen => specimen.GetFitness());
    }

    protected double GetCurrentBestFitness()
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