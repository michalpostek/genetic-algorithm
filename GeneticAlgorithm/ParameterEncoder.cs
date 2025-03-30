namespace GeneticAlgorithm;

public class ParameterEncoder(int min, int max, int length)
{
    private readonly Dictionary<string, double> _valueLookup = CreateValueLookup(min, max, length);    
    
    public double GetValue(string chromosomes)
    {
        return _valueLookup[chromosomes];
    }
    
    public double GetValue(bool[] chromosomes)
    {
        return _valueLookup[StringifyChromosomes(chromosomes)];
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