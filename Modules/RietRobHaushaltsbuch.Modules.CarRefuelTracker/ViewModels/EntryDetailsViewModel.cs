/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   EntryDetailsViewModel.cs
 *   Date			:   2020-11-25 16:36:16
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using Prism.Commands;
using Prism.Events;
using RietRobHaushaltbuch.Core.Base;
using RietRobHaushaltbuch.Core.DataAccess;
using RietRobHaushaltbuch.Core.Events;
using RietRobHaushaltbuch.Core.Helper;
using RietRobHaushaltbuch.Core.Interfaces;
using RietRobHaushaltbuch.Core.Models;
using System;

namespace RietRobHaushaltsbuch.Modules.CarRefuelTracker.ViewModels
{
    public class EntryDetailsViewModel : ViewModelBase, IViewModelHelper
    {
        #region Fields

        private DateTime _selectedDate;
        private int _id;
        private CarModel _carModel;
        private string _pricePerLiter;
        private string _amountOfFuel;
        private string _drivenDiastance;
        private bool _isOpen = false;
        private double tryparseOutput;
        private readonly IEventAggregator _eventAggregator;
        private EntryModel _entryModel;

        #endregion

        #region Properties
        public string DrivenDistance
        {
            get { return _drivenDiastance; }
            set { SetProperty(ref _drivenDiastance, value); }
        }
        public string AmountOfFuel
        {
            get { return _amountOfFuel; }
            set { SetProperty(ref _amountOfFuel, value); }
        }
        public string PricePerLiter
        {
            get { return _pricePerLiter; }
            set { SetProperty(ref _pricePerLiter, value); }
        }
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { SetProperty(ref _selectedDate, value);
            }
        }
        public bool IsOpen
        {
            get { return _isOpen; }
            set { SetProperty(ref _isOpen, value); }
        }
        #endregion

        #region Constructor

        public EntryDetailsViewModel(IEventAggregator eventAggregator, CarModel carModel)
        {
            _carModel = carModel;
            _eventAggregator = eventAggregator;
            RegisterCommands();
            SelectedDate = DateTime.Now;
            Initialize();
        }

        public EntryDetailsViewModel(IEventAggregator eventAggregator, CarModel carModel, EntryModel entryModelmodel)
        {
            _eventAggregator = eventAggregator;
            RegisterCommands();
            _carModel = carModel;
            _entryModel = entryModelmodel;
            Initialize();

        }

        #endregion

        #region Methods

        private void Initialize()
        {
            if (_entryModel != null)
            {
                Id = _entryModel.Id;
                SelectedDate = Convert.ToDateTime(_entryModel.EntryDate);
                AmountOfFuel = _entryModel.AmountOffuel;
                PricePerLiter = _entryModel.PricePerLiter;
                DrivenDistance = _entryModel.DrivenDistance;

            }
            else
            {
                _entryModel = new EntryModel();
                AmountOfFuel = "";
                PricePerLiter = "";
                DrivenDistance = "";
            }
        }

        public override void RegisterCommands()
        {
            SelectedDateTimeChangedCommand = new DelegateCommand(DateChanged).ObservesProperty(() => SelectedDate);
            TextboxChangedCommand = new DelegateCommand<string>(TextChanged);
            CancelAddEntryCommand = new DelegateCommand(CancelAddEntry);
            AddEntryCommand = new DelegateCommand(AddEntry);
        }

        private void AddEntry()
        {

            if (_entryModel.Id != 0)
            {
                _entryModel.Id = Id;
                _entryModel.EntryDate = SelectedDate.ToShortDateString();
                _entryModel.PricePerLiter = PricePerLiter;
                _entryModel.AmountOffuel = AmountOfFuel;
                _entryModel.DrivenDistance = DrivenDistance;

                _entryModel.ConsumptationPerHundredKilometer =
                    EntryCalculator.CalculateAverage(AmountOfFuel, DrivenDistance);

                _entryModel.CostPerHundredKilometer = 
                    EntryCalculator.CalculateAverage(EntryCalculator.CalculateTotalFuelCosts(AmountOfFuel,PricePerLiter),
                    DrivenDistance);
                _entryModel.TotalAmount = EntryCalculator.CalculateTotalFuelCosts(AmountOfFuel, PricePerLiter);

                SqLiteDataAccessCarRefuelTrackerModule.UpdateEntryInDatabase(_entryModel);
            }
            else
            {
                _entryModel.EntryDate = SelectedDate.ToShortDateString();
                _entryModel.PricePerLiter = PricePerLiter;
                _entryModel.AmountOffuel = AmountOfFuel;
                _entryModel.DrivenDistance = DrivenDistance;
                _entryModel.ConsumptationPerHundredKilometer =
                    EntryCalculator.CalculateAverage(AmountOfFuel, DrivenDistance);

                _entryModel.CostPerHundredKilometer =
                    EntryCalculator.CalculateAverage(EntryCalculator.CalculateTotalFuelCosts(AmountOfFuel, PricePerLiter),
                        DrivenDistance);
                
                _entryModel.TotalAmount = EntryCalculator.CalculateTotalFuelCosts(AmountOfFuel, PricePerLiter);
                _entryModel.CarId = _carModel.Id;
                SqLiteDataAccessCarRefuelTrackerModule.SaveEntryInDatabase(_entryModel);
            }
            
            _eventAggregator.GetEvent<NewsEvent>().Publish("EntryClosed");
            Close?.Invoke();
        }

        private void CancelAddEntry()
        {
            _eventAggregator.GetEvent<NewsEvent>().Publish("EntryClosed");
            Close?.Invoke();
        }

        private void TextChanged(string param)
        {
            if (param.Equals("PricePerLiter"))
            {
                if (!double.TryParse(PricePerLiter, out tryparseOutput))
                {
                    PricePerLiter = string.Empty;
                }
            }

            if (param.Equals("AmountOfFuel"))
            {
                if (!double.TryParse(AmountOfFuel, out tryparseOutput))
                {
                    AmountOfFuel = string.Empty;
                }
            }
            if (param.Equals("DrivenDistance"))
            {
                if (!double.TryParse(DrivenDistance, out tryparseOutput))
                {
                    DrivenDistance = string.Empty;
                }
            }
        }

        private void DateChanged()
        {
            IsOpen = false;
        }

        #endregion

        #region Commands
        public DelegateCommand SelectedDateTimeChangedCommand { get; set; }
        public DelegateCommand<string> TextboxChangedCommand { get; set; }
        public DelegateCommand CancelAddEntryCommand { get; set; }
        public DelegateCommand AddEntryCommand { get; set; }

        #endregion



    }
}