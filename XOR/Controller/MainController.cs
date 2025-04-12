using XOR.Model;
using XOR.View;

namespace XOR.Controller;

public partial class MainController : Form
{
    private new const int Width = 1200;
    private new const int Height = 800;

    private readonly FormView _formView;
    private readonly ResultView _resultView;

    public MainController()
    {
        _formView = new FormView(HandleSubmit);
        _resultView = new ResultView(HandleClear);

        Controls.Add(_formView.Form);
        Controls.Add(_resultView.ChartContainer);

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
        _resultView.ChartContainer.Visible = !displayForm;
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

        _resultView.UpdateChart(data);
        ToggleVisibility(false);
    }
}