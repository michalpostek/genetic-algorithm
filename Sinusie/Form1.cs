namespace Sinusie;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        var sinusie = new Model.Sinusie(13);

        Console.WriteLine("Best " + sinusie.GetCurrentBestFitness() + " | Avg " + sinusie.GetCurrentAverageFitness());

        for (var i = 0; i < 100; i++)
        {
            sinusie.Evolve();
            Console.WriteLine("Best " + sinusie.GetCurrentBestFitness() + " | Avg " + sinusie.GetCurrentAverageFitness());
        }
    }
}