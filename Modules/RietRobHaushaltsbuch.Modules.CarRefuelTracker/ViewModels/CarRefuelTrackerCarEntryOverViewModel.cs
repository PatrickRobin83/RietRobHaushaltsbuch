/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   CarRefuelTrackerCarEntryOverViewModel.cs
 *   Date			:   2020-11-24 09:09:05
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using RietRobHaushaltbuch.Core;
using RietRobHaushaltbuch.Core.DataAccess;
using RietRobHaushaltbuch.Core.Events;
using RietRobHaushaltbuch.Core.Helper;
using RietRobHaushaltbuch.Core.Interfaces;
using RietRobHaushaltbuch.Core.Models;
using RietRobHaushaltsbuch.Modules.CarRefuelTracker.ViewModels.Base;
using RietRobHaushaltsbuch.Modules.CarRefuelTracker.Views;

namespace RietRobHaushaltsbuch.Modules.CarRefuelTracker.ViewModels
{
    public class CarRefuelTrackerCarEntryOverViewModel : BaseViewModel, IViewModelHelper
    {
        #region Fields

        private ObservableCollection<EntryModel> _allEntrysForSelectedCar;
        private IEventAggregator _eventAggregator;
        private CarModel _carModel;
        private string _averagePricePerLiter;
        private string _totalFuelAmount;
        private string _totalRefuelCosts;
        private string _totalDrivenDistance;
        private string _averageConsumption;
        private string _averageCostsOfHundredKilometer;
        private EntryModel _selectedEntryModel;
        private bool _isEntryModelSelected;

        public bool IsEntryModelSelected
        {
            get { return _isEntryModelSelected; }
            set { SetProperty(ref _isEntryModelSelected, value); }
        }

        public EntryModel SelectedEntryModel
        {
            get { return _selectedEntryModel; }
            set { SetProperty(ref _selectedEntryModel, value); }
        }

        #endregion

        #region Properties

        public ObservableCollection<EntryModel> AllEntrysForSelectedCar
        {
            get { return _allEntrysForSelectedCar; }
            set { SetProperty(ref _allEntrysForSelectedCar, value); }
        }

        public string AverageCostsOfHundredKilometer
        {
            get { return _averageCostsOfHundredKilometer; }
            set { SetProperty(ref _averageCostsOfHundredKilometer, value); }
        }

        public string AverageConsumption
        {
            get { return _averageConsumption; }
            set { SetProperty(ref _averageConsumption, value); }
        }

        public string TotalDrivenDistance
        {
            get { return _totalDrivenDistance; }
            set { SetProperty(ref _totalDrivenDistance, value); }
        }

        public string TotalRefuelCosts
        {
            get { return _totalRefuelCosts; }
            set { SetProperty(ref _totalRefuelCosts, value); }
        }

        public string TotalFuelAmount
        {
            get { return _totalFuelAmount; }
            set { SetProperty(ref _totalFuelAmount, value); }
        }

        public string AveragePricePerLiter
        {
            get { return _averagePricePerLiter; }
            set { SetProperty(ref _averagePricePerLiter, value); }
        }

        
        public CarModel CarModel
        {
            get { return _carModel; }
            set { SetProperty(ref _carModel, value); }
        }

        #endregion

        #region Constructor

        public CarRefuelTrackerCarEntryOverViewModel(IEventAggregator ea)
        {
            _eventAggregator = ea;
            RegisterCommands();
            _eventAggregator.GetEvent<ObjectEvent>().Subscribe(HandleCarModelSelection);
            _eventAggregator.GetEvent<NewsEvent>().Subscribe(HandleEntryEvent);
        }
        #endregion

        #region Methods

        private void HandleEntryEvent(string entryEvent)
        {
            if (entryEvent.Equals("EntryClosed"))
            {
                UpdateEntryList();
            }
        }

        private void UpdateEntryList()
        {
            AllEntrysForSelectedCar = new ObservableCollection<EntryModel>(SqLiteDataAccessCarRefuelTrackerModule.LoadEntrysForCar(CarModel.Id));
            CalculateAverages();
        }

        private void HandleCarModelSelection(object selectedCarModel)
        {
            if (selectedCarModel.GetType() == typeof(CarModel))
            {
                CarModel = (CarModel)selectedCarModel;
                UpdateEntryList();
                CarModel.Entries = AllEntrysForSelectedCar;
            }
        }

        public void CalculateAverages()
        {
            AveragePricePerLiter = AverageCalculator.CalculateAveragePricePerLiter(AllEntrysForSelectedCar);
            TotalFuelAmount = AverageCalculator.CalculateTotalFuelAmount(AllEntrysForSelectedCar);
            TotalRefuelCosts = AverageCalculator.CalculateTotalRefuelCosts(AllEntrysForSelectedCar);
            TotalDrivenDistance = AverageCalculator.CalculateTotalDrivenDistance(AllEntrysForSelectedCar);
            AverageConsumption = AverageCalculator.CalculateTotalAverageConsumption(AllEntrysForSelectedCar);
            AverageCostsOfHundredKilometer = AverageCalculator.CalculateTotalAverageCostsOfHundredKilometer(AllEntrysForSelectedCar);
        }

        public void RegisterCommands()
        {
            AddEntryCommand = new DelegateCommand(AddEntry);
            EditEntryCommand = new DelegateCommand(EditEntry).ObservesCanExecute(() => IsEntryModelSelected);
            DeleteEntryCommand = new DelegateCommand(DeleteEntry).ObservesCanExecute(() => IsEntryModelSelected);
            EntrySelectionChangedCommand = new DelegateCommand(EntrySelectionChanged);
        }

        private void EntrySelectionChanged()
        {
            if (SelectedEntryModel != null)
            {
                IsEntryModelSelected = true;
            }
            else
            {
                IsEntryModelSelected = false;
            }
        }

        private void DeleteEntry()
        {
            SqLiteDataAccessCarRefuelTrackerModule.DeleteEntryFromDatabase(SelectedEntryModel);
            UpdateEntryList();
        }

        private void EditEntry()
        {
            EntryDetailsView entryDetailsView = new EntryDetailsView
            {
                DataContext = new EntryDetailsViewModel(_eventAggregator, CarModel, SelectedEntryModel)
            };
            entryDetailsView.ShowDialog();
        }

        private void AddEntry()
        {
            EntryDetailsView entryDetailsView = new EntryDetailsView
            {
                DataContext = new EntryDetailsViewModel(_eventAggregator, CarModel)
            };
            entryDetailsView.ShowDialog();
        }
        #endregion

        #region Commands
        public DelegateCommand AddEntryCommand { get; set; }
        public DelegateCommand EditEntryCommand { get; set; }
        public DelegateCommand DeleteEntryCommand { get; set; }
        public DelegateCommand EntrySelectionChangedCommand { get; set; }

        #endregion

    }
}