namespace GeneticAlgorithm;

public class Genome
{
    public bool[] Chromosomes { get; }
    
    public Genome(int length)
    {
        Chromosomes = GenerateRandomChromosomes(length);
    }

    public Genome(bool[] chromosomes)
    {
        Chromosomes = chromosomes;
    }
    
    public void Mutate(int points)
    {
        var random = new Random();

        for (var i = 0; i < points; i++)
        {
            var index = random.Next(0, Chromosomes.Length);

            Chromosomes[index] = !Chromosomes[index];
        }
    }
    
    public static Tuple<Genome, Genome> Crossover(Genome parent1, Genome parent2)
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

        return new Tuple<Genome, Genome>(new Genome(child1), new Genome(child2));
    } 

    private static bool[] GenerateRandomChromosomes(int chromosomes)
    {
        var random = new Random();
        var param = new bool[chromosomes];

        for (var i = 0; i < chromosomes; i++) param[i] = random.Next(2) == 0;

        return param;
    }
}