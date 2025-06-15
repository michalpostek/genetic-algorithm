using GeneticAlgorithm;
using MutatedRug.Controller;

namespace MutatedRug;

internal static class Program
{
    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Population._random = new Random(DateTime.Now.Millisecond);
        Application.Run(new MainController());
    }
}