namespace XOR.Model;

public delegate double ActivationFunction(double x);

public static class Neuron
{
    public static ActivationFunction Sigmoid => value => 1d / (1d + Math.Exp(-value));

    public static double GetValue((double x1, double w1)[] inputs, double bias, ActivationFunction activationFunction)
    {
        var weightedInputs = inputs.Aggregate(0d, (sum, curr) => sum + curr.x1 * curr.w1);

        return activationFunction(weightedInputs + bias);
    }
}