﻿namespace Sinusie.Model;

public static class FitnessCalculator
{
    private static readonly Dictionary<double, double> TargetFnData = InitTargetFnData();

    public static double CalculateFitness(double pA, double pB, double pC)
    {
        return TargetFnData.Keys.Aggregate(0d, (sum, x) =>
        {
            var y = pA * Math.Sin(pB * x + pC);

            return sum += Math.Pow(TargetFnData[x] - y, 2);
        });
    }

    private static Dictionary<double, double> InitTargetFnData()
    {
        return new Dictionary<double, double>
        {
            { -1.00000, 0.59554 },
            { -0.80000, 0.58813 },
            { -0.60000, 0.64181 },
            { -0.40000, 0.68587 },
            { -0.20000, 0.44783 },
            { 0.00000, 0.40836 },
            { 0.20000, 0.38241 },
            { 0.40000, 0.05933 },
            { 0.60000, -0.12478 },
            { 0.80000, -0.36487 },
            { 1.00000, -0.39935 },
            { 1.20000, -0.50881 },
            { 1.40000, -0.63435 },
            { 1.60000, -0.59979 },
            { 1.80000, -0.64107 },
            { 2.00000, -0.51808 },
            { 2.20000, -0.38127 },
            { 2.40000, -0.12349 },
            { 2.60000, -0.09624 },
            { 2.80000, 0.27893 },
            { 3.00000, 0.48965 },
            { 3.20000, 0.33089 },
            { 3.40000, 0.70615 },
            { 3.60000, 0.53342 },
            { 3.80000, 0.43321 },
            { 4.00000, 0.64790 },
            { 4.20000, 0.48834 },
            { 4.40000, 0.18440 },
            { 4.60000, -0.23890 },
            { 4.80000, -0.10261 },
            { 5.00000, -0.33594 },
            { 5.20000, -0.35101 },
            { 5.40000, -0.62027 },
            { 5.60000, -0.55719 },
            { 5.80000, -0.66377 },
            { 6.00000, -0.62740 }
        };
    }
}