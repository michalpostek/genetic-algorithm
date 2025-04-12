namespace EvolutionChart;

public struct GenerationStats(double bestFitness, double averageFitness)
{
    public readonly double BestFitness = bestFitness;
    public readonly double AverageFitness = averageFitness;
}