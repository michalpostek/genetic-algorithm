namespace XOR.Model;

internal record XorInput(bool X, bool Y);

public static class FitnessCalculator
{
    private static readonly Dictionary<XorInput, bool> XorGateTruthTable = new()
    {
        { new XorInput(false, false), false },
        { new XorInput(true, false), true },
        { new XorInput(false, true), true },
        { new XorInput(true, true), false }
    };

    public static double Calculate(double[] parameterValues)
    {
        return XorGateTruthTable.Keys.Aggregate(0d, (sum, xorInput) =>
        {
            var hiddenLayer = new[]
            {
                Neuron.GetValue(
                    [(Convert.ToDouble(xorInput.X), parameterValues[0]), (Convert.ToDouble(xorInput.Y), parameterValues[1])],
                    parameterValues[2],
                    Neuron.Sigmoid
                ),
                Neuron.GetValue(
                    [(Convert.ToDouble(xorInput.X), parameterValues[3]), (Convert.ToDouble(xorInput.Y), parameterValues[4])],
                    parameterValues[5],
                    Neuron.Sigmoid
                )
            };

            var result = Neuron.GetValue(
                [(hiddenLayer[0], parameterValues[6]), (hiddenLayer[1], parameterValues[7])],
                parameterValues[8],
                Neuron.Sigmoid
            );

            return sum + Math.Pow(Convert.ToDouble(XorGateTruthTable[xorInput]) - result, 2);
        });
    }
}