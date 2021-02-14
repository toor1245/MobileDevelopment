using System;
using Microcharts;
using MobileDevelopment.Interfaces;
using OxyPlot;
using OxyPlot.Series;
using SkiaSharp;

namespace MobileDevelopment.Services
{
    public class ChartStore : IChartStore
    {
        public Chart CreateBarChart()
        {
            return new PieChart
            {
                Entries = new[]
                {
                    new ChartEntry(25)
                    {
                        Color = SKColors.Yellow
                    },
                    new ChartEntry(25)
                    {
                        Color = SKColors.Green
                    },
                    new ChartEntry(45)
                    {
                        Color = SKColors.LightBlue
                    },
                    new ChartEntry(5)
                    {
                        Color = SKColors.Violet,
                    }
                },
                Margin = 5f,
                LabelTextSize = 30,
                LabelMode = LabelMode.None,
                HoleRadius = 0.5f,
            };
        }

        public PlotModel CreatePlotModel()
        {
            var model = new PlotModel { Title = "Plot" };
            model.Series.Add(new FunctionSeries(Math.Cos, -Math.PI, Math.PI, 0.1, "cos(x)"));
            return model;
        }
    }
}