using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;
using Sinusie.Model;

namespace Sinusie.View;

public delegate void OnClear();

public class ResultView : Form
{
    private readonly Chart _chart;
    private readonly Button _clearButton;
    private readonly OnClear _onClear;

    public ResultView(OnClear onClear)
    {
        _onClear = onClear;

        Width = 1200;
        Height = 800;

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
        var axisXInterval = Math.Floor(data.Length / 10f);

        _chart.Series.Clear();
        _chart.ChartAreas[0].AxisX.Interval = axisXInterval;
        _chart.ChartAreas[0].AxisX.IntervalOffset = data.Length % 10;

        var avgFitnessSeries = new Series
        {
            Name = "Average fitness",
            ChartType = SeriesChartType.Line,
            Color = Color.SlateGray,
            BorderWidth = 3,
            IsVisibleInLegend = true,
            Font = new Font("Arial", 10, FontStyle.Bold),
            LabelForeColor = Color.SlateGray,
            LabelBackColor = Color.FromArgb(128, 255, 255, 255)
        };

        var bestFitnessSeries = new Series
        {
            Name = "Best fitness",
            ChartType = SeriesChartType.Line,
            Color = Color.DarkGoldenrod,
            BorderWidth = 3,
            IsVisibleInLegend = true,
            Font = new Font("Arial", 10, FontStyle.Bold),
            LabelForeColor = Color.DarkGoldenrod,
            LabelBackColor = Color.FromArgb(128, 255, 255, 255)
        };

        for (var i = 0; i < data.Length; i++)
        {
            var avg = data[i].AverageFitness;
            var best = data[i].BestFitness;

            avgFitnessSeries.Points.AddXY(i, avg);
            bestFitnessSeries.Points.AddXY(i, best);

            if (i % axisXInterval == 0) avgFitnessSeries.Points[i].Label = Math.Round(avg, 2).ToString(CultureInfo.InvariantCulture);

            if ((i - Math.Floor(axisXInterval / 2)) % axisXInterval == 0)
                bestFitnessSeries.Points[i].Label = Math.Round(best, 2).ToString(CultureInfo.InvariantCulture);
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
        chartArea.AxisX.Minimum = 0;
        chartArea.AxisX.Title = "Generation";
        chartArea.AxisX.MajorGrid.LineColor = Color.Gray;

        chartArea.AxisY.Interval = 5;
        chartArea.AxisY.Title = "Fitness";

        var legend = new Legend();

        var chart = new Chart
        {
            Size = new Size(Width, Height - 200)
        };

        chart.ChartAreas.Add(chartArea);
        chart.Legends.Add(legend);

        return chart;
    }
}