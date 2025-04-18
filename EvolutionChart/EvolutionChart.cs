﻿using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;

namespace EvolutionChart;

public delegate void OnClear();

public partial class EvolutionChart : Form
{
    private readonly Chart _chart;
    private readonly int _chartHeight;
    private readonly int _chartWidth;
    private readonly Button _clearButton;

    private readonly double _fitnessAxisInterval;
    private readonly int _fitnessPrecision;
    private readonly OnClear _onClear;

    public EvolutionChart(OnClear onClear, double fitnessAxisInterval, int fitnessPrecision)
    {
        _chartWidth = 1200;
        _chartHeight = 600;
        _onClear = onClear;
        _fitnessAxisInterval = fitnessAxisInterval;
        _fitnessPrecision = fitnessPrecision;

        ChartContainer = new FlowLayoutPanel
        {
            Size = new Size(_chartWidth, _chartHeight),
            WrapContents = true,
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown
        };

        _chart = CreateChart();

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

    public void Update(GenerationStats[] data)
    {
        var generationAxisLabelInterval = Math.Floor(data.Length / 10f);

        _chart.ChartAreas.First().AxisX.Interval = generationAxisLabelInterval;
        _chart.Series.Clear();

        var avgFitnessSeries = new Series
        {
            Name = "Average fitness",
            ChartType = SeriesChartType.Line,
            Color = Color.SlateGray,
            BorderWidth = 2,
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
            BorderWidth = 2,
            IsVisibleInLegend = true,
            Font = new Font("Arial", 10, FontStyle.Bold),
            LabelForeColor = Color.DarkGoldenrod,
            LabelBackColor = Color.FromArgb(128, 255, 255, 255)
        };

        for (var i = 0; i < data.Length; i++)
        {
            avgFitnessSeries.Points.AddXY(i, data[i].AverageFitness);
            bestFitnessSeries.Points.AddXY(i, data[i].BestFitness);

            if (i % generationAxisLabelInterval == 0)
            {
                avgFitnessSeries.Points[i].Label = Math.Round(data[i].AverageFitness, _fitnessPrecision).ToString(CultureInfo.InvariantCulture);
            }

            if ((i - Math.Floor(generationAxisLabelInterval / 2)) % generationAxisLabelInterval == 0)
            {
                bestFitnessSeries.Points[i].Label = Math.Round(data[i].BestFitness, _fitnessPrecision).ToString(CultureInfo.InvariantCulture);
            }
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

        chartArea.AxisY.Interval = _fitnessAxisInterval;
        chartArea.AxisX.Title = "Generation";
        chartArea.AxisY.Title = "Fitness";

        var legend = new Legend();

        var chart = new Chart
        {
            Size = new Size(_chartWidth, _chartHeight),
            Dock = DockStyle.Fill,
            AutoSize = true
        };

        chart.ChartAreas.Add(chartArea);
        chart.Legends.Add(legend);

        return chart;
    }
}