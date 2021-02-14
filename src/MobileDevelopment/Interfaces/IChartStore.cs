using Microcharts;
using OxyPlot;

namespace MobileDevelopment.Interfaces
{
    public interface IChartStore
    {
        Chart CreateBarChart();
        PlotModel CreatePlotModel();
    }
}