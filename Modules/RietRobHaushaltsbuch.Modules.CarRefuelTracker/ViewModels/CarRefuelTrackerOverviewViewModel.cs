using System.Collections.ObjectModel;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;
using RietRobHaushaltsbuch.Modules.CarRefuelTracker.DataAccess;
using RietRobHaushaltsbuch.Modules.CarRefuelTracker.Models;

namespace RietRobHaushaltsbuch.Modules.CarRefuelTracker.ViewModels
{
    public class CarRefuelTrackerOverviewViewModel : BindableBase
    {
        #region Fields
        private string _title;
        private CarModel _selectedCarModel;
        private ObservableCollection<CarModel> _availableCars;
        private bool _isCarModelSelected = false;

        #endregion

        #region Properties
        public ObservableCollection<CarModel> AvailableCars
        {
            get { return _availableCars; }
            set { SetProperty(ref _availableCars, value); }
        }
        public CarModel SelectedCarModel
        {
            get { return _selectedCarModel; }
            set { SetProperty(ref _selectedCarModel, value); }
        }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public bool IsCarModelSelected
        {
            get { return _isCarModelSelected; }
            set { SetProperty(ref _isCarModelSelected, value); }
        }

        #endregion

        #region Contstructor

        public CarRefuelTrackerOverviewViewModel()
        {
            Title = "CarRefuelTracker Overview";
            AvailableCars = new ObservableCollection<CarModel>(SQLiteDataAccess.LoadCars());
            RegisterCommands();
        }

        private void RegisterCommands()
        {
            CreateCarCommand = new DelegateCommand(CreateCar);
            SelectionChangedCommand = new DelegateCommand(CarSelectionChanged);
            EditCarCommand = new DelegateCommand(EditCar).ObservesProperty(() => IsCarModelSelected);
        }

        private void CarSelectionChanged()
        {
            if (SelectedCarModel != null)
            {
                IsCarModelSelected = true;
            }
            else
            {
                IsCarModelSelected = false;
            }
        }

        private void EditCar()
        {
            MessageBox.Show($@"{SelectedCarModel.Brand.BrandName} {SelectedCarModel.ModelType.ModelName} will be edited");
        }

        private void CreateCar()
        {
            MessageBox.Show($@"CreateCarWindow opens");
        }

        #endregion

        #region Commands

        public DelegateCommand CreateCarCommand { get; set; }
        public DelegateCommand EditCarCommand { get; set; }
        public DelegateCommand SelectionChangedCommand { get; set; }

        #endregion
    }
}
