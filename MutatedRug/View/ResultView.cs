using System.Windows.Forms.DataVisualization.Charting;

namespace MutatedRug.View;

public delegate void OnClear();

public partial class ResultView : Form
{
    private readonly Chart _chart;
    private readonly Button _clearButton;
    private readonly OnClear _onClear;

    public ResultView(OnClear onClear)
    {
        ChartContainer = CreateChartContainer();

        _chart = CreateChart();
        _chart.Width = 800;

        _clearButton = CreateClearButton();
        _clearButton.Click += OnClear;

        _onClear = onClear;

        ChartContainer.Controls.Add(_chart);
        ChartContainer.Controls.Add(_clearButton);
    }

    public FlowLayoutPanel ChartContainer { get; }

    public void UpdateChart(GenerationData[] data)
    {
        _chart.Series.Clear();

        var avgFitnessSeries = new Series
        {
            Name = "Average fitness",
            ChartType = SeriesChartType.FastLine,
            Color = Color.SlateGray,
            BorderWidth = 3,
            IsVisibleInLegend = true
        };

        var bestFitnessSeries = new Series
        {
            Name = "Best fitness",
            ChartType = SeriesChartType.FastLine,
            Color = Color.DarkGoldenrod,
            BorderWidth = 3,
            IsVisibleInLegend = true
        };

        for (var i = 0; i < data.Length; i++)
        {
            avgFitnessSeries.Points.AddXY(i, data[i].AverageFitness);
            bestFitnessSeries.Points.AddXY(i, data[i].BestFitness);
        }

        _chart.Series.Add(avgFitnessSeries);
        _chart.Series.Add(bestFitnessSeries);
    }

    private void OnClear(object? sender, EventArgs e)
    {
        _onClear();
    }

    private Button CreateClearButton()
    {
        var clearButton = new Button();
        clearButton.Text = "Clear";
        clearButton.AutoSize = true;

        return clearButton;
    }

    private Chart CreateChart()
    {
        var chartArea = new ChartArea();
        chartArea.AxisX.Interval = 1;
        chartArea.AxisY.Interval = 0.1;
        chartArea.AxisX.Minimum = 0;
        chartArea.AxisX.Title = "Generation";
        chartArea.AxisY.Title = "Fitness";

        var legend = new Legend();

        var chart = new Chart();
        chart.Size = new Size(Width, Height);
        chart.Dock = DockStyle.Fill;
        chart.AutoSize = true;
        chart.ChartAreas.Add(chartArea);
        chart.Legends.Add(legend);

        return chart;
    }

    private FlowLayoutPanel CreateChartContainer()
    {
        var panel = new FlowLayoutPanel();
        panel.Size = new Size(Width, Height - 200);

        panel.WrapContents = true;
        panel.Dock = DockStyle.Fill;
        panel.FlowDirection = FlowDirection.TopDown;

        return panel;
    }
}