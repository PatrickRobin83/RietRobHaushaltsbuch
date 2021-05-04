using System;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using RietRobHaushaltbuch.Core.Base;
using RietRobHaushaltbuch.Core.DataAccess;
using RietRobHaushaltbuch.Core.Enum;
using RietRobHaushaltbuch.Core.Events;
using RietRobHaushaltbuch.Core.Helper;
using RietRobHaushaltbuch.Core.Interfaces;
using RietRobHaushaltbuch.Core.Models;
using NLog;

namespace RietRobHaushaltsbuch.Modules.OverView.ViewModels
{
    public class OverViewModel : ViewModelBase, IViewModelHelper
    {
        #region Fields

        private string _headLine;
        private ObservableCollection<CarModel> _availableCars;
        private CarModel _selectedCar;
        private string _selectedYear;
        private ObservableCollection<string> _yearToSelect = new ObservableCollection<string>();
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Properties

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

        public string SelectedYear
        {
            get { return _selectedYear; }
            set { SetProperty(ref _selectedYear, value); }
        }

        public ObservableCollection<string> YearToSelect
        {
            get { return _yearToSelect; }
            set { SetProperty(ref _yearToSelect, value); }
        }

        #endregion

        #region Constructor

        public OverViewModel(IEventAggregator eventaggregator)
        {
            eventaggregator.GetEvent<NewsEvent>().Subscribe(LoadOverView);
            HeadLine = "Übersicht der Gesamtkosten für das Jahr: ";
            YearToSelect = CreateYearEntrysForComboBoxSelection.AddYearsToComboBox();
            SelectedYear = YearToSelect.FirstOrDefault();
            RegisterCommands();
            LoadCars();
        }

        #endregion

        #region Methods
        public void LoadCars()
        {
            AvailableCars = new ObservableCollection<CarModel>(SqLiteDataAccessCarRefuelTrackerModule.LoadCars(SelectedYear));
            foreach (CarModel car in AvailableCars)
            {
                car.CarName = $"{car.Brand.BrandName} {car.ModelType.ModelName}";
                car.AveragePricePerLiter = AverageCalculator.CalculateAveragePricePerLiter(car.Entries);
                car.TotalFuelAmount = AverageCalculator.CalculateTotalFuelAmount(car.Entries);
                car.TotalRefuelCosts = AverageCalculator.CalculateTotalRefuelCosts(car.Entries);
                car.TotalDrivenDistance = AverageCalculator.CalculateTotalDrivenDistance(car.Entries);
                car.AverageConsumption = AverageCalculator.CalculateTotalAverageConsumption(car.Entries);
                car.AverageCostsOfHundredKilometer = AverageCalculator.CalculateTotalAverageCostsOfHundredKilometer(car.Entries);
            }
        }

        #region Implementation of IViewModelHelper
        public void RegisterCommands()
        {
            CarSelectionChangedCommand = new DelegateCommand(CarSelectionChanged);
            YearSelectionChangedCommand = new DelegateCommand(SelectedYearChanged);
        }
        #endregion
        private void CarSelectionChanged()
        {

        }
        private void SelectedYearChanged()
        {
            LoadCars();
        }
        private void LoadOverView(string parameter)
        {
            if (parameter.Equals("OverView"))
            {
                LoadCars();
            }
        }
        #endregion

        #region Commands
        public DelegateCommand CarSelectionChangedCommand { get; set; }
        public DelegateCommand YearSelectionChangedCommand { get; set; }
        #endregion

    }
}
