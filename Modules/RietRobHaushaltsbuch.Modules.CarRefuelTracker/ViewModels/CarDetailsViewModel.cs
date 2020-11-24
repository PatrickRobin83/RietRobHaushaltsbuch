/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   CarDetailsViewModel.cs
 *   Date			:   2020-11-24 12:45:11
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Windows;
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
    public class CarDetailsViewModel : BindableBase, ICloseWindows
    {

        #region Fields

        private CarModel _carModel;
        private int _id;
        private bool _isActive;
        private BrandModel _selectedBrand;
        private ModelTypeModel _selectedModelType;
        private FuelTypeModel _selectedFuelType;
        private IEventAggregator _eventAggregator;
        private ObservableCollection<EntryModel> _entries;
        private ObservableCollection<BrandModel> _availableBrands;
        private ObservableCollection<ModelTypeModel> _availableCarModels;
        private ObservableCollection<FuelTypeModel> _availableFuelTypes;
        #endregion

        #region Properties
        public ObservableCollection<FuelTypeModel> AvailableFuelTypes
        {
            get { return _availableFuelTypes; }
            set { SetProperty(ref _availableFuelTypes, value); }
        }
        public ObservableCollection<ModelTypeModel> AvailableCarModels
        {
            get { return _availableCarModels; }
            set { SetProperty(ref _availableCarModels, value); }
        }
        public ObservableCollection<BrandModel> AvailableBrands
        {
            get { return _availableBrands; }
            set { SetProperty(ref _availableBrands, value); }
        }
        public ObservableCollection<EntryModel> Entries
        {
            get { return _entries; }
            set { SetProperty(ref _entries, value); }
        }
        public FuelTypeModel SelectedFuelType
        {
            get { return _selectedFuelType; }
            set { SetProperty(ref _selectedFuelType, value); }
        }
        public ModelTypeModel SelectedModelType
        {
            get { return _selectedModelType; }
            set { SetProperty(ref _selectedModelType, value); }
        }
        public BrandModel SelectedBrand
        {
            get { return _selectedBrand; }
            set { SetProperty(ref _selectedBrand, value); }
        }
        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value); }
        }
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public CarModel CarModel
        {
            get { return _carModel; }
            set { SetProperty(ref _carModel, value); }
        }
        #endregion

        #region Constructor

        public CarDetailsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            RegisterCommands();
            CarModel = new CarModel();
            AvailableFuelTypes = new ObservableCollection<FuelTypeModel>(SQLiteDataAccess.LoadAllFuelTypes());
            AvailableBrands = new ObservableCollection<BrandModel>(SQLiteDataAccess.LoadAllBrands());

            if (AvailableBrands != null && AvailableBrands.Count > 0)
            {
                SelectedBrand = AvailableBrands.First();
                AvailableCarModels = new ObservableCollection<ModelTypeModel>(SQLiteDataAccess.ModelsFromBrands(SelectedBrand.Id));
            }

            if (AvailableCarModels != null && AvailableCarModels.Count > 0)
            {
                SelectedModelType = AvailableCarModels.First();
            }

            if (AvailableFuelTypes != null && AvailableFuelTypes.Count > 0)
            {
                SelectedFuelType = AvailableFuelTypes.First();
            }

            IsActive = true;
            
        }
        public CarDetailsViewModel(CarModel carModel, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            RegisterCommands();
            CarModel = carModel;
            DataToControls();
        }

        private void DataToControls()
        {   
            AvailableBrands = new ObservableCollection<BrandModel>(SQLiteDataAccess.LoadAllBrands());
            AvailableFuelTypes = new ObservableCollection<FuelTypeModel>(SQLiteDataAccess.LoadAllFuelTypes());
            Id = CarModel.Id;
            IsActive = CarModel.IsActive;
            SelectedBrand = CarModel.Brand;
            AvailableCarModels = new ObservableCollection<ModelTypeModel>(SQLiteDataAccess.ModelsFromBrands(SelectedBrand.Id));
            SelectedModelType = CarModel.ModelType;
            SelectedFuelType = CarModel.FuelType;
            Entries = CarModel.Entries;
        }


        #endregion

        #region Methods
        private void RegisterCommands()
        {
            SaveCarCommand = new DelegateCommand(SaveCar);
            CloseCarDetailsCommand = new DelegateCommand(CloseCarDetails);
            AddBrandCommand = new DelegateCommand(AddBrand);
            CarBrandChangedCommand = new DelegateCommand(BrandChanged);
        }

        private void BrandChanged()
        {
            AvailableCarModels = new ObservableCollection<ModelTypeModel>(SQLiteDataAccess.ModelsFromBrands(SelectedBrand.Id));
        }

        private void AddBrand()
        {
            AddBrandView addBrandView = new AddBrandView();
            addBrandView.DataContext = new AddBrandViewModel();
            addBrandView.ShowDialog();
        }

        private void CloseCarDetails()
        {
            _eventAggregator.GetEvent<EditCarEvent>().Publish("Cancel");
            Close?.Invoke();
            
        }

        private void SaveCar()
        {
            CarModel carToSave = new CarModel();
            carToSave.Id = Id;
            carToSave.IsActive = IsActive;
            carToSave.Brand = SelectedBrand;
            carToSave.BrandId = SelectedBrand.Id;
            carToSave.ModelType = SelectedModelType;
            carToSave.ModelId = SelectedModelType.Id;
            carToSave.FuelType = SelectedFuelType;
            carToSave.TypeoffuelId = SelectedFuelType.Id;
            carToSave.Entries = Entries;

            if (carToSave.Id > 0)
            {
                SQLiteDataAccess.UpdateCar(carToSave);
            }
            else
            {
                SQLiteDataAccess.SaveCar(carToSave);
            }

            _eventAggregator.GetEvent<CarModelSendEvent>().Publish("CarSaved");
            _eventAggregator.GetEvent<CarModelSendEvent>().Publish(carToSave);
            CloseCarDetails();
        }
        #endregion

        public DelegateCommand SaveCarCommand { get; set; }
        public DelegateCommand CloseCarDetailsCommand { get; set; }
        public DelegateCommand AddBrandCommand { get; set; }
        public DelegateCommand CarBrandChangedCommand { get; set; }

        public Action Close { get; set; }
    }
}