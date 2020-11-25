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
using RietRobHaushaltbuch.Core.Interfaces;
using RietRobHaushaltsbuch.Modules.CarRefuelTracker.DataAccess;
using RietRobHaushaltsbuch.Modules.CarRefuelTracker.Models;
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
        private string _averageFuelAmount;
        private string _averageRefuelCosts;
        private string _averageDrivenDistance;
        private string _averageConsumption;
        private string _averageCosts;
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

        public string AverageCosts
        {
            get { return _averageCosts; }
            set { SetProperty(ref _averageCosts, value); }
        }

        public string AverageConsumption
        {
            get { return _averageConsumption; }
            set { SetProperty(ref _averageConsumption, value); }
        }

        public string AverageDrivenDistance
        {
            get { return _averageDrivenDistance; }
            set { SetProperty(ref _averageDrivenDistance, value); }
        }

        public string AverageRefuelCosts
        {
            get { return _averageRefuelCosts; }
            set { SetProperty(ref _averageRefuelCosts, value); }
        }

        public string AverageFuelAmount
        {
            get { return _averageFuelAmount; }
            set { SetProperty(ref _averageFuelAmount, value); }
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

        public CarRefuelTrackerCarEntryOverViewModel(CarModel carModel, IEventAggregator ea)
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
            AllEntrysForSelectedCar = new ObservableCollection<EntryModel>(SQLiteDataAccess.LoadEntrysForCar(CarModel.Id));
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
            double tmpPricePerLiter = 0;
            double tmpFuelAmount = 0;
            double tmpRefuelCosts = 0;
            double tmpDrivenDistance = 0;
            double tmpConsumption = 0;
            double tmpCosts = 0;

            foreach (EntryModel entryModel in AllEntrysForSelectedCar)
            {
                tmpPricePerLiter += Convert.ToDouble(entryModel.PricePerLiter);
                tmpFuelAmount += Convert.ToDouble(entryModel.AmountOffuel);
                tmpRefuelCosts += Convert.ToDouble(entryModel.TotalAmount);
                tmpDrivenDistance += Convert.ToDouble(entryModel.DrivenDistance);
                tmpConsumption += Convert.ToDouble(entryModel.ConsumptationPerHundredKilometer);
                tmpCosts += Convert.ToDouble(entryModel.CostPerHundredKilometer);
            }
            #region double.IsNan Validation

            if (!double.IsNaN(Math.Round(tmpPricePerLiter / AllEntrysForSelectedCar.Count, 3)))
            {
                AveragePricePerLiter = Convert.ToString(Math.Round(tmpPricePerLiter / AllEntrysForSelectedCar.Count, 3));
            }
            else
            {
                AveragePricePerLiter = "0";
            }
            if (!double.IsNaN(Math.Round(tmpFuelAmount, 2)))
            {
                AverageFuelAmount = Convert.ToString(Math.Round(tmpFuelAmount,2));
            }
            else
            {
                AverageFuelAmount = "0";
            }

            if (!double.IsNaN(Math.Round(tmpRefuelCosts, 2)))
            {
                AverageRefuelCosts = Convert.ToString(Math.Round(tmpRefuelCosts, 2));
            }
            else
            {
                AverageRefuelCosts = "0";
            }

            if (!double.IsNaN(Math.Round(tmpDrivenDistance, 2)))
            {
                AverageDrivenDistance = Convert.ToString(Math.Round(tmpDrivenDistance,0));
            }
            else
            {
                AverageDrivenDistance = "0";
            }

            if (!double.IsNaN(Math.Round(tmpConsumption / AllEntrysForSelectedCar.Count, 2)))
            {
                AverageConsumption = Convert.ToString(Math.Round(tmpConsumption / AllEntrysForSelectedCar.Count, 2));
            }
            else
            {
                AverageConsumption = "0";
            }
            if (!double.IsNaN(Math.Round(tmpCosts / AllEntrysForSelectedCar.Count, 2)))
            {
                AverageCosts = Convert.ToString(Math.Round(tmpCosts / AllEntrysForSelectedCar.Count, 2));
            }
            else
            {
                AverageCosts = "0";
            }
            #endregion
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
            SQLiteDataAccess.DeleteEntryFromDatabase(SelectedEntryModel);
            UpdateEntryList();
        }

        private void EditEntry()
        {
            EntryDetailsView entryDetailsView = new EntryDetailsView();
            entryDetailsView.DataContext = new EntryDetailsViewModel(_eventAggregator, CarModel, SelectedEntryModel);
            entryDetailsView.ShowDialog();
        }

        private void AddEntry()
        {
            EntryDetailsView entryDetailsView = new EntryDetailsView();
            entryDetailsView.DataContext = new EntryDetailsViewModel(_eventAggregator, CarModel);
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