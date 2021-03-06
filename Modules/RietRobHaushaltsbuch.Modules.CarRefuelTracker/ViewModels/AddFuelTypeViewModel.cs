﻿/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   AddFuelTypeViewModel.cs
 *   Date			:   2020-11-25 14:44:59
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
using RietRobHaushaltbuch.Core.Enum;
using RietRobHaushaltbuch.Core.Events;
using RietRobHaushaltbuch.Core.Helper;
using RietRobHaushaltbuch.Core.Interfaces;
using RietRobHaushaltbuch.Core.Models;

namespace RietRobHaushaltsbuch.Modules.CarRefuelTracker.ViewModels
{
    public class AddFuelTypeViewModel : ViewModelBase, IViewModelHelper
    {
        #region Fields
        private readonly IEventAggregator _eventAggregator;
        private bool _hasCharacters;
        private int _height = 180;
        private int _width = 380;
        private string _title = "Kraftstoffart hinzufügen";
        private string _txtFuelTypeName;
        private static NLog.Logger appLogger = NLog.LogManager.GetCurrentClassLogger();
        #endregion

        #region Properties
        public bool HasCharacters
        {
            get { return _hasCharacters; }
            set { SetProperty(ref _hasCharacters, value); }
        }
        public string TxtFuelTypeName
        {
            get { return _txtFuelTypeName; }
            set { SetProperty(ref _txtFuelTypeName, value); }
        }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public int Width
        {
            get { return _width; }
            set { SetProperty(ref _width, value); }
        }
        public int Height
        {
            get { return _height; }
            set { SetProperty(ref _height, value); }
        }
        #endregion

        #region Constructor

        public AddFuelTypeViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            RegisterCommands();
        }
        #endregion

        #region Methods
        public void RegisterCommands()
        {
            AddFuelTypeCommand = new DelegateCommand(AddFuelType).ObservesProperty(() => HasCharacters); ;
            CancelAddFuelTypeCommand = new DelegateCommand(CancelAddFuel);
            TextChangedCommand = new DelegateCommand(TextChanged).ObservesProperty(() => HasCharacters);
        }
        private void TextChanged()
        {
            if (!string.IsNullOrWhiteSpace(TxtFuelTypeName) && TxtFuelTypeName.Length >= 2)
            {
                HasCharacters = true;
            }
            else
            {
                HasCharacters = false;
            }
        }
        private void CancelAddFuel()
        {
            LogHelper.WriteToLog(appLogger, "Add fueltype canceled", LogState.Info);
            Close?.Invoke();
        }
        private void AddFuelType()
        {
            FuelTypeModel fuelTypeModelToAdd = new FuelTypeModel {TypeOfFuel = TxtFuelTypeName};
            fuelTypeModelToAdd.Id = SqLiteDataAccessCarRefuelTrackerModule.AddFuelType(fuelTypeModelToAdd).Id;
            _eventAggregator.GetEvent<ObjectEvent>().Publish(fuelTypeModelToAdd);
            LogHelper.WriteToLog(appLogger, $"Fueltype {TxtFuelTypeName} added", LogState.Info);
            Close?.Invoke();
        }
        #endregion

        #region Commands
        public DelegateCommand AddFuelTypeCommand { get; set; }
        public DelegateCommand CancelAddFuelTypeCommand { get; set; }
        public DelegateCommand TextChangedCommand { get; set; }
        #endregion
    }
}