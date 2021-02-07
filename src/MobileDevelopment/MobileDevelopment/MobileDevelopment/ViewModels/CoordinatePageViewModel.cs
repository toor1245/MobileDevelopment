using MobileDevelopment.Extensions;
using MobileDevelopment.Models;
using Xamarin.Forms;

namespace MobileDevelopment.ViewModels
{
    public class CoordinatePageViewModel : BaseViewModel
    {
        private string _input = "0°0'0\"S";
        private string _decimalDegree = "00.0S";
        
        public CoordinatePageViewModel()
        {
            Title = "Coordinate";
            ConvertCommand = new Command(Convert);
            PropertyChanged +=
                (_, _) => ConvertCommand.ChangeCanExecute();
        }

        public string Input
        {
            get => _input;
            set => SetProperty(ref _input, value);
        }

        public string DecimalDegree
        {
            get => _decimalDegree;
            set => SetProperty(ref _decimalDegree, value);
        }

        public Command ConvertCommand { get; }

        public void Convert()
        {
            var coordinate = new CoordinateMh(_input);
            Input = coordinate.GetDegreeMinuteSecondFormat();
            DecimalDegree = coordinate.GetDecimalDegreeFormat();
        }
    }
}