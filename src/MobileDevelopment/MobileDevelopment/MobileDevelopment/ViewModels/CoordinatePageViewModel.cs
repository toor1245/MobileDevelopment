using MobileDevelopment.Extensions;
using MobileDevelopment.Models;
using Xamarin.Forms;

namespace MobileDevelopment.ViewModels
{
    public class CoordinatePageViewModel : BaseViewModel
    {
        private const string DegreeDefault = "0°0'0\"S";
        private const string DecimalDegreeDefault = "00.0000 S";
        
        private string _input = DegreeDefault;
        private string _coordinateAInput = DegreeDefault;
        private string _coordinateBInput = DegreeDefault;
        private string _decimalDegree = DecimalDegreeDefault;
        private string _result = DecimalDegreeDefault;
        
        public CoordinatePageViewModel()
        {
            Title = "Coordinate";
            ConvertCommand = new Command(Convert);
            CalculateCommand = new Command(Calculate);
            PropertyChanged += (_, _) => ConvertCommand.ChangeCanExecute();
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
        
        public string CoordinateA
        {
            get => _coordinateAInput;
            set => SetProperty(ref _coordinateAInput, value);
        }

        public string CoordinateB
        {
            get => _coordinateBInput;
            set => SetProperty(ref _coordinateBInput, value);
        }
        
        public string Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        public Command ConvertCommand { get; }

        public Command CalculateCommand { get; }

        public void Convert()
        {
            var coordinate = new CoordinateMh(_input);
            Input = coordinate.GetDegreeMinuteSecondFormat();
            DecimalDegree = coordinate.GetDecimalDegreeFormat();
        }

        public void Calculate()
        {
            var coordinateA = new CoordinateMh(_coordinateAInput);
            var coordinateB = new CoordinateMh(_coordinateBInput);
            Result = (coordinateA - coordinateB).GetDegreeMinuteSecondFormat();
        }
    }
}