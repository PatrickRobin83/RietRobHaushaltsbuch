using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Mvvm;
using RietRobHaushaltbuch.Core.Base;
using RietRobHaushaltbuch.Core.DataAccess;
using RietRobHaushaltbuch.Core.Interfaces;
using RietRobHaushaltbuch.Core.Models;

namespace RietRobHaushaltsbuch.Modules.OverView.ViewModels
{
    public class OverViewModel : ViewModelBase, IViewModelHelper
    {
        private string _headLine;
        private ObservableCollection<CarModel> _availableCars;
        private CarModel _selectedCar;


        public CarModel SelectedCar
        {
            get { return _selectedCar; }
            set { SetProperty(ref _selectedCar, value); }
        }

        public ObservableCollection<CarModel> AvailableCars
        {
            get { return _availableCars; }
            set { SetProperty(ref _availableCars, value); }
        }

        public string HeadLine
        {
            get { return _headLine; }
            set { SetProperty(ref _headLine, value); }
        }

        public OverViewModel()
        {
            HeadLine = "Übersicht der Gesamtkosten";
            RegisterCommands();
            InitializeViewModel();
        }

        private void InitializeViewModel()
        {
            AvailableCars = new ObservableCollection<CarModel>(SqLiteDataAccessCarRefuelTrackerModule.LoadCars());
        }

        #region Implementation of IViewModelHelper
        public void RegisterCommands()
        {
            //ToDO: Initalize here the DelegateCommands
            CarSelectionChangedCommand = new DelegateCommand(CarSelectionChanged);
        }
        #endregion

        private void CarSelectionChanged()
        {
            
        }
        public DelegateCommand CarSelectionChangedCommand { get; set; }
    }
}
