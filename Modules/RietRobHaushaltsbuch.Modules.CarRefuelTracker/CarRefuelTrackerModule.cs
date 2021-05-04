using RietRobHaushaltsbuch.Modules.CarRefuelTracker.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using RietRobHaushaltbuch.Core;
using RietRobHaushaltsbuch.Modules.CarRefuelTracker.ViewModels;

namespace RietRobHaushaltsbuch.Modules.CarRefuelTracker
{
    public class CarRefuelTrackerModule : IModule
    {
        #region Fields
        private readonly IRegionManager _regionManager;
        #endregion

        #region Properties

        #endregion

        #region Constructor
        public CarRefuelTrackerModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        #endregion

        #region Methods

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(CarRefuelTrackerOverView));
            _regionManager.RegisterViewWithRegion(RegionNames.CarEntryOverViewRegion, typeof(CarRefuelTrackerCarEntryOverView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ViewModelLocationProvider.Register<CarRefuelTrackerOverView, CarRefuelTrackerOverviewViewModel>();
            ViewModelLocationProvider.Register<CarRefuelTrackerCarEntryOverView, CarRefuelTrackerCarEntryOverViewModel>();
            ViewModelLocationProvider.Register<CarDetailsView, CarDetailsViewModel>();
            ViewModelLocationProvider.Register<AddBrandView, AddBrandViewModel>();
            ViewModelLocationProvider.Register<AddModelTypeView, AddModelTypeViewModel>();
            ViewModelLocationProvider.Register<AddFuelTypeView, AddFuelTypeViewModel>();
            ViewModelLocationProvider.Register<EntryDetailsView, EntryDetailsViewModel>();
            containerRegistry.RegisterForNavigation<CarRefuelTrackerCarEntryOverView>();

        }

        #endregion

        #region Commands

        #endregion

        #region Events
        #endregion
    }
}