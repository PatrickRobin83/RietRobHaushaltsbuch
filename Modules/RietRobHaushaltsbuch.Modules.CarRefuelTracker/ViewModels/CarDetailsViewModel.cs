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

using Prism.Commands;
using Prism.Events;
using RietRobHaushaltbuch.Core.Base;
using RietRobHaushaltbuch.Core.DataAccess;
using RietRobHaushaltbuch.Core.Events;
using RietRobHaushaltbuch.Core.Interfaces;
using RietRobHaushaltbuch.Core.Models;
using RietRobHaushaltsbuch.Modules.CarRefuelTracker.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Prism.Services.Dialogs;

namespace RietRobHaushaltsbuch.Modules.CarRefuelTracker.ViewModels
{
    public class CarDetailsViewModel : ViewModelBase, IViewModelHelper
    {
        //TODo: Implement a Do you really want to delete the xxxx from Database Dialog before Deleting something.
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
            Initialize();
        }
        public CarDetailsViewModel(CarModel carModel, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            CarModel = carModel;
            DataToControls();
        }

        #endregion

        #region Methods
        private void DataToControls()
        {
            RefreshBrandModelList();
            RefreshFuelTypeList();
            Id = CarModel.Id;
            IsActive = CarModel.IsActive;
            SelectedBrand = CarModel.Brand;
            RefreshCarModelList();
            SelectedModelType = CarModel.ModelType;
            SelectedFuelType = CarModel.FuelType;
            Entries = CarModel.Entries;
            _eventAggregator.GetEvent<ObjectEvent>().Subscribe(HandleObjectEvent);
            _eventAggregator.GetEvent<NewsEvent>().Subscribe(HandleNewsEvents);
            RegisterCommands();
        }

        private void HandleNewsEvents(string parameter)
        {
            if (parameter.Equals("EntryClosed"))
            {
                Entries = new ObservableCollection<EntryModel>(SqLiteDataAccessCarRefuelTrackerModule.LoadEntrysForCar(Id, DateTime.Now.Year.ToString()));
            }
        }

        private void HandleObjectEvent(object model)
        {
            if (model.GetType() == typeof(BrandModel))
            {
                RefreshBrandModelList();
            }

            if (model.GetType() == typeof(ModelTypeModel))
            {
                RefreshCarModelList();
            }

            if (model.GetType() == typeof(FuelTypeModel))
            {
                RefreshFuelTypeList();
            }
        }

        private void Initialize()
        {
            CarModel = new CarModel();
            RefreshFuelTypeList();
            RefreshBrandModelList();
            RegisterCommands();

            if (AvailableBrands != null && AvailableBrands.Count > 0)
            {
                SelectedBrand = AvailableBrands.First();
                RefreshCarModelList();
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
            _eventAggregator.GetEvent<ObjectEvent>().Subscribe(HandleObjectEvent);

        }

        public void RegisterCommands()
        {
            SaveCarCommand = new DelegateCommand(SaveCar);
            CloseCarDetailsCommand = new DelegateCommand(CloseCarDetails);
            AddBrandCommand = new DelegateCommand(AddBrand);
            CarBrandChangedCommand = new DelegateCommand(BrandChanged);
            RemoveBrandCommand = new DelegateCommand(RemoveBrand);
            AddModelTypeCommand = new DelegateCommand(AddModelType);
            RemoveModelTypeCommand = new DelegateCommand(RemoveModelType);
            AddFuelTypeCommand = new DelegateCommand(AddFuelType);
            RemoveFuelTypeCommand = new DelegateCommand(RemoveFuelType);
        }
        
        private void AddModelType()
        {
            AddModelTypeView addModelTypeView = new AddModelTypeView
            {
                DataContext = new AddModelTypeViewModel(_eventAggregator, SelectedBrand)
            };
            addModelTypeView.ShowDialog();
        }
        private void RemoveModelType()
        {
            SqLiteDataAccessCarRefuelTrackerModule.RemoveModelTypeFromDatabase(SelectedModelType);
            RefreshCarModelList();
        }
        private void AddFuelType()
        {
            AddFuelTypeView addFuelTypeView = new AddFuelTypeView
            {
                DataContext = new AddFuelTypeViewModel(_eventAggregator)
            };
            addFuelTypeView.ShowDialog();
        }
        private void RemoveFuelType()
        {
            SqLiteDataAccessCarRefuelTrackerModule.RemoveFuelTypeFromDatabase(SelectedFuelType);
            RefreshFuelTypeList();
        }

        private void BrandChanged()
        {
            RefreshCarModelList();
        }

        private void AddBrand()
        {
            AddBrandView addBrandView = new AddBrandView
            {
                DataContext = new AddBrandViewModel(_eventAggregator),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                SaveWindowPosition = true
            };
            addBrandView.ShowDialog();
        }
        private void RemoveBrand()
        {
            SqLiteDataAccessCarRefuelTrackerModule.RemoveBrandFromDataBase(SelectedBrand);
            RefreshBrandModelList();
        }

        private void CloseCarDetails()
        {
            _eventAggregator.GetEvent<NewsEvent>().Publish("Cancel");
            Close?.Invoke();
            
        }

        private void SaveCar()
        {
            CarModel carToSave = new CarModel
            {
                Id = Id,
                IsActive = IsActive,
                Brand = SelectedBrand,
                BrandId = SelectedBrand.Id,
                ModelType = SelectedModelType,
                ModelId = SelectedModelType.Id,
                FuelType = SelectedFuelType,
                TypeoffuelId = SelectedFuelType.Id,
                Entries = Entries
            };

            if (carToSave.Id > 0)
            {
                SqLiteDataAccessCarRefuelTrackerModule.UpdateCar(carToSave);
            }
            else
            {
                SqLiteDataAccessCarRefuelTrackerModule.SaveCar(carToSave);
            }

            _eventAggregator.GetEvent<ObjectEvent>().Publish("CarSaved");
            _eventAggregator.GetEvent<ObjectEvent>().Publish(carToSave);
            CloseCarDetails();
        }

        private void RefreshBrandModelList()
        {
            AvailableBrands = new ObservableCollection<BrandModel>(SqLiteDataAccessCarRefuelTrackerModule.LoadAllBrands());
        }

        private void RefreshFuelTypeList()
        {
            AvailableFuelTypes = new ObservableCollection<FuelTypeModel>(SqLiteDataAccessCarRefuelTrackerModule.LoadAllFuelTypes());
        }

        private void RefreshCarModelList()
        {
            AvailableCarModels = new ObservableCollection<ModelTypeModel>(SqLiteDataAccessCarRefuelTrackerModule.ModelsFromBrands(SelectedBrand.Id));
        }

        #endregion

        #region Commands
        public DelegateCommand SaveCarCommand { get; set; }
        public DelegateCommand CloseCarDetailsCommand { get; set; }
        public DelegateCommand RemoveBrandCommand { get; set; }
        public DelegateCommand AddBrandCommand { get; set; }
        public DelegateCommand CarBrandChangedCommand { get; set; }
        public DelegateCommand AddModelTypeCommand { get; set; }
        public DelegateCommand RemoveModelTypeCommand { get; set; }
        public DelegateCommand AddFuelTypeCommand { get; set; }
        public DelegateCommand RemoveFuelTypeCommand { get; set; }
        #endregion
    }
}