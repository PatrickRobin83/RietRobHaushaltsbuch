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

using System;
using System.Runtime.Versioning;
using Prism.Commands;
using Prism.Events;
using RietRobHaushaltbuch.Core;
using RietRobHaushaltbuch.Core.Interfaces;
using RietRobHaushaltsbuch.Modules.CarRefuelTracker.DataAccess;
using RietRobHaushaltsbuch.Modules.CarRefuelTracker.Models;

namespace RietRobHaushaltsbuch.Modules.CarRefuelTracker.ViewModels
{
    public class EntryDetailsViewModel : BaseViewModel, IViewModelHelper
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

        #endregion

        #region Methods
        public void RegisterCommands()
        {
            SelectedDateTimeChangedCommand = new DelegateCommand(DateChanged).ObservesProperty(() => SelectedDate);
            TextboxChangedCommand = new DelegateCommand<string>(TextChanged);
            CancelAddEntryCommand = new DelegateCommand(CancelAddEntry);
            AddEntryCommand = new DelegateCommand(AddEntry);
        }

        private void AddEntry()
        {
            double tmpAmountFuel;
            double tmpPricePerLiter;
            double tmpDrivenDistance;
            double consumption;
            double totalAmount;
            double costPerHundred;
            
            if (_entryModel.Id != 0)
            {
                _entryModel.Id = Id;
                _entryModel.EntryDate = SelectedDate.ToShortDateString();
                _entryModel.PricePerLiter = PricePerLiter;
                _entryModel.AmountOffuel = AmountOfFuel;
                _entryModel.DrivenDistance = DrivenDistance;
                tmpAmountFuel = Convert.ToDouble(AmountOfFuel);
                tmpPricePerLiter = Convert.ToDouble(PricePerLiter);
                tmpDrivenDistance = Convert.ToDouble(DrivenDistance);
                consumption = tmpAmountFuel / tmpDrivenDistance * 100;
                totalAmount = tmpPricePerLiter * tmpAmountFuel;
                costPerHundred = totalAmount / tmpDrivenDistance * 100;
                consumption = Math.Round(consumption, 2);
                totalAmount = Math.Round(totalAmount, 2);
                costPerHundred = Math.Round(costPerHundred, 2);
                _entryModel.ConsumptationPerHundredKilometer = consumption.ToString();
                _entryModel.CostPerHundredKilometer = costPerHundred.ToString();
                _entryModel.TotalAmount = totalAmount.ToString();

                SQLiteDataAccess.UpdateEntryInDatabase(_entryModel);
            }
            else
            {
                _entryModel.EntryDate = SelectedDate.ToShortDateString();
                _entryModel.PricePerLiter = PricePerLiter;
                _entryModel.AmountOffuel = AmountOfFuel;
                _entryModel.DrivenDistance = DrivenDistance;
                tmpAmountFuel = Convert.ToDouble(AmountOfFuel);
                tmpPricePerLiter = Convert.ToDouble(PricePerLiter);
                tmpDrivenDistance = Convert.ToDouble(DrivenDistance);
                consumption = tmpAmountFuel / tmpDrivenDistance * 100;
                totalAmount = tmpPricePerLiter * tmpAmountFuel;
                costPerHundred = totalAmount / tmpDrivenDistance * 100;
                consumption = Math.Round(consumption, 2);
                totalAmount = Math.Round(totalAmount, 2);
                costPerHundred = Math.Round(costPerHundred, 2);
                _entryModel.ConsumptationPerHundredKilometer = consumption.ToString();
                _entryModel.CostPerHundredKilometer = costPerHundred.ToString();
                _entryModel.TotalAmount = totalAmount.ToString();
                _entryModel.CarId = _carModel.Id;
                SQLiteDataAccess.SaveEntryInDatabase(_entryModel);
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