using MobileDevelopment.Models;

namespace MobileDevelopment.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        #region .fields
        
        private static readonly Student Student;
        private string _name = $"Student: {Student.FirstName} {Student.LastName}";
        private string _group = $"Group: {Student.Group}";
        private string _recordBook = $"{Student.RecordBook}";
        
        #endregion

        #region .ctors
        
        static HomePageViewModel() => Student = new Student();
        public HomePageViewModel() => Title = "Home";
        
        #endregion

        #region .props
        
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        
        public string Group
        {
            get => _group;
            set => SetProperty(ref _group, value);
        }
        
        public string RecordBook
        {
            get => _recordBook;
            set => SetProperty(ref _recordBook, value);
        }
        
        #endregion
    }
}
