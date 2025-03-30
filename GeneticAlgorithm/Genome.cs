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

    private static bool[] GenerateRandomChromosomes(int chromosomes)
    {
        var random = new Random();
        var param = new bool[chromosomes];

        for (var i = 0; i < chromosomes; i++) param[i] = random.Next(2) == 0;

        return param;
    }
}