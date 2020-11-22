using RietRobHaushaltsbuch.Modules.CarRefuelTracker.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using RietRobHaushaltbuch.Core;

namespace RietRobHaushaltsbuch.Modules.CarRefuelTracker
{
    public class CarRefuelTrackerModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public CarRefuelTrackerModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion, typeof(ViewA));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}