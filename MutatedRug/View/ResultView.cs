using System.Windows.Forms.DataVisualization.Charting;
using MutatedRug.Model;

namespace MutatedRug.View;

public delegate void OnClear();

public partial class ResultView : Form
{
    private readonly Chart _chart;
    private readonly Button _clearButton;
    private readonly OnClear _onClear;

    public ResultView(OnClear onClear)
    {
        _onClear = onClear;

        ChartContainer = new FlowLayoutPanel
        {
            Size = new Size(Width, Height - 200),
            WrapContents = true,
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown
        };

        _chart = CreateChart();
        _chart.Width = 800;

        _clearButton = new Button
        {
            Text = "Clear",
            AutoSize = true
        };
        _clearButton.Click += OnClear;

        ChartContainer.Controls.Add(_chart);
        ChartContainer.Controls.Add(_clearButton);
    }

    public FlowLayoutPanel ChartContainer { get; }

    public void UpdateChart(GenerationStats[] data)
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

    private Chart CreateChart()
    {
        var chartArea = new ChartArea();
        chartArea.AxisX.Interval = 1;
        chartArea.AxisY.Interval = 0.1;
        chartArea.AxisX.Minimum = 0;
        chartArea.AxisX.Title = "Generation";
        chartArea.AxisY.Title = "Fitness";

        var legend = new Legend();

        var chart = new Chart
        {
            Size = new Size(Width, Height),
            Dock = DockStyle.Fill,
            AutoSize = true
        };

        chart.ChartAreas.Add(chartArea);
        chart.Legends.Add(legend);

        return chart;
    }
}