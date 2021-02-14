using System.Windows.Input;
using Microcharts;
using MobileDevelopment.Interfaces;
using OxyPlot;
using Xamarin.Forms;

namespace MobileDevelopment.ViewModels
{
    public class ChartPageViewModel : BaseViewModel
    {
        private Chart _barChart;
        private bool _isVisible = true;
        private bool _isVisiblePlot;
        private bool _isToggled;
        
        public ChartPageViewModel()
        {
            var chartStore = DependencyService.Get<IChartStore>();
            Title = "Chart";
            ToggledCommand = new Command(SwitchPage);
            Model = chartStore.CreatePlotModel();
            
            _barChart = chartStore.CreateBarChart();
        }
        
        public Chart Chart
        {
            get => _barChart;
            set => SetProperty(ref _barChart, value);
        }

        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }
        
        public bool IsVisiblePlot
        {
            get => _isVisiblePlot;
            set => SetProperty(ref _isVisiblePlot, value);
        }

        public bool IsToggled
        {
            get => _isToggled;
            set => SetProperty(ref _isToggled, value);
        }

        public PlotModel Model { get; set; }

        public ICommand ToggledCommand { get; }

        public void SwitchPage()
        {
            IsVisiblePlot = !IsVisiblePlot;
            IsVisible = !IsToggled;
        }
    }
}