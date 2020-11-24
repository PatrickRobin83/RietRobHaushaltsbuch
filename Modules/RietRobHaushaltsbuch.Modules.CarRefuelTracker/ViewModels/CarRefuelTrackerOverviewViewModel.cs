﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using MahApps.Metro.Controls;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using RietRobHaushaltbuch.Core;
using RietRobHaushaltsbuch.Modules.CarRefuelTracker.DataAccess;
using RietRobHaushaltsbuch.Modules.CarRefuelTracker.Models;
using RietRobHaushaltsbuch.Modules.CarRefuelTracker.Views;

namespace RietRobHaushaltsbuch.Modules.CarRefuelTracker.ViewModels
{
    public class CarRefuelTrackerOverviewViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        #region Fields

        private IEventAggregator _eventAggregator;
        private string _title;
        private CarModel _selectedCarModel;
        private ObservableCollection<CarModel> _availableCars;
        private bool _isCarModelSelected = false;
        private bool _visibility;

        private CarRefuelTrackerCarEntryOverViewModel _carEntryOverViewModel;

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
        public bool Visibility
        {
            get { return _visibility; }
            set { SetProperty(ref _visibility, value); }
        }
        public CarRefuelTrackerCarEntryOverViewModel CarEntryOverViewModel
        {
            get { return _carEntryOverViewModel; }
            set { SetProperty(ref _carEntryOverViewModel, value); }
        }
        #endregion

        #region Contstructor

        public CarRefuelTrackerOverviewViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            Title = "CarRefuelTracker Overview";
            AvailableCars = new ObservableCollection<CarModel>(SQLiteDataAccess.LoadCars());
            RegisterCommands();
            _eventAggregator.GetEvent<EditCarEvent>().Subscribe(CarCreated);
        }
        #endregion

        #region Methods
        private void CarCreated(string parameter)
        {
            if (parameter.Equals("CarSaved") || parameter.Equals("Cancel"))
            {
                AvailableCars = new ObservableCollection<CarModel>(SQLiteDataAccess.LoadCars());
            }
        }

        private void RegisterCommands()
        {
            CreateCarCommand = new DelegateCommand(CreateCar);
            CarSelectionChangedCommand = new DelegateCommand(CarSelectionChanged);
            EditCarCommand = new DelegateCommand(EditCar).ObservesCanExecute(() => IsCarModelSelected);
            DeleteCarCommand = new DelegateCommand(DeleteCar).ObservesCanExecute(() => IsCarModelSelected);

        }

        private void CarSelectionChanged()
        {
            if (SelectedCarModel != null)
            {
                IsCarModelSelected = true;
                Visibility = true;
                _eventAggregator.GetEvent<CarModelSendEvent>().Publish(SelectedCarModel);

            }
            else
            {
                IsCarModelSelected = false;
                Visibility = false;
            }
        }

        private void EditCar()
        {
            CarDetailsView carDetailsView = new CarDetailsView();
            carDetailsView.DataContext = new CarDetailsViewModel(SelectedCarModel, _eventAggregator);
            carDetailsView.SaveWindowPosition = true;
            carDetailsView.ShowDialog();
        }

        private void DeleteCar()
        {
            SQLiteDataAccess.DeleteCar(SelectedCarModel);
            AvailableCars = new ObservableCollection<CarModel>(SQLiteDataAccess.LoadCars());
            SelectedCarModel = AvailableCars.FirstOrDefault();
        }

        private void CreateCar()
        {
            CarDetailsView carDetailsView = new CarDetailsView();
            carDetailsView.DataContext = new CarDetailsViewModel(_eventAggregator);
            carDetailsView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            carDetailsView.SaveWindowPosition = true;
            carDetailsView.ShowDialog();
        }
        #endregion


        #region Commands

        public DelegateCommand CreateCarCommand { get; set; }
        public DelegateCommand EditCarCommand { get; set; }
        public DelegateCommand DeleteCarCommand { get; set; }
        public DelegateCommand CarSelectionChangedCommand { get; set; }

        #endregion
    }
}
