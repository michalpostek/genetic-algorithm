namespace GeneticAlgorithm;

/// <summary>
///     Encodes a chromosomes (represented as a binary string) into a value between <c>min</c> and <c>max</c>).
///     The binary values are evenly distributed between the minimum and maximum values.
/// </summary>
public class ParameterEncoder
{
    private readonly Dictionary<string, double> _lazyValueLookup;
    private readonly int _min;
    private readonly double _valueStep;

    public ParameterEncoder(int min, int max, int length)
    {
        var entries = (int)Math.Pow(2, length);

        _min = min;
        _valueStep = (max - min) / (double)(entries - 1);
        _lazyValueLookup = new Dictionary<string, double>();
    }

    public double GetValue(bool[] chromosomes)
    {
        var stringifiedChromosomes = StringifyChromosomes(chromosomes);

        if (_lazyValueLookup.TryGetValue(stringifiedChromosomes, out var cachedValue))
        {
            return cachedValue;
        }

        var offset = Convert.ToInt32(stringifiedChromosomes, 2);
        var value = _min + offset * _valueStep;

        _lazyValueLookup[stringifiedChromosomes] = value;

        return value;
    }

    private static string StringifyChromosomes(bool[] chromosomes)
    {
        var chars = new char[chromosomes.Length];

        for (var i = 0; i < chromosomes.Length; i++)
        {
            chars[i] = chromosomes[i] ? '1' : '0';
        }

        return new string(chars);
    }
}
