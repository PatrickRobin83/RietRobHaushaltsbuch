using System;
using System.Collections.ObjectModel;
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
        private string _headLine;
        private ObservableCollection<CarModel> _availableCars;
        private CarModel _selectedCar;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();


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

        public OverViewModel(IEventAggregator eventaggregator)
        {
            eventaggregator.GetEvent<NewsEvent>().Subscribe(LoadOverView);
            HeadLine = "Übersicht der Gesamtkosten";
            RegisterCommands();
            LogHelper.WriteToLog("Loaded", logger, LogState.Debug);
           LoadCars();
        }

        private void LoadOverView(string parameter)
        {
            if (parameter.Equals("OverView"))
            {
                LoadCars();
            }
        }

        public void LoadCars()
        {
            AvailableCars = new ObservableCollection<CarModel>(SqLiteDataAccessCarRefuelTrackerModule.LoadCars());
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
        }
        #endregion

        private void CarSelectionChanged()
        {

        }
        public DelegateCommand CarSelectionChangedCommand { get; set; }
    }
}
