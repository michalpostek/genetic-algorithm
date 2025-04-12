using XOR.Model;
using XOR.View;
using GenerationStats = EvolutionChart.GenerationStats;

namespace XOR.Controller;

public partial class MainController : Form
{
    private new const int Width = 1200;
    private new const int Height = 800;
    private readonly EvolutionChart.EvolutionChart _evolutionChart;

    private readonly FormView _formView;

    public MainController()
    {
        _formView = new FormView(HandleSubmit);
        _evolutionChart = new EvolutionChart.EvolutionChart(HandleClear, 0.05, 6);

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

    private void HandleSubmit(int generations)
    {
        var xor = new Xor();
        var data = new GenerationStats[generations + 1];

        data[0] = new GenerationStats(xor.GetCurrentAverageFitness(), xor.GetCurrentBestFitness());

        for (var i = 0; i < generations; i++)
        {
            xor.Evolve();
            data[i + 1] = new GenerationStats(xor.GetCurrentAverageFitness(), xor.GetCurrentBestFitness());
        }

        _evolutionChart.Update(data);
        ToggleVisibility(false);
    }
}