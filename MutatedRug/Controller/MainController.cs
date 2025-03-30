using MutatedRug.View;

namespace MutatedRug.Controller;

public partial class MainController : Form
{
    private new const int Width = 1000;
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

    private void HandleSubmit(int generations, int populationSize)
    {
        var mutatedRug = new Model.MutatedRug(populationSize);
        var data = new GenerationData[generations + 1];

        data[0] = new GenerationData(mutatedRug.GetCurrentAverageFitness(), mutatedRug.GetCurrentBestFitness());

        for (var i = 0; i < generations; i++)
        {
            mutatedRug.Evolve();
            data[i + 1] = new GenerationData(mutatedRug.GetCurrentAverageFitness(), mutatedRug.GetCurrentBestFitness());
        }

        _resultView.UpdateChart(data);
        ToggleVisibility(false);
    }
}