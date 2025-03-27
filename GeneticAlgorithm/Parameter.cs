namespace GeneticAlgorithm;

public class Parameter
{
    private readonly Dictionary<string, double> _valueLookup;
    public bool[] Chromosomes { get; }
    
    public Parameter(int min, int max, int length)
    {
        _valueLookup = CreateValueLookup(min, max, length);
        Chromosomes = GenerateRandomChromosomes(length);
    }

    public Parameter(int min, int max, bool[] chromosomes)
    {
        _valueLookup = CreateValueLookup(min, max, chromosomes.Length);
        Chromosomes = chromosomes;
    }

    public double GetValue()
    {
        return _valueLookup[StringifyChromosomes(Chromosomes)];
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

    private static string StringifyChromosomes(bool[] chromosomes)
    {
        var chars = new char[chromosomes.Length];

        for (var i = 0; i < chromosomes.Length; i++) chars[i] = chromosomes[i] ? '1' : '0';

        return new string(chars);
    }

    private static Dictionary<string, double> CreateValueLookup(int min, int max, int chromosomes)
    {
        var lookup = new Dictionary<string, double>();
        var entries = (int)Math.Pow(2, chromosomes);
        var diff = (max - min) / (double)(entries - 1);

        for (var i = 0; i < entries; i++)
        {
            var key = Convert.ToString(i, 2).PadLeft(chromosomes, '0');
            lookup.Add(key, min + (diff * i));
        }

        return lookup;
    }
}