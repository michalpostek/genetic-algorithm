namespace GeneticAlgorithm;

/// <summary>
///     Represents an individual in a genetic algorithm.
///     Provides mutation and crossover methods to modify its chromosomes and create new individuals
/// </summary>
public class Individual
{
    public Individual(int length)
    {
        Chromosomes = GenerateRandomChromosomes(length);
    }

    private Individual(bool[] chromosomes)
    {
        Chromosomes = chromosomes;
    }

    public bool[] Chromosomes { get; }

    public Individual Clone()
    {
        return new Individual(Chromosomes.ToArray());
    }

    public void FlipBit(int bits)
    {
        var random = new Random();

        for (var i = 0; i < bits; i++)
        {
            var index = random.Next(0, Chromosomes.Length);

            Chromosomes[index] = !Chromosomes[index];
        }
    }

    public static Tuple<Individual, Individual> Crossover(Individual parent1, Individual parent2)
    {
        var random = new Random();
        var child1 = new bool[parent1.Chromosomes.Length];
        var child2 = new bool[parent1.Chromosomes.Length];
        var crossoverPoint = random.Next(0, parent1.Chromosomes.Length);

        for (var i = 0; i < crossoverPoint; i++)
        {
            child1[i] = parent1.Chromosomes[i];
            child2[i] = parent2.Chromosomes[i];
        }

        for (var i = crossoverPoint; i < parent2.Chromosomes.Length; i++)
        {
            child1[i] = parent2.Chromosomes[i];
            child2[i] = parent1.Chromosomes[i];
        }

        return new Tuple<Individual, Individual>(new Individual(child1), new Individual(child2));
    }

    private static bool[] GenerateRandomChromosomes(int chromosomes)
    {
        var random = new Random();
        var param = new bool[chromosomes];

        for (var i = 0; i < chromosomes; i++)
        {
            param[i] = random.Next(2) == 0;
        }

        return param;
    }
}
