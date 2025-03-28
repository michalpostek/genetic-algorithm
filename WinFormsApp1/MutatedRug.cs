namespace WinFormsApp1;

public class MutatedRug(int populationSize) : GeneticAlgorithm.GeneticAlgorithm<MySpecimen>(populationSize)
{
    private readonly int _tournamentSize = (int)Math.Ceiling((double)populationSize / 10);
    
    public override void Evolve(int generations)
    {
        LogCurrentPopulation();
        
        while (CurrentGeneration < generations)
        {
            var newPopulation = new MySpecimen[Population.Length];

            for (var i = 0; i < Population.Length - 1; i++)
            {
                newPopulation[i] = TournamentSelection(_tournamentSize);
            }

            MutateEach(1);
            newPopulation[Population.Length - 1] = EliteHotDeckSelection();
            UpdatePopulation(newPopulation);
            LogCurrentPopulation();
        }
    }
}