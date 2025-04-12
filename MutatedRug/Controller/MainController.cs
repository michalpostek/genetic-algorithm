using EvolutionChart;
using MutatedRug.View;

namespace MutatedRug.Controller;

public partial class MainController : Form
{
    private new const int Width = 1200;
    private new const int Height = 800;
    private readonly EvolutionChart.EvolutionChart _evolutionChart;

    private readonly FormView _formView;

    public MainController()
    {
        _formView = new FormView(HandleSubmit);
        _evolutionChart = new EvolutionChart.EvolutionChart(HandleClear, 0.1, 2);

        Controls.Add(_formView.Form);
        Controls.Add(_evolutionChart.ChartContainer);

        InitializeComponent();

        Size = new Size(Width, Height);
    }

    private void HandleClear()
    {
        ToggleVisibility(true);
    }

    private void ToggleVisibility(bool displayForm)
    {
        _formView.Form.Visible = displayForm;
        _evolutionChart.ChartContainer.Visible = !displayForm;
    }

    private void HandleSubmit(int generations, int populationSize)
    {
        var mutatedRug = new Model.MutatedRug(populationSize);
        var data = new GenerationStats[generations + 1];

        data[0] = new GenerationStats(mutatedRug.GetCurrentAverageFitness(), mutatedRug.GetCurrentBestFitness());

        for (var i = 0; i < generations; i++)
        {
            mutatedRug.Evolve();
            data[i + 1] = new GenerationStats(mutatedRug.GetCurrentAverageFitness(), mutatedRug.GetCurrentBestFitness());
        }

        _evolutionChart.Update(data);
        ToggleVisibility(false);
    }
}